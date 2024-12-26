using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System_Erp.Model;

namespace System_Erp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        public DbSet<SolicitacaoDeCargo> SolicitacoesDeCargos { get; set; }
        public DbSet<SolicitacaoEspecialidadeMedica> SolicitacoesEspecialidadeMedica { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsuarioModel>()
                .Property(u => u.CargoDoUsuario)
                .HasConversion<string>();

            modelBuilder.Entity<SolicitacaoDeCargo>()
                .Property(s => s.Status)
                .HasConversion<string>();

            modelBuilder.Entity<SolicitacaoEspecialidadeMedica>()
                .Property(s => s.Status)
                .HasConversion<string>();

             modelBuilder.Entity<Agendamento>()
                .HasIndex(a => a.PacienteId)
                .HasDatabaseName("IX_Agendamentos_PacienteId");

            modelBuilder.Entity<Agendamento>()
                .HasIndex(a => a.MedicoId)
                .HasDatabaseName("IX_Agendamentos_MedicoId");
                
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Paciente) 
                .WithMany()  
                .HasForeignKey(a => a.PacienteId)
                .OnDelete(DeleteBehavior.NoAction);   

             modelBuilder.Entity<Agendamento>()
                .Property(s => s.Status)
                .HasConversion<string>();    
        }

    }
    
}