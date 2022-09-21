using CursusApp.Backend.Dtos;
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
        private readonly CursusRepository _cursusRepository;
        private readonly CursusInstantieRepository _cursusInstantieRepository;

        public CursusController(CursusRepository cursusRepository, CursusInstantieRepository cursusInstantieRepository)
        {
            _cursusRepository = cursusRepository;
            _cursusInstantieRepository = cursusInstantieRepository;
        }

        //[HttpPost]
        //public async Task<ActionResult> Post(CursusDto cursusDto)
        //{
        //    if(cursusDto == null)
        //    {
        //        return BadRequest();
        //    } else
        //    {
        //        var nieuweCursus = await _cursusRepository.Create(cursusDto.Titel, cursusDto.Cursuscode, cursusDto.Duur);
        //        Console.WriteLine("een nieuwe cursus gemaakt!");
        //        var nieuweCursusInstantie = await _cursusInstantieRepository.Create(cursusDto.Startdatum, nieuweCursus.Id);
        //        Console.WriteLine("Een nieuwe cursusinstantie gemaakt!");
        //        return Ok();
        //    }
        //}

        [HttpGet]
        public async Task<IEnumerable<Cursus>> GetAll()
        {
            return await _cursusRepository.GetAll();
        }

        [HttpPost]
        public async Task<ActionResult> Post(CursusDto[] cursusDtos)
        {
            if (cursusDtos == null) return BadRequest();

            int nieuweCursussenToegevoegd = 0;
            int nieuweCursusInstantiesToegevoegd = 0;

            foreach (var cursusDto in cursusDtos)
            {
                var cursus = await _cursusRepository.GetByCursusCode(cursusDto.Cursuscode);
                if (cursus == null) nieuweCursussenToegevoegd++;
                cursus ??= await _cursusRepository.Create(cursusDto.Titel, cursusDto.Cursuscode, cursusDto.Duur);

                nieuweCursusInstantiesToegevoegd++;
                var nieuweCursusInstantie = await _cursusInstantieRepository.Create(cursusDto.Startdatum, cursus.Id);
            }
            FeedbackDto feedbackResponse = new FeedbackDto();
            feedbackResponse.duplicaten = 0;
            feedbackResponse.cursusInstantieToevoeging = nieuweCursusInstantiesToegevoegd;
            feedbackResponse.cursusToevoeging = nieuweCursussenToegevoegd;

            return Ok(feedbackResponse);
        }
    }
}
