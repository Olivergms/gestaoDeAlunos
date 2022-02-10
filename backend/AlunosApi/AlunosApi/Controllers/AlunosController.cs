using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }
        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> ObterAlunos()
        {
            try
            {
                var alunos = await _alunoService.ObterAlunos();
                return Ok(alunos);
            }
            catch 
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter lista de alunos");
            }
        }

        [HttpGet("alunopornome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> ObterAlunosPorNome([FromQuery]string nome)
        {
            try
            {
                var alunos = await _alunoService.ObterALunosPorNome(nome);
                if (alunos.Count() == 0) return NotFound($"Não existem alunos com o critério {nome}");

                return Ok(alunos);
            }
            catch
            {

                return BadRequest("Requisição inválida");
            }
        }

        [HttpGet("{id:int}", Name = "ObterAluno")]
        public async Task<ActionResult<Aluno>> ObterAlunoPorId(int id)
        {
            try
            {
                var aluno = await _alunoService.ObterAluno(id);

                if (aluno == null) return NotFound($"Não existe aluno com o id={id}");

                return Ok(aluno);
            }
            catch 
            {

                return BadRequest("Requisição inválida");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CriarAluno(Aluno aluno)
        {
            try
            {
                await _alunoService.CriarAluno(aluno);

                return CreatedAtRoute("ObterAluno", new { id = aluno.Id }, aluno);
            }
            catch
            {

                return BadRequest("Requisição inválida");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> AtualizarAluno(int id, [FromBody]Aluno aluno)
        {
            try
            {
                if(aluno.Id == id)
                {
                    await _alunoService.AtualizarAluno(aluno);
                    return Ok($"Aluno com id:{id} foi atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch
            {

                return BadRequest("Requisição inválida");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> RemoverAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.ObterAluno(id);

                if(aluno != null)
                {
                    await _alunoService.DeletarAluno(aluno);
                    return Ok($"Aluno de id={id} foi excluido com sucesso");
                }
                else
                {
                    return NotFound($"Aluno com o id={id} não encontrado");
                }
            }
            catch
            {

                return BadRequest("Requisição inválida");
            }
        }


    }
}
