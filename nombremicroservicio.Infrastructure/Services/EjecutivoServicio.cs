using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nombremicroservicio.Infrastructure.Services
{
    public class EjecutivoServicio: IEjecutivo
    {
        #region Propiedades
        private readonly IEjecutivoRepository _ejecutivoRepository;
        #endregion

        #region Constructor
        public EjecutivoServicio(IEjecutivoRepository ejecutivoRepository)
        {
            _ejecutivoRepository = ejecutivoRepository;
        }
        #endregion

        #region Get Metodos
        public async Task<IList<CatalogoEjecutivo>> CatalogoEjecutivosAsync(int patioID)
        {
            return await _ejecutivoRepository.CatalogoEjecutivosAsync(patioID);
        } 
        #endregion
    }
}
