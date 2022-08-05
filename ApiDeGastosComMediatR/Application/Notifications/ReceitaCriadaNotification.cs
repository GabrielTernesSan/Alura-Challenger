using MediatR;

namespace ApiDeGastosComMediatR.Application.Notifications
{
    public class ReceitaCriadaNotification : INotification
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public string Data { get; set; }
    }
}
