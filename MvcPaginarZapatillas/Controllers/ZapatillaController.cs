using Microsoft.AspNetCore.Mvc;
using MvcPaginarZapatillas.Models;
using MvcPaginarZapatillas.Repository;

namespace MvcPaginarZapatillas.Controllers
{
    public class ZapatillaController : Controller
    {
        private ZapatillasRepository _repo;

        public ZapatillaController(ZapatillasRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IActionResult>Index()
        {
            List<Zapatilla> zapatillas = await this._repo.GetZapatillasAsync();
            return View(zapatillas);
        }


        public async Task<IActionResult> DetailsPartial(int idZapatilla, int? posicion)
        {
            Zapatilla zapatilla = await this._repo.GetZapatillaByIdAsync(idZapatilla);

            if (posicion == null)
            {
                posicion = 1;
            }

            ImagenPaginada imagenPaginada = await this._repo.GetImagenZapatillasPaginadasAsync(idZapatilla, posicion.Value);

            ViewData["POSICION"] = posicion;
            ViewData["REGISTROTOTAL"] = imagenPaginada.TotalRegistros;
            ViewData["Imagenes"] = imagenPaginada.Imagenes;

            return View(zapatilla);
        }



        public async Task<IActionResult> ImagesPartial(int idZapatilla, int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            ImagenPaginada imagenPaginada = await this._repo.GetImagenZapatillasPaginadasAsync(idZapatilla, posicion.Value);
            ViewData["POSICION"] = posicion;
            ViewData["REGISTROTOTAL"] = imagenPaginada.TotalRegistros;
            return PartialView("_ImagesPartial", imagenPaginada.Imagenes);
        }


    }
}
