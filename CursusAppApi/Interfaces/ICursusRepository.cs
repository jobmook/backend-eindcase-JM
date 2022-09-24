using CursusApp.Core.Models;

namespace CursusApp.Backend.Interfaces
{
    public interface ICursusRepository
    {
        public Task<Cursus?> GetByCursusCode(string code);
        public Task<Cursus> Create(string titel, string cursuscode, int duur);
        public Task<List<Cursus>> GetAll();
        public Task RemoveAll();

    }
}
