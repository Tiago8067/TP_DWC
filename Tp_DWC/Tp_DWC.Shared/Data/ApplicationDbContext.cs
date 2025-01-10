using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_DWC.Shared.Models;
using Tp_DWC.Shared.Models.EstadoModels;

namespace Tp_DWC.Shared.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento Estado -> MudancaEstado (EstadoAtual)
            modelBuilder.Entity<MudancaEstado>()
                .HasOne(me => me.EstadoAtual)
                .WithMany(e => e.MudancasEstado)
                .HasForeignKey(me => me.EstadoAtualId)
                .OnDelete(DeleteBehavior.Restrict); // Evita deleções em cascata

            // Relacionamento Estado -> MudancaEstado (NovoEstado)
            modelBuilder.Entity<MudancaEstado>()
                .HasOne(me => me.NovoEstado)
                .WithMany()
                .HasForeignKey(me => me.NovoEstadoId)
                .OnDelete(DeleteBehavior.Restrict); // Evita deleções em cascata

            // Relacionamento Estado -> Assistencia
            modelBuilder.Entity<Assistencia>()
                .HasOne(a => a.Estado)
                .WithMany(e => e.Assistencias)
                .HasForeignKey(a => a.EstadoId)
                .OnDelete(DeleteBehavior.Restrict); // Evita deleções em cascata

            //preencher dados do Estado
            modelBuilder.Entity<Estado>().HasData(
                new Estado { PK_Estado = Guid.Parse("11111111-1111-1111-1111-111111111111"), Descricao = "nova" },
                new Estado { PK_Estado = Guid.Parse("22222222-2222-2222-2222-222222222222"), Descricao = "em execução" },
                new Estado { PK_Estado = Guid.Parse("33333333-3333-3333-3333-333333333333"), Descricao = "à espera de material" },
                new Estado { PK_Estado = Guid.Parse("44444444-4444-4444-4444-444444444444"), Descricao = "resolvido" },
                new Estado { PK_Estado = Guid.Parse("55555555-5555-5555-5555-555555555555"), Descricao = "para entregar" },
                new Estado { PK_Estado = Guid.Parse("66666666-6666-6666-6666-666666666666"), Descricao = "entregue" },
                new Estado { PK_Estado = Guid.Parse("77777777-7777-7777-7777-777777777777"), Descricao = "pago" }
            );

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Morada> Moradas { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Assistencia> Assistencias { get; set; }
        public DbSet<RegistoFotografico> RegistoFotograficos { get; set; }
        public DbSet<RegistoMaoDeObra> RegistoMaoDeObras { get; set; }
        public DbSet<RegistoMaterial> RegistoMaterials { get; set; }
        public DbSet<MudancaEstado> MudancasEstado { get; set; }
        public DbSet<Estado> Estados { get; set; }
    }
}
