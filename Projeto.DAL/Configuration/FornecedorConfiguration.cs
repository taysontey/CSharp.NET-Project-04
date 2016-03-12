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
    public class FornecedorConfiguration : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorConfiguration()
        {
            ToTable("TB_FORNECEDOR");

            HasKey(f => f.IdFornecedor);

            Property(f => f.IdFornecedor)
                .HasColumnName("IDFORNECEDOR")
                .IsRequired();

            Property(f => f.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(50)
                .IsRequired();

            Property(f => f.CNPJ)
                .HasColumnName("CNPJ")
                .HasMaxLength(14)
                .IsRequired();
        }
    }
}
