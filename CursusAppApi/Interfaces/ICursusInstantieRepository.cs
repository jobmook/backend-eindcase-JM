using CursusApp.Core.Models;

namespace CursusApp.Backend.Interfaces
{
    public interface ICursusInstantieRepository
    {
        Task<CursusInstantie> Create(int cursusId, string startdatum);
        Task<CursusInstantie> Get(int cursusId, string startdatum);
    }
}