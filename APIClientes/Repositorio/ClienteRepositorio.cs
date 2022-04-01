using APIClientes.Data;
using APIClientes.Models;
using APIClientes.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIClientes.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ApplicationDbContext _db;

        // crear una variable para mapeo
        private IMapper _mapper;

        public ClienteRepositorio(ApplicationDbContext db,IMapper mapper)
        {
            _db= db;
            _mapper = mapper;
        }

        public async  Task<ClienteDto> CreateUpdate(ClienteDto clienteDto)
        {
            //primero mapeamos el coliente que recibe
            Cliente cliente=_mapper.Map<ClienteDto,Cliente>(clienteDto);

            //sim el cliente es mayor a 0 el id existe se trata de un update
            if (cliente.Id>0)
            {
               _db.Clientes.Update(cliente);
            }
            else
            {
                await _db.Clientes.AddAsync(cliente);

            }

            //guardamos los cambis
            await _db.SaveChangesAsync();

            // mandamos mapeado para retorne el modelo cliete dto y no cliente
            return _mapper.Map<Cliente,ClienteDto>(cliente);


        }

        public async  Task<bool> DeleteCliente(int id)
        {
            try
            {
                Cliente cliente = await _db.Clientes.FindAsync(id);

                // en caso que no exista
                if (cliente==null)
                {
                    return false;
                }
                _db.Clientes.Remove(cliente);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public async  Task<ClienteDto> GetClienteById(int id)
        {
            Cliente cliente = await _db.Clientes.FindAsync(id);

            return _mapper.Map<ClienteDto>(cliente);

        }

        public async  Task<List<ClienteDto>> GetClientes()
        {
            //que sea una lista cliente
            List<Cliente> lista = await _db.Clientes.ToListAsync();

            //mapeamos
            return _mapper.Map<List<ClienteDto>>(lista);
        }
    }
}
