using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entity.Entities;

namespace Projeto.DAL.Configuration
{
    public class CategoriaConfiguration : EntityTypeConfiguration<Categoria>
    {
        public CategoriaConfiguration()
        {
            ToTable("TB_CATEGORIA");

            HasKey(c => c.IdCategoria);

            Property(c => c.IdCategoria)
                .HasColumnName("IDCATEGORIA")
                .IsRequired();

            Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(25)
                .IsRequired();

            Property(c => c.Descricao)
                .HasColumnName("DESCRICAO");                    
        }
    }
}
