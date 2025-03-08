using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Core.Entities;
namespace Unicam.Libreria.Core

{
    [Table("Autori")]
    public class Autore
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public virtual ICollection<Libro> Libri { get; set; } = new HashSet<Libro>();  
    }
}

/*C'E' LA POSSIBILITA' DI UTILIZZARE LE ANNOTAZIONI, GUARDARE IL FILE MyDbContext (parte commentata) OPPURE AutoreConfiguration PER NUOVA VERSIONE
 * 
 * [Table("Autori")]
    public class AutoreConAnnotazioni
    {
        [Key]
        [Column("IdAutore")]
        public int Id { get; set; }
        [Column("NomeAutore")]
        public string Nome { get; set; } = string.Empty;
        [Column("CognomeAutore")]
        public string Cognome { get; set; } = string.Empty;
    } */
