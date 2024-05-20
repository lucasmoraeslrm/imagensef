using Imagens1.Models;
using Microsoft.EntityFrameworkCore;

namespace Imagens1.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) 
        { 
        
        }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}
