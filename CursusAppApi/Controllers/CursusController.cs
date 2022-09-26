using CursusApp.Backend.Dtos;
using CursusApp.Backend.Interfaces;
using CursusApp.Backend.Repositories;
using CursusApp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursusApp.Backend.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class CursusController : ControllerBase
    {
        private readonly ICursusRepository _cursusRepository;
        private readonly ICursusInstantieRepository _cursusInstantieRepository;

        public CursusController(ICursusRepository cursusRepository, ICursusInstantieRepository cursusInstantieRepository)
        {
            _cursusRepository = cursusRepository;
            _cursusInstantieRepository = cursusInstantieRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Cursus>> GetAll()
        {
            return await _cursusRepository.GetAll();
        }

        [HttpPost]
        public async Task<ActionResult> Post(CursusDto[] cursusDtos)
        {
            if (cursusDtos is null || cursusDtos.Length == 0) return BadRequest();

            int nieuweCursussenToegevoegd = 0;
            int nieuweCursusInstantiesToegevoegd = 0;

            foreach (var cursusDto in cursusDtos)
            {
                var cursus = await _cursusRepository.GetByCursusCode(cursusDto.Cursuscode);
                if (cursus is null) nieuweCursussenToegevoegd++;
                cursus ??= await _cursusRepository.Create(cursusDto.Titel, cursusDto.Cursuscode, cursusDto.Duur);

                var nieuweCurusInstantie = await _cursusInstantieRepository.Get(cursus.Id, cursusDto.Startdatum);
                if (nieuweCurusInstantie is null) nieuweCursusInstantiesToegevoegd++;
                nieuweCurusInstantie ??= await _cursusInstantieRepository.Create(cursus.Id, cursusDto.Startdatum);
            }
            FeedbackDto feedbackResponse = new FeedbackDto();
            feedbackResponse.duplicaten = cursusDtos.Length - nieuweCursusInstantiesToegevoegd;
            feedbackResponse.cursusInstantieToevoeging = nieuweCursusInstantiesToegevoegd;
            feedbackResponse.cursusToevoeging = nieuweCursussenToegevoegd;
            return Ok(feedbackResponse);
        }

        // deprecated
        [HttpGet]
        [Route("remove-all-entries")]
        public async Task<ActionResult> RemoveAll()
        {
            try
            {
                await _cursusRepository.RemoveAll();
                return Ok("Alle cursussen en gerelateerde cursusintanties zijn verwijderd.");
            }
            catch{
                return BadRequest();
            }
            
        }
    }
}
