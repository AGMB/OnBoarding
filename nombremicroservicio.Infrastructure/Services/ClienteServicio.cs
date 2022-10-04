using Microsoft.EntityFrameworkCore;
using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.DTO;
using nombremicroservicio.Entities.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nombremicroservicio.Infrastructure.Services
{
    public class ClienteServicio : ICliente
    {
        #region Propiedades
        private readonly IClienteRepository _clienteRepository;
        private readonly ISolicitudCreditoRepository _solicitudCreditoRepository;
        #endregion

        #region Constructor
        public ClienteServicio(IClienteRepository clienteRepository, ISolicitudCreditoRepository solicitudCreditoRepository)
        {
            _clienteRepository = clienteRepository;
            _solicitudCreditoRepository = solicitudCreditoRepository;
        }
        #endregion

        #region CRUD Operaciones
        /// <summary>
        /// Actualizar Cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns>
        /// 0 No existe Cliente
        /// 1 Cliente Actualizado
        /// 2 No se puede actualizar el cliente, # identificacion ya existe
        /// </returns>
        public async Task<int> ActualizarClienteAsync(ClienteDto cliente)
        {
            var response = 0;
            var clienteActualizar = await _clienteRepository.GetByIdAsync(cliente.ClienteId);

            if (clienteActualizar is null)
                return response;

            if (!await ExisteOtroClienteConMismaIdentificacion(clienteActualizar.ClienteId, cliente.Identificacion))
            {
                clienteActualizar.Identificacion = cliente.Identificacion;
                clienteActualizar.Nombres = cliente.Nombres;
                clienteActualizar.Apellidos = cliente.Apellidos;
                clienteActualizar.Edad = cliente.Edad;
                clienteActualizar.FechaNacimiento = cliente.FechaNacimiento;
                clienteActualizar.Telefono = cliente.Telefono;
                clienteActualizar.Direccion = cliente.Direccion;
                clienteActualizar.EstadoCivil = cliente.EstadoCivil;
                clienteActualizar.IdentificacionConyuge = cliente.IdentificacionConyuge;
                clienteActualizar.NombreConyuge = cliente.NombreConyuge;
                clienteActualizar.SujetoCredito = cliente.SujetoCredito;

                _clienteRepository.Update(clienteActualizar);
                await _clienteRepository.SaveChangesAsync();
                response = 1;
            }
            else
                response = 2;

            return response;
        }

        public async Task<int> EliminarClienteAsync(int clienteid)
        {
            var clienteEliminar = await _clienteRepository.GetByIdAsync(clienteid);
            if (clienteEliminar is null)
                return 0;
            if (await ClienteEstaActivo(clienteid))
                return 2;

            _clienteRepository.Remove(clienteEliminar);
            await _clienteRepository.SaveChangesAsync();

            return 1;
        }

        public async Task<bool> GuardarClienteAsync(ClienteAddDto cliente)
        {
            var response = false;
            if (!await ExisteClientePorIdentificacion(cliente.Identificacion))
            {
                var clienteGuardar = new Cliente
                {
                    Identificacion = cliente.Identificacion,
                    Nombres = cliente.Nombres,
                    Apellidos = cliente.Apellidos,
                    Edad = cliente.Edad,
                    FechaNacimiento = cliente.FechaNacimiento,
                    Telefono = cliente.Telefono,
                    Direccion = cliente.Direccion,
                    EstadoCivil = cliente.EstadoCivil,
                    IdentificacionConyuge = cliente.IdentificacionConyuge,
                    NombreConyuge = cliente.NombreConyuge,
                    SujetoCredito = cliente.SujetoCredito,

                };
                await _clienteRepository.AddAsync(clienteGuardar);
                await _clienteRepository.SaveChangesAsync();
                response = true;
            }
            return response;
        }

        public async Task<ClienteDto> ObtenerClienteAsync(string identificacion)
        {
            var cliente = await _clienteRepository.GetAllIQuerable().FirstOrDefaultAsync(x => x.Identificacion.Equals(identificacion));

            if (cliente is null)
                return null;

            var result = new ClienteDto
            {
                ClienteId = cliente.ClienteId,
                Identificacion = cliente.Identificacion,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                Edad = cliente.Edad,
                FechaNacimiento = cliente.FechaNacimiento,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                EstadoCivil = cliente.EstadoCivil,
                IdentificacionConyuge = cliente.IdentificacionConyuge,
                NombreConyuge = cliente.NombreConyuge,
                SujetoCredito = cliente.SujetoCredito,
            };

            return result;

        }

        public async Task<IList<ClienteDto>> ObtenerClientesAsync()
        {
            List<ClienteDto> clientes = await _clienteRepository.GetAllIQuerable()
                           .Select(x => new ClienteDto
                           {
                               ClienteId = x.ClienteId,
                               Identificacion = x.Identificacion,
                               Nombres = x.Nombres,
                               Apellidos = x.Apellidos,
                               Edad = x.Edad,
                               FechaNacimiento = x.FechaNacimiento,
                               Telefono = x.Telefono,
                               Direccion = x.Direccion,
                               EstadoCivil = x.EstadoCivil,
                               IdentificacionConyuge = x.IdentificacionConyuge,
                               NombreConyuge = x.NombreConyuge,
                               SujetoCredito = x.SujetoCredito,
                           }).ToListAsync();

            return clientes;
        }
        #endregion

        #region Metodos Privados
        private async Task<bool> ExisteClientePorIdentificacion(string identificacion)
        {
            return await _clienteRepository.GetAllIQuerable().AnyAsync(x => x.Identificacion.Equals(identificacion));
        }

        private async Task<bool> ExisteOtroClienteConMismaIdentificacion(int clienteActualID, string identificacion)
        {
            return await _clienteRepository.GetAllIQuerable()
                                           .AnyAsync(x => x.Identificacion.Equals(identificacion) && x.ClienteId != clienteActualID);
        }

        private async Task<bool> ClienteEstaActivo(int clienteId)
        {
            return await _solicitudCreditoRepository.ClienteEstaAsociadoAsync(clienteId);
        } 
        #endregion
    }
}
