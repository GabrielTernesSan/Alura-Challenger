using ApiDeGastosComMediatR.Application.Models;
using ApiDeGastosComMediatR.Context;

namespace ApiDeGastosComMediatR.Repositories
{
    public class ReceitaRepository : Repository<Receita>, IReceitaRepository
    {
        public ReceitaRepository(AppDbContext context) : base(context)
        {}
    }
}
