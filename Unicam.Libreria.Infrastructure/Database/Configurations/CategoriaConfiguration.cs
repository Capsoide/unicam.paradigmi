using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unicam.Libreria.Core;
using Unicam.Libreria.Core.Entities;

namespace Unicam.Libreria.Infrastructure.Database.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorie");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("IdCategoria");
            builder.Property(p => p.Nome).HasColumnName("NomeCategoria");
            builder.Property(p => p.Nome).HasMaxLength(100);    //nel db ilnomeCategoria ha una lunghezza max du 100 caratteri (mMAPPATURA MANUALE DEL DB)
        }
    }
}
