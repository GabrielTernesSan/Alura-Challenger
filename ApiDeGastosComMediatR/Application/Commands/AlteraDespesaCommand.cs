using MediatR;

namespace ApiDeGastosComMediatR.Application.Commands
{
    public class AlteraDespesaCommand : IRequest<string>
    {
        public int DespesaId { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public string Data { get; set; }
    }
}
