using APIClientes.Data;
using APIClientes.Models;
using APIClientes.Models.Dto;
using APIClientes.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly IClienteRepositorio _clienteRepositorio;
        protected ResponseDto _response;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _response= new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {

            try
            {
                var lista = await _clienteRepositorio.GetClientes();
                _response.Result= lista;
                _response.DisplayMessage = "Lista de Clientes";

            }
            catch (Exception ex)
            {

                _response.IsSuccess=false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_response);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
               var cliente= await _clienteRepositorio.GetClienteById(id);
            if (cliente==null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Cliente no existe";
                return NotFound(_response);
            }

            _response.Result = cliente;
            _response.DisplayMessage = "Informacion del Cliente";
            return Ok(_response);

        }

        //[HttpPost]
        //public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        //{
        //    _db.Clientes.Add(cliente);
        //    await _db.SaveChangesAsync();

        //    return CreatedAtAction("GetCliente",new {id=cliente.Id},cliente);liy
        //}

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteDto clienteDto)
        {
            try
            {
                ClienteDto model = await _clienteRepositorio.CreateUpdate(clienteDto);
                _response.Result = model;

                return CreatedAtAction("GetCliente", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.DisplayMessage = "Error Al Guardar Cliente";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> PutCliente(int id ,Cliente cliente)
        //{
        //    if (id!=cliente.Id)
        //    {
        //        return BadRequest();
        //    }
        //     _db.Entry(cliente).State=EntityState.Modified;

        //        await _db.SaveChangesAsync();

        //    return NoContent();
        //}


        [HttpPut("{id}")]
        public async Task<ActionResult> PutCliente(int id, ClienteDto clienteDto)
        {
            try
            {
                ClienteDto model = await _clienteRepositorio.CreateUpdate(clienteDto);
                _response.Result=model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess=false;
                _response.DisplayMessage = "Error Al Actualizar Cliente";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteCliente(int id)
        //{
        //    var cliente = await _db.Clientes.FindAsync(id);
        //    if (cliente==null)
        //    {
        //        return NotFound();

        //    }
        //    _db.Clientes.Remove(cliente);
        //    await _db.SaveChangesAsync();

        //    return NoContent();

        //}


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {

            try
            {
                bool estaEliminado = await _clienteRepositorio.DeleteCliente(id);

                if (estaEliminado)
                {
                    _response.Result = estaEliminado;
                    _response.DisplayMessage = "Cliente Eliminado con exito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al eliminar eleimento";
                    return BadRequest(_response);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString()};
                return BadRequest(_response);
            }
        }


    }
}
