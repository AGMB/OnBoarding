using CsvHelper;
using Microsoft.Extensions.Configuration;
using nombremicroservicio.Domain.Interfaces.Repository;
using nombremicroservicio.Domain.Interfaces.Services;
using nombremicroservicio.Entities.DTO;
using nombremicroservicio.Entities.Entidades;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace nombremicroservicio.Infrastructure.Services
{
    public class CargaInicialServicio : ICargaInicial
    {
        #region Propiedades
        private readonly IBaseRepository<Marca> _marcaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IBaseRepository<Ejecutivo> _ejecutivoRepository;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public CargaInicialServicio(IBaseRepository<Marca> marcaRepository, IClienteRepository clienteRepository, IBaseRepository<Ejecutivo> ejecutivoRepository, IConfiguration configuration)
        {
            _clienteRepository = clienteRepository;
            _ejecutivoRepository = ejecutivoRepository;
            _marcaRepository = marcaRepository;
            _configuration = configuration;
        }
        #endregion

        #region Metodos Privados
        private void CargaClientes()
        {
            if (_clienteRepository.GetAllIQuerable().ToList().Count == 0)
            {
                using (var streamReader = new StreamReader(_configuration["ArchivosIniciales:Clientes"]))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var records = csvReader.GetRecords<ClienteAddDto>().GroupBy(x => x.Identificacion).Select(grp => grp.First()).ToList();
                        var recordToAdd = new List<Cliente>();
                        foreach (var record in records)
                        {
                            recordToAdd.Add(new Cliente
                            {
                                Identificacion = record.Identificacion,
                                Nombres = record.Nombres,
                                Apellidos = record.Apellidos,
                                Edad = record.Edad,
                                FechaNacimiento = record.FechaNacimiento,
                                Direccion = record.Direccion,
                                Telefono = record.Telefono,
                                EstadoCivil = record.EstadoCivil,
                                IdentificacionConyuge = record.IdentificacionConyuge,
                                NombreConyuge = record.NombreConyuge,
                                SujetoCredito = record.SujetoCredito
                            });
                        }
                        _clienteRepository.AddRange(recordToAdd);
                        _clienteRepository.SaveChanges();
                    }
                }
            }
            else
            {
                using (var streamReader = new StreamReader(_configuration["ArchivosIniciales:Clientes"]))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var records = csvReader.GetRecords<ClienteAddDto>().GroupBy(x => x.Identificacion).Select(grp => grp.First()).ToList();
                        var recordToAdd = new List<Cliente>();
                        foreach (var record in records)
                        {
                            if (!_clienteRepository.GetAllIQuerable().Any(x => x.Identificacion.Equals(record.Identificacion)))
                            {
                                recordToAdd.Add(new Cliente
                                {
                                    Identificacion = record.Identificacion,
                                    Nombres = record.Nombres,
                                    Apellidos = record.Apellidos,
                                    Edad = record.Edad,
                                    FechaNacimiento = record.FechaNacimiento,
                                    Direccion = record.Direccion,
                                    Telefono = record.Telefono,
                                    EstadoCivil = record.EstadoCivil,
                                    IdentificacionConyuge = record.IdentificacionConyuge,
                                    NombreConyuge = record.NombreConyuge,
                                    SujetoCredito = record.SujetoCredito
                                });
                            }
                        }
                        _clienteRepository.AddRange(recordToAdd);
                        _clienteRepository.SaveChanges();
                    }
                }
            }
        }

        private void CargaEjecutivos()
        {

            if (_ejecutivoRepository.GetAllIQuerable().ToList().Count == 0)
            {
                using (var streamReader = new StreamReader(_configuration["ArchivosIniciales:Ejecutivos"]))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var records = csvReader.GetRecords<EjecutivoDto>().GroupBy(x => x.Identificacion).Select(grp => grp.First()).ToList();
                        var recordToAdd = new List<Ejecutivo>();
                        foreach (var record in records)
                        {
                            recordToAdd.Add(new Ejecutivo
                            {
                                Identificacion = record.Identificacion,
                                Nombres = record.Nombres,
                                Apellidos = record.Apellidos,
                                Edad = record.Edad,
                                Direccion = record.Direccion,
                                TelefonoConvencional = record.TelefonoConvencional,
                                Celular = record.Celular,
                                PatioId = record.PatioId
                            });
                        }
                        _ejecutivoRepository.AddRange(recordToAdd);
                        _ejecutivoRepository.SaveChanges();
                    }
                }
            }
            else
            {
                using (var streamReader = new StreamReader(_configuration["ArchivosIniciales:Ejecutivos"]))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var records = csvReader.GetRecords<EjecutivoDto>().GroupBy(x => x.Identificacion).Select(grp => grp.First()).ToList();
                        var recordToAdd = new List<Ejecutivo>();
                        foreach (var record in records)
                        {
                            if (!_ejecutivoRepository.GetAllIQuerable().Any(x => x.Identificacion.Equals(record.Identificacion)))
                            {
                                recordToAdd.Add(new Ejecutivo
                                {
                                    Identificacion = record.Identificacion,
                                    Nombres = record.Nombres,
                                    Apellidos = record.Apellidos,
                                    Edad = record.Edad,
                                    Direccion = record.Direccion,
                                    TelefonoConvencional = record.TelefonoConvencional,
                                    Celular = record.Celular,
                                    PatioId = record.PatioId
                                });
                            }
                        }
                        _ejecutivoRepository.AddRange(recordToAdd);
                        _ejecutivoRepository.SaveChanges();
                    }
                }
            }
        }

        private void CargaMarcass()
        {
            if (_marcaRepository.GetAllIQuerable().ToList().Count == 0)
            {
                using (var streamReader = new StreamReader(_configuration["ArchivosIniciales:Marcas"]))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var records = csvReader.GetRecords<MarcaDto>().GroupBy(x => x.NombreMarca).Select(grp => grp.First()).ToList();
                        var recordToAdd = new List<Marca>();
                        foreach (var record in records)
                        {
                            recordToAdd.Add(new Marca
                            {
                                NombreMarca = record.NombreMarca
                            });
                        }
                        _marcaRepository.AddRange(recordToAdd);
                        _marcaRepository.SaveChanges();
                    }
                }
            }
            else
            {
                using (var streamReader = new StreamReader(_configuration["ArchivosIniciales:Marcas"]))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var records = csvReader.GetRecords<MarcaDto>().GroupBy(x => x.NombreMarca).Select(grp => grp.First()).ToList();
                        var recordToAdd = new List<Marca>();
                        foreach (var record in records)
                        {
                            if (!_marcaRepository.GetAllIQuerable().Any(x => x.NombreMarca.Equals(record.NombreMarca)))
                            {
                                recordToAdd.Add(new Marca
                                {
                                    NombreMarca = record.NombreMarca
                                });
                            }
                        }
                        _marcaRepository.AddRange(recordToAdd);
                        _marcaRepository.SaveChanges();
                    }
                }
            }
        }
        #endregion

        #region CargaInicial
        public void CargarArchivosIniciales()
        {
            CargaClientes();
            CargaEjecutivos();
            CargaMarcass();
        } 
        #endregion
    }
}
