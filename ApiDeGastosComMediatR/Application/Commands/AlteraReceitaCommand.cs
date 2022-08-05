using MediatR;

namespace ApiDeGastosComMediatR.Application.Commands
{
    public class AlteraReceitaCommand : IRequest<string>
    {
        public int ReceitaId { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public string Data { get; set; }
    }
}
