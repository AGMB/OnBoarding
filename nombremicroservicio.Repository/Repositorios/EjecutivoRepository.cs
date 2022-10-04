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
    public class EjecutivoRepository: BaseRepository<Ejecutivo>, IEjecutivoRepository
    {
        public EjecutivoRepository(CreditoAutoDbContext dbContext):base(dbContext)
        {

        }

        public async Task<IList<CatalogoEjecutivo>> CatalogoEjecutivosAsync(int patioID)
        {
            var ejecutivos = await GetAllIQuerable().Where(x => x.PatioId == patioID)
                             .Select(x => new CatalogoEjecutivo
                             {
                                 EjecutivoId = x.EjecutivoId,
                                 NombreEjecutivo = x.Nombres + " " + x.Apellidos
                             }).ToListAsync();

            return ejecutivos;
        }
    }
}
