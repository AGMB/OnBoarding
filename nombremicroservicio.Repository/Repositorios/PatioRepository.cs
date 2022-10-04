using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Entities.Entidades;
using nombremicroservicio.Repository.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace nombremicroservicio.Repository.Repositorios
{
    public class PatioRepository: BaseRepository<Patio>, IPatioRepository
    {
        public PatioRepository(CreditoAutoDbContext dbContext):base(dbContext)
        {

        }
    }
}
