
using AlunosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlunosApi.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> ObterAlunos();
        Task<Aluno> ObterALuno(int id);
        Task<IEnumerable<Aluno>> ObterALunosPorNome(string nome);
        Task CriarAluno(Aluno aluno);
        Task AtualizarAluno(Aluno aluno);
        Task DeletarAluno(Aluno aluno);
    }
}
