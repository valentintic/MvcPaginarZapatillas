using Microsoft.EntityFrameworkCore;
using MvcPaginarZapatillas.Models;

namespace MvcPaginarZapatillas.Data
{
    public class ZapatillasContext: DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options) : base(options) {}

        public DbSet<Zapatilla> Zapatilla { get; set; }
        public DbSet<ImagenZapatilla> ImagenZapatilla { get; set; }
    }
}
