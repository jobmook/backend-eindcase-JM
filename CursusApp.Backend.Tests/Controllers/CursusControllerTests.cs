using Microsoft.VisualStudio.TestTools.UnitTesting;
using CursusApp.Backend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursusApp.Backend.Interfaces;
using CursusApp.Core.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using CursusApp.Backend.Dtos;

namespace CursusApp.Backend.Controllers.Tests
{
    [TestClass()]
    public class CursusControllerTests
    {
        CursusController _sut;
        Mock<ICursusRepository> _mockCursusRepo;
        Mock<ICursusInstantieRepository> _mockInstantieRepo;

        [TestMethod]
        public async Task GetAll_UseRepoToRetrieveCursussenAsync()
        {
            _mockInstantieRepo = new Mock<ICursusInstantieRepository>();
            _mockCursusRepo = new Mock<ICursusRepository>();
            _mockCursusRepo.Setup(x => x.GetAll()).ReturnsAsync(new List<Cursus> { new(), new(), new() });

            _sut = new CursusController(_mockCursusRepo.Object, _mockInstantieRepo.Object);
            var result = await _sut.GetAll();
            _mockCursusRepo.Verify(x => x.GetAll());
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task Post_NullRequest()
        {
            _mockInstantieRepo = new Mock<ICursusInstantieRepository>();
            _mockCursusRepo = new Mock<ICursusRepository>();
            _sut = new CursusController(_mockCursusRepo.Object, _mockInstantieRepo.Object);
            
            var result = await _sut.Post(null);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task Post_EmptyArray()
        {
            _mockInstantieRepo = new Mock<ICursusInstantieRepository>();
            _mockCursusRepo = new Mock<ICursusRepository>();
            _sut = new CursusController(_mockCursusRepo.Object, _mockInstantieRepo.Object);
            CursusDto[] emptyArray = new CursusDto[] { };

            var result = await _sut.Post(emptyArray);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task Post_Valid_Array_MetGevondenCursusEnInstantie()
        {
            _mockInstantieRepo = new Mock<ICursusInstantieRepository>();
            _mockCursusRepo = new Mock<ICursusRepository>();
            _sut = new CursusController(_mockCursusRepo.Object, _mockInstantieRepo.Object);

            CursusDto testDto = new CursusDto { Duur = 5, Cursuscode = "TEST", Startdatum = "01/01/2022", Titel = "TestDTO" };
            CursusDto[] validArray = new CursusDto[] { testDto };

            _mockCursusRepo.Setup(x => x.GetByCursusCode("TEST"))
                .ReturnsAsync(new Cursus());
            _mockInstantieRepo.Setup(x => x.Get(1, "01/01/2022"))
                .ReturnsAsync(new CursusInstantie());
            var result = await _sut.Post(validArray);

            _mockCursusRepo.Verify(x => x.GetByCursusCode(It.IsAny<string>()));
            _mockInstantieRepo.Verify(x => x.Get(It.IsAny<int>(), It.IsAny<string>()));
            _mockCursusRepo.VerifyNoOtherCalls();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Post_Valid_Array_MetNietGevondenCursusEnInstantie()
        {
            _mockInstantieRepo = new Mock<ICursusInstantieRepository>();
            _mockCursusRepo = new Mock<ICursusRepository>();
            _sut = new CursusController(_mockCursusRepo.Object, _mockInstantieRepo.Object);

            CursusDto testDto = new CursusDto { Duur = 5, Cursuscode = "TEST", Startdatum = "01/01/2022", Titel = "TestDTO" };
            CursusDto[] validArray = new CursusDto[] { testDto };

            _mockCursusRepo.Setup(x => x.GetByCursusCode("TEST"))
                .ReturnsAsync(() => null);
            _mockInstantieRepo.Setup(x => x.Get(1, "01/01/2022"))
                .ReturnsAsync(() => null);
            _mockCursusRepo.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new Cursus());
            _mockInstantieRepo.Setup(x => x.Create(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new CursusInstantie());

            var result = await _sut.Post(validArray);

            _mockCursusRepo.Verify(x => x.GetByCursusCode(It.IsAny<string>()));
            _mockInstantieRepo.Verify(x => x.Get(It.IsAny<int>(), It.IsAny<string>()));
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult)); // hoe check je het object wat wordt mee gegeven?
        }
    }
}