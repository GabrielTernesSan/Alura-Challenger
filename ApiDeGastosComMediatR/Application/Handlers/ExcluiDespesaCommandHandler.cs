using ApiDeGastosComMediatR.Application.Commands;
using ApiDeGastosComMediatR.Application.Notifications;
using ApiDeGastosComMediatR.Repositories;
using MediatR;

namespace ApiDeGastosComMediatR.Application.Handlers
{
    public class ExcluiDespesaCommandhandler : IRequestHandler<ExcluiReceitaCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IDespesaRepository _repository;

        public ExcluiDespesaCommandhandler(IMediator mediator, IDespesaRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(ExcluiReceitaCommand request, CancellationToken cancellationToken)
        {
            var receita = await _repository.GetById(p => p.DespesaId == request.Id);

            try
            {
                await _repository.Delete(receita);

                await _repository.Save();

                await _mediator.Publish(new DespesaExcluidaNotification { Id = request.Id, IsEfetivado = true });

                return await Task.FromResult("Despesa alterada com sucesso");
            }catch (Exception ex)
            {
                await _mediator.Publish(new DespesaExcluidaNotification { Id = request.Id, IsEfetivado = true });
                await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro ao fazer sua requisição");
            }
        }
    }
}
