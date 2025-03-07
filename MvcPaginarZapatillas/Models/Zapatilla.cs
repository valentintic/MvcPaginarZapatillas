using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcPaginarZapatillas.Models
{
    [Table("ZAPASPRACTICA")]
    public class Zapatilla
    {
        [Key]
        [Column("IDPRODUCTO")]
        public int Id { get; set; }
        [Column("NOMBRE")]
        public string Name { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
        [Column("PRECIO")] 
        public int Precio { get; set; }
    }
}
