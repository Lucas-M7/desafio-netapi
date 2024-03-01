using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_netapi.Context;
using desafio_netapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace desafio_netapi.Controllers
{

    [ApiController]
    [Route("controller")]
    public class TarefaController : ControllerBase
    {

        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CriarTarefa(TarefaModel tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterTarefaPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpGet("{id}")]
        public IActionResult ObterTarefaPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            return Ok();
        }

        [HttpGet("ObterTarefaPorTitulo")]
        public IActionResult ObterTarefaPorTitulo(string titulo)
        {
            var tarefa = _context.Tarefas.Find(titulo);

            if (tarefa == null)
                return NotFound();

            return Ok();
        }

        [HttpGet("ObterTarefaPorData")]
        public IActionResult ObterTarefaPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterTarefaPorStatus")]
        public IActionResult ObterTarefaPorStatus(EnumStatusTarefa status)
        {
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarTarefa(int id, TarefaModel tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Update(tarefa);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            _context.Remove(tarefaBanco);
            _context.SaveChanges();
            return Ok();
        }
    }
}