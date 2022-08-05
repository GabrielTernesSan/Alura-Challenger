using MediatR;

namespace ApiDeGastosComMediatR.Application.Commands
{
    public class ExcluiReceitaCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
