using CursusApp.Backend.DataAccess;
using CursusApp.Backend.Interfaces;
using CursusApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CursusApp.Backend.Repositories
{
    public class CursusRepository : ICursusRepository
    {
        private readonly CursusDbContext _context;

        public CursusRepository(CursusDbContext context)
        {
            _context = context;
        }

        public async Task<Cursus?> GetByCursusCode(string code)
        {
            return await _context.Cursussen.SingleOrDefaultAsync(c => c.Code == code);
        }

        public async Task<Cursus> Create(string titel, string cursuscode, int duur)
        {
            Cursus nieuweCursus = new Cursus();
            nieuweCursus.Duur = duur;
            nieuweCursus.Titel = titel;
            nieuweCursus.Code = cursuscode;
            _context.Cursussen.Add(nieuweCursus);
            await _context.SaveChangesAsync();
            return nieuweCursus;
        }

        public async Task<List<Cursus>> GetAll()
        {
            return await _context.Cursussen.Include(x => x.CursusInstanties).ToListAsync();
        }

        public async Task RemoveAll()
        {
            var cursussen = await _context.Cursussen.Include(x => x.CursusInstanties).ToListAsync();
            _context.RemoveRange(cursussen);
            await _context.SaveChangesAsync();
        }
    }
}