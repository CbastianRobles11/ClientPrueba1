using APIClientes.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIClientes.Repositorio
{
    public interface IClienteRepositorio
    {
        // aki indicaremos todos los metodos que vamos a usar 


        // traemos la lista del Models.Rto/ClienteDto
        //captura lista de clientes
        Task<List<ClienteDto>> GetClientes();

        //retorn un cliente
        Task<ClienteDto> GetClienteById(int id);


        //updetea y crea cliente
        Task<ClienteDto> CreateUpdate(ClienteDto clienteDto);

        Task<bool> DeleteCliente(int id);


    }
}
