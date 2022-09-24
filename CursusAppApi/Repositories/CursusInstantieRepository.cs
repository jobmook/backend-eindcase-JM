using CursusApp.Backend.DataAccess;
using CursusApp.Backend.Interfaces;
using CursusApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CursusApp.Backend.Repositories
{
    public class CursusInstantieRepository : ICursusInstantieRepository
    {
        private readonly CursusDbContext _context;

        public CursusInstantieRepository(CursusDbContext context)
        {
            _context = context;
        }

        public async Task<CursusInstantie> Create(int cursusId, string startdatum)
        {
            CursusInstantie newCursusInstantie = new CursusInstantie { Startdatum = DateTime.Parse(startdatum).Date, CursusId = cursusId};
            _context.CursusInstanties.Add(newCursusInstantie);
            await _context.SaveChangesAsync();
            return newCursusInstantie;
        }

        public async Task<CursusInstantie> Get(int cursusId, string startdatum)
        {
            var parsedDate = DateTime.Parse(startdatum).Date;
            return await _context.CursusInstanties.FirstOrDefaultAsync(x => x.Startdatum == parsedDate && x.CursusId == cursusId);
        }
    }
}
