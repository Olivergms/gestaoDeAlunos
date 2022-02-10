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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Aluno>> ObterAlunoPorId(int id)
        {
            try
            {
                var aluno = await _alunoService.ObterALuno(id);

                if (aluno == null) return NotFound($"Não existe aluno com o id={id}");

                return Ok(aluno);
            }
            catch 
            {

                throw;
            }
        }
    }
}
