using System.Data;
using System.Net.WebSockets;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcPaginarZapatillas.Data;
using MvcPaginarZapatillas.Models;

#region PROCEDIMIENTOS
/*
 create procedure SP_GET_IMAGENES_ZAPATILLAS_PAGINADAS
   (@posicion int, @idproducto int, @registros int out)
   as
       select @registros = count(IDIMAGEN) from IMAGENESZAPASPRACTICA;
       with ImagenesPaginadas as (
           select CAST(ROW_NUMBER() over (order by IDIMAGEN) as int) as POSICION, IDIMAGEN, IDPRODUCTO, IMAGEN from IMAGENESZAPASPRACTICA
           where  IDPRODUCTO = @idproducto
       )
   SELECT POSICION, IDIMAGEN,IDPRODUCTO, IMAGEN FROM ImagenesPaginadas
   WHERE POSICION = @posicion;
       go
 */
#endregion

namespace MvcPaginarZapatillas.Repository
{
    public class ZapatillasRepository
    {
        private ZapatillasContext _context;
        public ZapatillasRepository(ZapatillasContext context)
        {
            _context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync ()
        {
            return await this._context.Zapatilla.ToListAsync();
        }

        public async Task<Zapatilla> GetZapatillaByIdAsync(int idZapatilla)
        {
            return await this._context.Zapatilla.Where(x => x.Id == idZapatilla)
                .FirstOrDefaultAsync();
        }

        public async Task<ImagenPaginada> GetImagenZapatillasPaginadasAsync(int idProducto, int posicion)
        {
            string sql = "SP_GET_IMAGENES_ZAPATILLAS_PAGINADAS @posicion, @idproducto, @registros OUTPUT";

            // Declaramos los parámetros de entrada y salida
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamIdProducto = new SqlParameter("@idproducto", idProducto);
            SqlParameter pamTotalRegistros = new SqlParameter("@registros", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            var imagenes = await this._context.ImagenZapatilla
                .FromSqlRaw(sql, pamPosicion, pamIdProducto, pamTotalRegistros)
                .ToListAsync();

            ImagenPaginada imagenPaginada = new ImagenPaginada();
            imagenPaginada.Imagenes = imagenes;
            imagenPaginada.TotalRegistros = (int)pamTotalRegistros.Value;  // Obtenemos el valor de los registros

            return imagenPaginada;
        }

    }
}
