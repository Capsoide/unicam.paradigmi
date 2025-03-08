using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unicam.Libreria.Core;
using Unicam.Libreria.Core.Entities;
using Unicam.Libreria.Infrastructure.Database.Configurations;

namespace Unicam.Libreria.Infrastructure.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        
        {  }
        //keyword virtual = overridabile, questo serve se si abilita il lazy loading, perchè dovrà andare ad effettuare la modifica di questa classe con un'altra classe proxy (classe che si metterà in mezzo)
        //che quando interrogata effettuerà la query (i dati non li carica prima ma a bisogno) 
        
        public virtual DbSet<Autore> Autori { get; set; }   
        public virtual DbSet<Libro> Libri { get; set; }
        public virtual DbSet<Categoria> Categorie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)  //metodo chiamato quando il modello (DbContext) viene creato
        {
            base.OnModelCreating(modelBuilder);     //assicura che la configurazione base del modello venga eseguita prima di applicare personalizzazioni
            //modelBuilder.ApplyConfiguration(new AutoreConfiguration());   //imposta id come chiave primaria per la tabella autore
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);  //applica tutte le configurazioni definite in questo assembly
            
         /*
         //mappatura dei campi alle colonne del db: associa le proprietà della classe ai nomi dei campi nel db
            modelBuilder.Entity<Autore>().Property(p => p.Id).HasColumnName("IdAutore"); //p.Id → colonna "IdAutore"
            modelBuilder.Entity<Autore>().Property(p => p.Nome).HasColumnName("NomeAutore"); //p.Nome → colonna "NomeAutore"
            modelBuilder.Entity<Autore>().Property(p => p.Cognome).HasColumnName("CognomeAutore");   //p.Cognome → colonna "CognomeAutore"
         */
        }
    }
}
