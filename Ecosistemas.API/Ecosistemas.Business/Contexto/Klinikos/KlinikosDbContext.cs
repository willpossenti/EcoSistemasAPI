using Ecosistemas.Business.Entities;
using Ecosistemas.Business.Entities.Klinikos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Contexto.Klinikos
{
    public class KlinikosDbContext : DbContext
    {
        public KlinikosDbContext(
            DbContextOptions<KlinikosDbContext> options) : base(options)
        {

        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaPaciente> PessoaPacientes { get; set; }

        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }

        public DbSet<Etnia> Etnias { get; set; }
        public DbSet<Raca> Racas { get; set; }
        public DbSet<Justificativa> Justificativas { get; set; }

        public DbSet<Nacionalidade> Nacionalidades { get; set; }

        public DbSet<Sexo> Sexos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
    }
}
