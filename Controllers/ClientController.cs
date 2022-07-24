using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using HandyMan.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HandyMan.Interfaces;
using HandyMan.Models;

namespace HandyMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
        {
            try
            {
                var clients = await _clientRepository.GetClientsAsync();
                var clientsToReturn = _mapper.Map<IEnumerable<ClientDto>>(clients);
                return Ok(clientsToReturn);
            }
            catch
            {
                return NotFound(new { message = "Empty!" });
            }
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(id);
                if (client == null)
                {
                    return NotFound(new { message = "Client Is Not Found!" });
                }
                return _mapper.Map<ClientDto>(client);
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditClient(int id, ClientDto clientDto)
        {
            if (id != clientDto.Client_ID)
            {
                return NotFound(new { message = "Client Is Not Found!" });
            }

            var client = _mapper.Map<Client>(clientDto);
            _clientRepository.EditClient(client);
            try
            {
                await _clientRepository.SaveAllAsync();
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Client
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientDto>> PostClient(ClientDto clientDto)
        {
            if (clientDto == null)
            {
                return NotFound(new { message = "Client Is Not Found!" });
            }
            if (ModelState.IsValid)
            {
                var client = _mapper.Map<Client>(clientDto);
                _clientRepository.CreateClient(client);
                try
                {
                    await _clientRepository.SaveAllAsync();
                }
                catch
                {
                    return BadRequest(new {Error = "Can't Add This User!" });
                }
                return CreatedAtAction("GetClient", new { id = clientDto.Client_ID }, clientDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                _clientRepository.DeleteClientById(id);
                await _clientRepository.SaveAllAsync();
            }
            catch
            {
                return NotFound(new { message = "Client Is Not Found!" });
            }

            return NoContent();
        }
    }
}
