using Microsoft.EntityFrameworkCore;
using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Entities.Entidades;
using nombremicroservicio.Repository.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Repository.Repositorios
{
    public class SolicitudCreditoRepository: BaseRepository<SolicitudCredito>, ISolicitudCreditoRepository
    {
        public SolicitudCreditoRepository(CreditoAutoDbContext dbContext):base(dbContext)
        {

        }

        public async Task<bool> ClienteEstaAsociadoAsync(int clienteId)
        {
            return await GetAllIQuerable().Where(x => x.ClienteId == clienteId).AnyAsync();
        }

        public async Task<bool> PatioEstaAsociadoAsync(int patioId)
        {
            return await GetAllIQuerable().Where(x => x.PatioId == patioId).AnyAsync();
        }

        public  async Task<bool> VehiculoEstaAsociadoAsync(int vehiculoId)
        {
            return await GetAllIQuerable().Where(x => x.VehiculoId == vehiculoId).AnyAsync();
        }
    }
}
