using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nombremicroservicio.Infrastructure.Services
{
    public class MarcaServicio: IMarca
    {
        #region Propiedades
        private readonly IMarcaRepository _marcaRepository;
        #endregion

        #region Constructor
        public MarcaServicio(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }
        #endregion

        #region Get Metodos
        public async Task<IList<CatalogoMarca>> CatalogoMarcasAsync()
        {
            return await _marcaRepository.CatalogoMarcasAsync();
        } 
        #endregion
    }
}
