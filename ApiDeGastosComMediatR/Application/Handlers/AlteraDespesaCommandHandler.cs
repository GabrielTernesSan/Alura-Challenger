using ApiDeGastosComMediatR.Application.Commands;
using ApiDeGastosComMediatR.Application.Models;
using ApiDeGastosComMediatR.Application.Notifications;
using ApiDeGastosComMediatR.Repositories;
using MediatR;

namespace ApiDeGastosComMediatR.Application.Handlers
{
    public class AlteraDespesaCommandHandler : IRequestHandler<AlteraDespesaCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IDespesaRepository _repository;

        public AlteraDespesaCommandHandler(IMediator mediator, IDespesaRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(AlteraDespesaCommand command, CancellationToken cancellationToken)
        {
            var despesa = await _repository.GetById(p => p.DespesaId == command.DespesaId);

            despesa.Descricao = command.Descricao;
            despesa.Valor = command.Valor;
            despesa.Data = command.Data;

            try
            {
                await _repository.Update(despesa);

                await _repository.Save();

                await _mediator.Publish(new DespesaAlteradaNotification { Descricao = despesa.Descricao, Valor = despesa.Valor, Data = despesa.Data });

                return await Task.FromResult("Despesa alterada com sucesso");
            } catch(Exception ex)
            {
                await _mediator.Publish(new DespesaAlteradaNotification { Descricao = despesa.Descricao, Valor = despesa.Valor, Data = despesa.Data });
                await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro ao executar sua requisição");
            }
        }
    }
}
