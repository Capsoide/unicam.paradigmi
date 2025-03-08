using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unicam.Libreria.Core.Entities;
using Unicam.Libreria.Infrastructure.Database;

namespace Unicam.Libreria.Console
{
    public class MainService
    {
        private MyDbContext _context;
        public MainService(MyDbContext context)
        {
            _context = context;
        }

        private void JoinManuale()
        {
            var result = (from l in _context.Libri
                          join a in _context.Autori on l.IdAutore equals a.Id
                          select new Libro()
                          {
                              Id = l.Id,
                              Autore = a,
                              IdAutore = l.IdAutore,
                              IdCategoria = l.IdCategoria,
                              Titolo = l.Titolo,
                              Descrizione = l.Descrizione,
                          }).ToList();
        }

        private void EagerLoading()
        {
            var libriConAutori = _context.Libri
                                .AsNoTracking()
                                .Include(i => i.Autore)
                                .ToList();
        }

        private async Task ExplicitLoadingAsync()
        {
            var libri = await _context.Libri
                .AsNoTracking()
                .ToListAsync();
            foreach (var libro in libri)
            {
                _context.Entry(libro).Reference(x => x.Autore).Load();
            }
        }

        private async Task LazyLoadingAsync()
        {
            var libri = await _context.Libri
                .AsNoTracking()
                .ToListAsync();
            foreach (var libro in libri)
            {
                _context.Entry(libro).Reference(x => x.Autore).Load();
                System.Console.WriteLine(libro.Autore.Nome);
            }
        }
        public async Task ExecuteAsync()
        {

            //concatenazione di query  

            /*var query = _context.Libri
                        .Where(w => w.IdAutore == 1);

            query = query.Where(w => w.IdCategoria == 2);

            query.ToList(); //lista di libri con id autore 1 e id categoria 2 */

            await LazyLoadingAsync();
            await ExplicitLoadingAsync();

            await AggiungiLibroAsync();
            await _context.SaveChangesAsync();

            /*
            var autori = _context.Autori.AsNoTracking().ToList();   //per non tracciare autori e quindi non avere dentro libri gli autori utilizzare var autori = _context.Autori.AsNoTracking().ToList();
            var categorie = _context.Categorie.AsNoTracking().ToList(); //per non tracciare categorie e quindi non avere dentro libri le categorie utilizzare var categorie = _context.Categorie.AsNoTracking().ToList();
            var libri = _context.Libri.AsNoTracking().ToList();

            var libroDaModificare = libri.Where(w => w.Id == 2).First();

            var trans = _context.Database.BeginTransaction();
            AggiungiLibro();
            EditLibroGiaTracciato(libroDaModificare);
            EditDiAlcuneProprieta();
            DeleteLibro();
            _context.SaveChanges();

            //System.Console.WriteLine("Stato dell'oggetto Nuovo Libro: " + _context.Entry(nuovoLibro).State);
            //System.Console.WriteLine("Stato dell'oggetto Old Libro: " + _context.Entry(oldLibro).State);
            /*var nuovoLibro = new Libro();
            nuovoLibro.Id = 1;
            _context.Libri.Add(nuovoLibro); */
        }

        private async Task AggiungiLibroAsync()
        {
            var libro = new Libro();
            libro.IdAutore = 1;
            libro.IdCategoria = 1;
            libro.Titolo = "Aggiunta libro";
            libro.Descrizione = "Descrizione TEST libro";

           await _context.Libri.AddAsync(libro);
            //il comando add sul Db Set è analogo ad effettuare l'operazione quii sotto:
            //_context.Entry(libro).State = EntityState.Added;
        }

        private void EditLibroGiaTracciato(Libro libro)
        {
            libro.Titolo = "Titolo del Libro modificato";
        }

        private void EditDiAlcuneProprieta()
        {
            var libroDaModificare = new Libro();
            libroDaModificare.Id = 3;
            libroDaModificare.Descrizione = "DESCRIZIONE MODIFICATA";

            var entry = _context.Entry(libroDaModificare);
            entry.Property(p => p.Descrizione).IsModified = true;
        }

        private void DeleteLibro()
        {
            var libroDaEliminare = new Libro();
            libroDaEliminare.Id = 4;

            _context.Entry(libroDaEliminare).State = EntityState.Deleted;
        }

    }
}
