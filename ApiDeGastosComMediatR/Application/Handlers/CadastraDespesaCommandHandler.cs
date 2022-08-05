using ApiDeGastosComMediatR.Application.Commands;
using ApiDeGastosComMediatR.Application.Models;
using ApiDeGastosComMediatR.Application.Notifications;
using ApiDeGastosComMediatR.Repositories;
using MediatR;

namespace ApiDeGastosComMediatR.Application.Handlers
{
    public class CadastraDespesaCommandHandler : IRequestHandler<CadastraDespesaCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IDespesaRepository _repository;

        public CadastraDespesaCommandHandler(IMediator mediator, IDespesaRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(CadastraDespesaCommand request, CancellationToken cancellationToken)
        {
            var despesa = new Despesa { Descricao = request.Descricao, Valor = request.Valor, Data = request.Data };

            try
            {
                await _repository.Add(despesa);

                await _repository.Save();

                await _mediator.Publish(new DespesaCriadaNotification { Descricao = request.Descricao, Valor = request.Valor, Data = request.Data });

                return await Task.FromResult("Despesa criada com sucesso");

            } catch (Exception ex)
            {
                await _mediator.Publish(new DespesaCriadaNotification { Descricao = request.Descricao, Valor = request.Valor, Data = request.Data });
                await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro ao fazer sua requisição");
            }
        }
    }
}
