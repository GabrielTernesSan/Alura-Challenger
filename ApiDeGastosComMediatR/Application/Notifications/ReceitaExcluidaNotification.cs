using MediatR;

namespace ApiDeGastosComMediatR.Application.Notifications
{
    public class ReceitaExcluidaNotification : INotification
    {
        public int Id { get; set; }
        public bool IsEfetivado { get; set; }
    }
}
