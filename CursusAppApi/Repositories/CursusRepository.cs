using CursusApp.Backend.DataAccess;
using CursusApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CursusApp.Backend.Repositories
{
    public class CursusRepository
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
            List<Cursus> cursussen =  await _context.Cursussen.Include(x => x.CursusInstanties).ToListAsync();
            foreach (var item in cursussen)
            {
                Console.WriteLine(item);
            }
            return cursussen;
        }
    }
}