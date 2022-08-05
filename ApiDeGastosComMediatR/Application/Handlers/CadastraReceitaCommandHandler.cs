using ApiDeGastosComMediatR.Application.Commands;
using ApiDeGastosComMediatR.Application.Models;
using ApiDeGastosComMediatR.Application.Notifications;
using ApiDeGastosComMediatR.Repositories;
using MediatR;

namespace ApiDeGastosComMediatR.Application.Handlers
{
    public class CadastraReceitaCommandHandler : IRequestHandler<CadastraReceitaCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IReceitaRepository _repository;

        public CadastraReceitaCommandHandler(IMediator mediator, IReceitaRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(CadastraReceitaCommand request, CancellationToken cancellationToken)
        {
            var receita = new Receita { Descricao = request.Descricao, Valor = request.Valor, Data = request.Data };
            try
            {
                await _repository.Add(receita);

                await _repository.Save();
               
                await _mediator.Publish(new ReceitaCriadaNotification { Id = receita.ReceitaId, Descricao = receita.Descricao, Data = receita.Data });

                return await Task.FromResult("Receita criada com sucesso");
            } catch(Exception ex)
            {
                await _mediator.Publish(new ReceitaCriadaNotification { Id = receita.ReceitaId, Descricao = receita.Descricao, Data = receita.Data });
                await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da criação");
            }
        }
    }
}
