using MediatR;

namespace ApiDeGastosComMediatR.Application.Notifications
{
    public class DespesaExcluidaNotification : INotification
    {
        public int Id { get; set; }
        public bool IsEfetivado { get; set; }
    }
}
