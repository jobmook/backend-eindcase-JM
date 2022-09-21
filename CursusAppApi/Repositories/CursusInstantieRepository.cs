using CursusApp.Backend.DataAccess;
using CursusApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CursusApp.Backend.Repositories
{
    public class CursusInstantieRepository
    {
        private readonly CursusDbContext _context;

        public CursusInstantieRepository(CursusDbContext context)
        {
            _context = context;
        }

        public async Task<CursusInstantie> Create(string startdatum, int cursusId)
        {
            CursusInstantie newCursusInstantie = new CursusInstantie { Startdatum = startdatum, CursusId = cursusId};
            _context.CursusInstanties.Add(newCursusInstantie);
            await _context.SaveChangesAsync();
            return newCursusInstantie;
        }

      

    }
}
