using System.ComponentModel.DataAnnotations.Schema;

namespace MvcPaginarZapatillas.Models
{
    public class ImagenPaginada
    {
        public int TotalRegistros { get; set; }
        public List<ImagenZapatilla> Imagenes { get; set; }
    }
}
