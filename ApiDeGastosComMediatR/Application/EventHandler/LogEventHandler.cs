using ApiDeGastosComMediatR.Application.Notifications;
using MediatR;

namespace ApiDeGastosComMediatR.Application.EventHandler
{
    public class LogEventHandler :
                           INotificationHandler<ReceitaCriadaNotification>,
                           INotificationHandler<ReceitaAlteradaNotification>,
                           INotificationHandler<ReceitaExcluidaNotification>,
                           INotificationHandler<ErroNotification>
    {
        public Task Handle(ReceitaCriadaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"CRIACAO: '{notification.Id} - {notification.Descricao} - {notification.Valor} - {notification.Data}'");
            });
        }

        public Task Handle(ReceitaAlteradaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ALTERACAO: '{notification.Id} - {notification.Descricao} - {notification.Valor} - {notification.Data} - {notification.IsEfetivado}'");
            });
        }

        public Task Handle(ReceitaExcluidaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"EXCLUSAO: '{notification.Id} - {notification.IsEfetivado}'");
            });
        }

        public Task Handle(ErroNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERRO: '{notification.Excecao} \n {notification.PilhaErro}'");
            });
        }
    }
}
