using MediatR;

namespace ApiDeGastosComMediatR.Application.Commands
{
    public class CadastraReceitaCommand : IRequest<string>
    {
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public string Data { get; set; }
    }
}
