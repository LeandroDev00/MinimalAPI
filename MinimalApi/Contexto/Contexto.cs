using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

namespace MinimalApi.Contexto
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) => Database.EnsureCreated();

        public DbSet<Produto> Produtos { get; set; }
    }
}
