using nombremicroservicio.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces
{
    public interface IMarca
    {
        public Task<IList<CatalogoMarca>> CatalogoMarcasAsync();
    }
}
