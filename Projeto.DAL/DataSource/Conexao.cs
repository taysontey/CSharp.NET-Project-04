using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Projeto.Entity.Entities;
using Projeto.DAL.Configuration;
using System.ComponentModel.DataAnnotations.Schema;


namespace Projeto.DAL.DataSource
{
    public class Conexao : IdentityDbContext<Usuario>
    {
        public Conexao()
            : base(ConfigurationManager.ConnectionStrings["projeto"].ConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoriaConfiguration());
            modelBuilder.Configurations.Add(new FornecedorConfiguration());
            modelBuilder.Configurations.Add(new ProdutoConfiguration());

            modelBuilder.Configurations.Add(new UsuarioConfiguration()); //Entidade do usuário

            base.OnModelCreating(modelBuilder);

            //personalizar as entidades do Identity
            modelBuilder.Entity<IdentityUser>().ToTable("TB_USUARIO")
                .Property(u => u.Id)
                .HasColumnName("IDUSUARIO");

            modelBuilder.Entity<IdentityUser>().ToTable("TB_USUARIO")
                .Property(u => u.UserName)
                .HasColumnName("LOGIN")
                .HasMaxLength(50);

            modelBuilder.Entity<IdentityUser>().ToTable("TB_USUARIO")
                .Property(u => u.PasswordHash)
                .HasColumnName("SENHA");

            //sobrescrever a entidade IdentityUser pela entidade Usuario
            modelBuilder.Entity<Usuario>().ToTable("TB_USUARIO")
                .Property(u => u.Id)
                .HasColumnName("IDUSUARIO");

            modelBuilder.Entity<IdentityRole>().ToTable("TB_PERFIL");
            modelBuilder.Entity<IdentityUserRole>().ToTable("TB_USUARIOPERFIL");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("TB_USUARIOLOGIN");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("TB_USUARIOCLAIM");
        }

        public DbSet<Categoria> Categoria;
        public DbSet<Fornecedor> Fornecedor;
        public DbSet<Produto> Produto;
    }
}
