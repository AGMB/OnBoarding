using nombremicroservicio.Entities.DTO;
using nombremicroservicio.Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces.Repository
{
    public interface IMarcaRepository: IBaseRepository<Marca>
    {
        public Task<IList<CatalogoMarca>> CatalogoMarcasAsync();
    }
}
