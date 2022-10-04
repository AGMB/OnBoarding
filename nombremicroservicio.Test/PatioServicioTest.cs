using MockQueryable.NSubstitute;
using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.Entidades;
using nombremicroservicio.Infrastructure.Services;
using nombremicroservicio.Repository.Repositorios;
using nombremicroservicio.Test.DbContext;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nombremicroservicio.Test
{
    [TestFixture]
    class PatioServicioTest: MockCreditoAutoDbContext
    {
        [Test]
        public async Task ObtenerPatiosAsync_ExistePatios_RetornaListaPatioss()
        {
            //Arrange
            IPatioRepository _patioRepository = Substitute.For<IPatioRepository>();
            ISolicitudCreditoRepository _solicitudCredito = Substitute.For<ISolicitudCreditoRepository>();
            var target = new PatioServicio(_patioRepository, _solicitudCredito);
            var listaPatios = (
                new List<Patio>
                {
                    new Patio { PatioId = 1, Nombre = "Test", Direccion = "Test", NumeroPuntoVenta = 1, Telefono ="123456"},
                    new Patio { PatioId = 2, Nombre = "Test", Direccion = "Test", NumeroPuntoVenta = 2, Telefono ="123456"}
                }
            ).AsQueryable();
            var listaPatiosMock = listaPatios.BuildMock();
            _patioRepository.GetAllIQuerable().Returns(listaPatiosMock);
            int numeroPatiosEsperados = 2;

            //Act
            var result = await target.ObtenerPatiosAsync();
            int numeroPatiosActuales = result.Count;

            //Assert
            Assert.AreEqual(numeroPatiosEsperados, numeroPatiosActuales);
        }

        [Test]
        public async Task ObtenerPatioAsync_ExistePatio_RetornaPatio()
        {
            //Arrange
            var dbContext = BuildDatabaseMock(Guid.NewGuid().ToString());
            Patio patio = new Patio { Direccion = "Test", Nombre = "Test", NumeroPuntoVenta = 1, PatioId = 1, Telefono = "12345" };
            dbContext.Patio.Add(patio);
            await dbContext.SaveChangesAsync();
            ISolicitudCreditoRepository solicitudCredito = Substitute.For<ISolicitudCreditoRepository>();
            IPatioRepository patioRepository = new PatioRepository(dbContext);
            IPatio patioServicio = new PatioServicio(patioRepository, solicitudCredito);

            //Act
            var result = await patioServicio.ObtenerPatioAsync(1);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task ObtenerPatioAsync_NoExistePatio_RetornaNull()
        {
            //Arrange
            var dbContext = BuildDatabaseMock(Guid.NewGuid().ToString());
            Patio patio = new Patio { Direccion = "Test", Nombre = "Test", NumeroPuntoVenta = 1, PatioId = 1, Telefono = "12345" };
            dbContext.Patio.Add(patio);
            await dbContext.SaveChangesAsync();
            ISolicitudCreditoRepository solicitudCredito = Substitute.For<ISolicitudCreditoRepository>();
            IPatioRepository patioRepository = new PatioRepository(dbContext);
            IPatio patioServicio = new PatioServicio(patioRepository, solicitudCredito);

            //Act
            var result = await patioServicio.ObtenerPatioAsync(2);

            //Assert
            Assert.IsNull(result);
        }
    }
}
