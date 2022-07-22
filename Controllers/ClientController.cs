﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandyMan.Interfaces;
using HandyMan.Models;
using NuGet.Protocol;

namespace HandyMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            try
            {
                return Ok(await _clientRepository.GetClientsAsync());
            }
            catch
            {
                return NotFound(new { message = "Empty!" });
            }
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(id);
                if (client == null)
                {
                    return NotFound(new { message = "Client Is Not Found!" });
                }
                return client;
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditClient(int id, Client client)
        {
            if (id != client.Client_ID)
            {
                return NotFound(new { message = "Client Is Not Found!" });
            }
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
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            if (client == null)
            {
                return NotFound(new { message = "Client Is Not Found!" });
            }
            _clientRepository.CreateClient(client);
            try
            {
                await _clientRepository.SaveAllAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }

            return CreatedAtAction("GetClient", new { id = client.Client_ID }, client);
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