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
    }
}
