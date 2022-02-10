using AlunosApi.Context;
using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Services
{
    public class AlunosService : IAlunoService
    {
        private readonly AppDbContext _context;

        public AlunosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluno>> ObterAlunos()
        {            
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch 
            {

                throw;
            }
        }

        public async Task<IEnumerable<Aluno>> ObterALunosPorNome(string nome)
        {
            try
            {
                IEnumerable<Aluno> alunos;

                if (!string.IsNullOrWhiteSpace(nome))
                {
                    alunos = await _context.Alunos.Where(a => a.Nome.Contains(nome)).ToListAsync();
                }
                else
                {
                    alunos = await ObterAlunos();
                }
                return alunos;


            }
            catch 
            {

                throw;
            }
        }
        public async Task<Aluno> ObterALuno(int id)
        {
            try
            {
                //find async primeiro busca na memoria
                var aluno = await _context.Alunos.FindAsync(id);

                return aluno;
            }
            catch 
            {

                throw;
            }
        }
        public async Task CriarAluno(Aluno aluno)
        {
            try
            {
                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();
            }
            catch 
            {

                throw;
            }
        }
        public async Task AtualizarAluno(Aluno aluno)
        {
            try
            {
                //altera o objeto modificado
                _context.Entry(aluno).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch 
            {

                throw;
            }
        }
        public async Task DeletarAluno(Aluno aluno)
        {
            try
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
            catch
            {

                throw;
            }
        }
    }
}
