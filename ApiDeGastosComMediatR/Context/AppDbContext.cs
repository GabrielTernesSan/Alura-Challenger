using ApiDeGastosComMediatR.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDeGastosComMediatR.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Receita> Receitas { get; set; }

        public DbSet<Despesa> Despesas { get; set; }
    }
}
