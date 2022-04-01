using APIClientes.Models;
using APIClientes.Models.Dto;
using AutoMapper;

namespace APIClientes
{
    public class MappingConfig
    {

        // es la clase encargada de mapear dto y modelos

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
              {
                //<de dto , de modelos>
                config.CreateMap<ClienteDto, Cliente>();
                  config.CreateMap<Cliente, ClienteDto>();

              }
            );
            return mappingConfig;

        }
    }
}
