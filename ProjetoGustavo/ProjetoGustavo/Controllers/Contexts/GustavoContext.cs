using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjetoGustavo.Controllers.Domains;

#nullable disable

namespace ProjetoGustavo.Controllers.Contexts
{
    public partial class GustavoContext : DbContext
    {
        public GustavoContext()
        {
        }

        public GustavoContext(DbContextOptions<GustavoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263
                // Usar o IP abaixo para executar os testes devido a variacao de endereco de acesso
                //optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1433; Initial Catalog=TesteGustavo; Integrated Security=False; user ID=sa; password=Pr0j3t0gust4v0;");

                // Para utilizar o Docker utilizar a string de conexão abaixo, Caso ainda tenha problemas ao se conectar, utilizar:
                // docker ps -a para identificar o container do banco de dados
                // docker inspect para verificar o endereco para a conexao

                optionsBuilder.UseSqlServer("Data Source=172.17.0.2; Initial Catalog=TesteGustavo; Integrated Security=False; user ID=sa; password=Pr0j3t0gust4v0;");
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.Surname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("surname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
