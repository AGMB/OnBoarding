using nombremicroservicio.Entities.DTO;
using nombremicroservicio.Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces.Repository
{
    public interface IEjecutivoRepository: IBaseRepository<Ejecutivo>
    {
        public Task<IList<CatalogoEjecutivo>> CatalogoEjecutivosAsync(int patioID);
    }
}
