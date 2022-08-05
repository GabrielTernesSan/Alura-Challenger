using ApiDeGastosComMediatR.Application.Commands;
using ApiDeGastosComMediatR.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiDeGastosComMediatR.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ReceitaController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IReceitaRepository _repository;

        public ReceitaController(IMediator mediator, IReceitaRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.Get());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var receita = await _repository.GetById(p => p.ReceitaId == id);

            if(receita == null)
            {
                return NotFound();
            }

            return Ok(receita);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CadastraReceitaCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(AlteraReceitaCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var receita = await _repository.GetById(p => p.ReceitaId == id);
            
            var obj = new ExcluiReceitaCommand { Id = receita.ReceitaId };
            var response = await _mediator.Send(obj);
            return Ok(response);
        }
    }
}
