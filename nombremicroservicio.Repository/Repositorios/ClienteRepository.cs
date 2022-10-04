using Microsoft.EntityFrameworkCore;
using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Entities.DTO;
using nombremicroservicio.Entities.Entidades;
using nombremicroservicio.Repository.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Repository.Repositorios
{
    public class ClienteRepository: BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(CreditoAutoDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<string> ObtenerClienteIdentificacionAsync(int clienteId)
        {
            var result = await GetAllIQuerable().Where(x => x.ClienteId.Equals(clienteId)).Select(x => x.Identificacion).FirstOrDefaultAsync();
            return result;
        }
    }
}
