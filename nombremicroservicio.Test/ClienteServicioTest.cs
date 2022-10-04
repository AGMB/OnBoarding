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
    public class ClienteServicioTest : MockCreditoAutoDbContext
    {
        [Test]
        public async Task GuardarClienteAsync_ClienteValido_SeGuardaCliente()
        {
            //Arrange
            ISolicitudCreditoRepository solicitudCredito = Substitute.For<ISolicitudCreditoRepository>();
            var dbContext = BuildDatabaseMock(Guid.NewGuid().ToString());
            int clientesEsperados = 1;
            var clienteGuardar = new ClienteAddDto
            {
                Nombres = "Test",
                Apellidos = "Test",
                Direccion = "Test",
                Edad = 25,
                EstadoCivil = "Test",
                FechaNacimiento = DateTime.Now,
                Identificacion = "Test",
                IdentificacionConyuge = "1234567890",
                NombreConyuge = "Test",
                SujetoCredito = true,
                Telefono = "1234560"
            };

            IClienteRepository clienteRepository = new ClienteRepository(dbContext);
            ICliente target = new ClienteServicio(clienteRepository, solicitudCredito);

            //Act
            var result = await target.GuardarClienteAsync(clienteGuardar);
            IList<ClienteDto> clientes = await target.ObtenerClientesAsync();

            //Assert
            Assert.AreEqual(clientesEsperados, clientes.Count);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GuardarClienteAsync_ClienteExiste_NoSeGuardaCliente()
        {
            //Arrange
            ISolicitudCreditoRepository solicitudCredito = Substitute.For<ISolicitudCreditoRepository>();
            var dbContext = BuildDatabaseMock(Guid.NewGuid().ToString());
            int clientesEsperados = 2;
            var clientes = new List<Cliente>() 
            {
                new Cliente              
                {
                    Nombres = "Test",
                    Apellidos = "Test",
                    Direccion = "Test",
                    Edad = 25,
                    EstadoCivil = "Test",
                    FechaNacimiento = DateTime.Now,
                    Identificacion = "12345",
                    IdentificacionConyuge = "1234567890",
                    NombreConyuge = "Test",
                    SujetoCredito = true,
                    Telefono = "1234560"
                },
                new Cliente
                {
                    Nombres = "Test2",
                    Apellidos = "Test2",
                    Direccion = "Test2",
                    Edad = 25,
                    EstadoCivil = "Test2",
                    FechaNacimiento = DateTime.Now,
                    Identificacion = "123456789",
                    IdentificacionConyuge = "1234567892",
                    NombreConyuge = "Test",
                    SujetoCredito = true,
                    Telefono = "1234560"
                }
            };

            IClienteRepository clienteRepository = new ClienteRepository(dbContext);
            clienteRepository.AddRange(clientes);
            await clienteRepository.SaveChangesAsync();
            ICliente target = new ClienteServicio(clienteRepository, solicitudCredito);
            ClienteAddDto clienteGuardar = new ClienteAddDto
            {
                Nombres = "Test",
                Apellidos = "Test",
                Direccion = "Test",
                Edad = 25,
                EstadoCivil = "Test",
                FechaNacimiento = DateTime.Now,
                Identificacion = "12345",
                IdentificacionConyuge = "1234567890",
                NombreConyuge = "Test",
                SujetoCredito = true,
                Telefono = "1234560"
            };

            //Act
            var result = await target.GuardarClienteAsync(clienteGuardar);
            IList<ClienteDto> listaClientes = await target.ObtenerClientesAsync();

            //Assert
            Assert.AreEqual(clientesEsperados, clientes.Count);
            Assert.IsFalse(result);
        }
    }
}
