using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entity.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.DAL.Configuration
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            ToTable("TB_USUARIO");

            HasKey(u => u.Id);

            Property(u => u.Id)
                .HasColumnName("IDUSUARIO")
                .IsRequired();

            Property(u => u.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(50)
                .IsRequired();

            Property(u => u.Sobrenome)
                .HasColumnName("SOBRENOME")
                .HasMaxLength(50)
                .IsRequired();

            Property(u => u.Foto)
                .HasColumnName("FOTO")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
