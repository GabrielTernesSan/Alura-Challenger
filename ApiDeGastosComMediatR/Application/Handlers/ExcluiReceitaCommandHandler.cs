using ApiDeGastosComMediatR.Application.Commands;
using ApiDeGastosComMediatR.Application.Models;
using ApiDeGastosComMediatR.Application.Notifications;
using ApiDeGastosComMediatR.Repositories;
using MediatR;

namespace ApiDeGastosComMediatR.Application.Handlers
{
    public class ExcluiReceitaCommandHandler : IRequestHandler<ExcluiReceitaCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IReceitaRepository _repository;

        public ExcluiReceitaCommandHandler(IMediator mediator, IReceitaRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(ExcluiReceitaCommand request, CancellationToken cancellationToken)
        {

            var receita = await _repository.GetById(p => p.ReceitaId == request.Id);

            try
            {
                await _repository.Delete(receita);

                await _repository.Save();

                await _mediator.Publish(new ReceitaExcluidaNotification { Id = request.Id, IsEfetivado = true });

                return await Task.FromResult("Receita excluida");
            }catch (Exception ex)
            {
                await _mediator.Publish(new ReceitaExcluidaNotification { Id = request.Id, IsEfetivado = true });
                await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da exclusão");
            }
        }
    }
}
