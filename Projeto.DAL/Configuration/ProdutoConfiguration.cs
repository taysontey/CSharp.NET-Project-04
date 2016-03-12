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
    public class ProdutoConfiguration : EntityTypeConfiguration<Produto>
    {
        public ProdutoConfiguration()
        {
            ToTable("TB_PRODUTO");

            HasKey(p => p.IdProduto);

            Property(p => p.IdProduto)
                .HasColumnName("IDPRODUTO")
                .IsRequired();

            Property(p => p.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(50)
                .IsRequired();

            Property(p => p.Preco)
                .HasColumnName("PRECO")
                .IsRequired();

            Property(p => p.Quantidade)
                .HasColumnName("QUANTIDADE")
                .IsRequired();

            #region Relacionamentos

            HasRequired(p => p.Fornecedor)
                .WithMany(f => f.Produtos)
                .HasForeignKey(p => p.IdFornecedor);

            HasRequired(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.IdCategoria);

            #endregion
        }
    }
}
