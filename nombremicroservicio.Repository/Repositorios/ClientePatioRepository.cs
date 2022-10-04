using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Entities.Entidades;
using nombremicroservicio.Repository.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace nombremicroservicio.Repository.Repositorios
{
    public class ClientePatioRepository: BaseRepository<ClientePatio>, IClientePatioRepository
    {
        public ClientePatioRepository(CreditoAutoDbContext dbContext):base(dbContext)
        {

        }
    }
}
