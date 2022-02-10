using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AlunosApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //verifica se contém dados na tabela, se não existir, ele inclui
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Maria da Penha",
                    Email = "mariapenha@yahoo.com",
                    Idade = 23
                },
                new Aluno
                {
                     Id = 2,
                     Nome = "Manuel Bueno",
                     Email = "manuelbueno@yahoo.com",
                     Idade = 22
                }

                );
        }

    }
}
