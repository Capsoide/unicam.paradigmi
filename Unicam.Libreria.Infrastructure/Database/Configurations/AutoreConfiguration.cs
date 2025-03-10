﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unicam.Libreria.Core;

namespace Unicam.Libreria.Infrastructure.Database.Configurations
{
    public class AutoreConfiguration : IEntityTypeConfiguration<Autore>
    {
        public void Configure(EntityTypeBuilder<Autore> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("IdAutore");
            builder.Property(p => p.Nome).HasColumnName("NomeAutore");
            builder.Property(p => p.Cognome).HasColumnName("CognomeAutore");
        }
    }
}
