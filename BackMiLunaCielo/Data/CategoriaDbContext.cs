using BackMiLunaCielo.Models;
using Microsoft.EntityFrameworkCore;

namespace BackMiLunaCielo.Data
{
    public class CategoriaDbContext : DbContext
    {
        //Se debe mapear todos los modelos aqui 
        public CategoriaDbContext(DbContextOptions<CategoriaDbContext> options):base(options)
        {

        }
        //Todas los modelos deben estar aqui
        public DbSet<Categoria> Categoria { get; set; } //Mapeado el primer modelo

        public DbSet<Usuario> Usuario { get; set; } //Mapeado el primer modelo

    }
}
