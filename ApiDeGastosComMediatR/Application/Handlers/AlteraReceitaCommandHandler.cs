using ApiDeGastosComMediatR.Application.Commands;
using ApiDeGastosComMediatR.Application.Models;
using ApiDeGastosComMediatR.Application.Notifications;
using ApiDeGastosComMediatR.Repositories;
using MediatR;

namespace ApiDeGastosComMediatR.Application.Handlers
{
    public class AlteraReceitaCommandHandler : IRequestHandler<AlteraReceitaCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IReceitaRepository _repository;

        public AlteraReceitaCommandHandler(IMediator mediator, IReceitaRepository repostiory)
        {
            _mediator = mediator;
            _repository = repostiory;
        }

        public async Task<string> Handle(AlteraReceitaCommand command, CancellationToken cancellationToken)
        {
            var receita = await _repository.GetById(p => p.ReceitaId == command.ReceitaId);

            receita.Descricao = command.Descricao;
            receita.Valor = command.Valor;
            receita.Data = command.Data;

            try
            {
                await _repository.Update(receita);

                await _repository.Save();

                await _mediator.Publish(new ReceitaAlteradaNotification { Id = receita.ReceitaId, Descricao = receita.Descricao, Valor = receita.Valor, Data = receita.Data, IsEfetivado = true});

                return await Task.FromResult("Receita alterada com sucesso");
            }catch (Exception ex)
            {
                await _mediator.Publish(new ReceitaAlteradaNotification { Id = receita.ReceitaId, Descricao = receita.Descricao, Data = receita.Data });
                await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da criação");
            }
        }
    }
}
