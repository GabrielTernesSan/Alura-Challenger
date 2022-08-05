using MediatR;

namespace ApiDeGastosComMediatR.Application.Commands
{
    public class ExcluiDespesaCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
