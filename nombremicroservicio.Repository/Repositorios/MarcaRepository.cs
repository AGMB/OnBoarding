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
    public class MarcaRepository : BaseRepository<Marca>, IMarcaRepository
    {
        public MarcaRepository(CreditoAutoDbContext dbContext):base(dbContext)
        {
                
        }
        public async Task<IList<CatalogoMarca>> CatalogoMarcasAsync()
        {
            var marcas = await GetAllIQuerable().Select(x => new CatalogoMarca
            {
                MarcaId = x.MarcaId,
                Marca = x.NombreMarca
            }).ToListAsync();

            return marcas;
        }
    }
}
