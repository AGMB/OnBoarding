using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.DTO;
using nombremicroservicio.Entities.Entidades;
using nombremicroservicio.Infrastructure.Services;
using nombremicroservicio.Repository.Repositorios;
using nombremicroservicio.Test.DbContext;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Test
{
    [TestFixture]
    public class SolicitudCreditoServicioTest: MockCreditoAutoDbContext
    {
        [Test]
        public async Task GeneraSolicitudAsync_ExisteSolicitudActiva_Retorna2()
        {
            //Arrange
            var dbContext = BuildDatabaseMock(Guid.NewGuid().ToString());
            var solicitudesExistentes = new List<SolicitudCredito>()
            {
                new SolicitudCredito
                {
                     ClienteId = 1,
                     PatioId = 1,
                     VehiculoId = 1,
                     FechaSolicitud = DateTime.Today,
                     EjecutivoId = 1,
                     MesesPlazo = 50,
                     Cuotas = 150,
                     Entrada = 5000,
                     EstadoSolicitud = "Registrada",
                     Observacion = "Test"
                },
                new SolicitudCredito
                {
                    ClienteId = 2,
                    PatioId = 1,
                    VehiculoId = 3,
                    FechaSolicitud = DateTime.Today,
                    EjecutivoId = 5,
                    MesesPlazo = 50,
                    Cuotas = 150,
                    Entrada = 5000,
                    EstadoSolicitud = "Cancelada",
                    Observacion = "Test"
                }
            };
            await dbContext.SolicitudCredito.AddRangeAsync(solicitudesExistentes);
            await dbContext.SaveChangesAsync();
            ISolicitudCreditoRepository solicitudCreditoRepository = new SolicitudCreditoRepository(dbContext);
            IVehiculoRepository vehiculoRepository = Substitute.For<IVehiculoRepository>();
            IClienteRepository clienteRepository = Substitute.For<IClienteRepository>();
            IClientePatioRepository clientePatioRepository = Substitute.For<IClientePatioRepository>();
            IClientePatio clientePatioServicio = Substitute.For<IClientePatio>();
            ISolicitudCredito solicitudCreditoServicio = new SolicitudCreditoServicio(solicitudCreditoRepository, vehiculoRepository, clientePatioServicio, clienteRepository);
            SolicitudCreditoAddDto solicitudGuardar = new SolicitudCreditoAddDto
            {
                ClienteId = 1,
                PatioId = 2,
                VehiculoId = 2,
                FechaSolicitud = DateTime.Today,
                EjecutivoId = 1,
                MesesPlazo = 50,
                Cuotas = 150,
                Entrada = 5000,
                EstadoSolicitud = "Registrada",
                Observacion = "Test"
            };
            int resultadoEsperado = 2;

            //Act
            var resultado = await solicitudCreditoServicio.GeneraSolicitudAsync(solicitudGuardar);

            //Assert
            Assert.AreEqual(resultadoEsperado, resultado);
        }

        [Test]
        public async Task GeneraSolicitudAsync_VehiculoEstaReservado_Retorna3()
        {
            //Arrange
            var dbContext = BuildDatabaseMock(Guid.NewGuid().ToString());
            var solicitudesExistentes = new List<SolicitudCredito>()
            {
                new SolicitudCredito
                {
                     ClienteId = 1,
                     PatioId = 1,
                     VehiculoId = 1,
                     FechaSolicitud = DateTime.Today,
                     EjecutivoId = 1,
                     MesesPlazo = 50,
                     Cuotas = 150,
                     Entrada = 5000,
                     EstadoSolicitud = "Registrada",
                     Observacion = "Test"
                },
                new SolicitudCredito
                {
                    ClienteId = 2,
                    PatioId = 1,
                    VehiculoId = 3,
                    FechaSolicitud = DateTime.Today,
                    EjecutivoId = 5,
                    MesesPlazo = 50,
                    Cuotas = 150,
                    Entrada = 5000,
                    EstadoSolicitud = "Cancelada",
                    Observacion = "Test"
                }
            };
            await dbContext.SolicitudCredito.AddRangeAsync(solicitudesExistentes);
     
            var vehiculoExistente = new Vehiculo
            {
                VehiculoId = 1,
                Placa = "ABC-123",
                MarcaId = 1,
                Modelo = "Test",
                NumeroChasis = "123456",
                Tipo = "Test",
                Cilindraje = "2000",
                Avaluo = 10000
            };
            await dbContext.AddAsync(vehiculoExistente);
            await dbContext.SaveChangesAsync();

            ISolicitudCreditoRepository solicitudCreditoRepository = new SolicitudCreditoRepository(dbContext);
            IVehiculoRepository vehiculoRepository = new VehiculoRepository(dbContext);
            IClienteRepository clienteRepository = Substitute.For<IClienteRepository>();
            IClientePatio clientePatioServicio = Substitute.For<IClientePatio>();
            ISolicitudCredito solicitudCreditoServicio = new SolicitudCreditoServicio(solicitudCreditoRepository, vehiculoRepository, clientePatioServicio, clienteRepository);
            SolicitudCreditoAddDto solicitudGuardar = new SolicitudCreditoAddDto
            {
                ClienteId = 4,
                PatioId = 2,
                VehiculoId = 1,
                FechaSolicitud = DateTime.Today,
                EjecutivoId = 1,
                MesesPlazo = 50,
                Cuotas = 150,
                Entrada = 5000,
                EstadoSolicitud = "Registrada",
                Observacion = "Test"
            };
            int resultadoEsperado = 3;

            //Act
            var resultado = await solicitudCreditoServicio.GeneraSolicitudAsync(solicitudGuardar);

            //Assert
            Assert.AreEqual(resultadoEsperado, resultado);
        }
    }
}
