using ApiDeGastosComMediatR.Application.Models;
using ApiDeGastosComMediatR.Context;

namespace ApiDeGastosComMediatR.Repositories
{
    public class DespesasRepository : Repository<Despesa>, IDespesaRepository
    {
        public DespesasRepository(AppDbContext context) : base(context)
        {}
    }
}
