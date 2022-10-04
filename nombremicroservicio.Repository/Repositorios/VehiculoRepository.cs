using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Entities.Entidades;
using nombremicroservicio.Repository.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace nombremicroservicio.Repository.Repositorios
{
    public class VehiculoRepository : BaseRepository<Vehiculo>, IVehiculoRepository
    {
        public VehiculoRepository(CreditoAutoDbContext dbContext):base(dbContext)
        { }

    }
}
