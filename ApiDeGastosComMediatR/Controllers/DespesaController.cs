using ApiDeGastosComMediatR.Application.Commands;
using ApiDeGastosComMediatR.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiDeGastosComMediatR.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DespesaController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IDespesaRepository _repository;

        public DespesaController(IMediator mediator, IDespesaRepository repository)
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
            var despesa = await _repository.GetById(p => p.DespesaId == id);

            if (despesa == null)
            {
                return NotFound();
            }

            return Ok(despesa);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CadastraDespesaCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(AlteraDespesaCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var receita = await _repository.GetById(p => p.DespesaId == id);

            var obj = new ExcluiReceitaCommand { Id = receita.DespesaId };
            var response = await _mediator.Send(obj);
            return Ok(response);
        }
    }
}
