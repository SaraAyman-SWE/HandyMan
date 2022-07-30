using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandyMan.Data;
using HandyMan.Models;
using AutoMapper;
using HandyMan.Interfaces;
using Microsoft.AspNetCore.Authorization;
using HandyMan.Dtos;

namespace HandyMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CraftController : ControllerBase
    {
        private readonly ICraftRepository _craftRepository;
        private readonly IMapper _mapper;


        public CraftController(ICraftRepository craftRepository, IMapper mapper)
        {
            _craftRepository = craftRepository;
            _mapper = mapper;
        }

        // GET: api/Craft
        [HttpGet("FindCrafts")]
        [AllowAnonymous] // tested
        public async Task<ActionResult<IEnumerable<CraftDto>>> GetCrafts()
        {
            try
            {
                var crafts = await _craftRepository.GetCraftAsync();
                
                return Ok(_mapper.Map<IEnumerable<CraftDto>>(crafts));
            }
            catch
            {
                return NotFound(new { message = "Empty!" });
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "Admin")] // tested
        public async Task<ActionResult<Craft>> GetCraftbyId(int id)
        {
            try
            {
                var craft = await _craftRepository.GetCraftByIdAsync(id);

                if (craft == null)
                {
                    return NotFound(new { message = "Craft Is Not Found!" });
                }
                return Ok(craft);
            }
            catch
            {
                return NotFound();
            }
        }



        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")] // tested
        public async Task<IActionResult> EditCraft(int id, CraftDto craftdto)
        {
            if (id != craftdto.Craft_ID)
            {
                return NotFound(new { message = "Craft Is Not Found!" });
            }

            var craft = _mapper.Map<Craft>(craftdto);

            _craftRepository.EditCraft(craft);
            try
            {
                await _craftRepository.SaveAllAsync();
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Craft
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "Admin")] // tested
        public async Task<ActionResult<CraftDto>> PostCraft(CraftDto craftdto)
        {
            if (craftdto == null)
            {
                return NotFound(new { message = "Craft Is Not Found!" });
            }
            if (ModelState.IsValid)
            {

                var craft = _mapper.Map<Craft>(craftdto);
                _craftRepository.CreateCraft(craft);
                try
                {


                    await _craftRepository.SaveAllAsync();
                }
                catch
                {
                    return BadRequest(new { Error = "Can't Add This User!" });
                }
                return CreatedAtAction("GetCraftbyId", new { id = craft.Craft_ID }, craft);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // DELETE: api/Craft/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")] // tested -> Same Region Problem ( can not cascade / must be empty )
        public async Task<IActionResult> DeleteCraft(int id)
        {
            try
            {
                _craftRepository.DeleteCraftById(id);
                await _craftRepository.SaveAllAsync();
            }
            catch
            {
                return NotFound(new { message = "Craft Is Not Found!" });
            }

            return NoContent();
        }
    }
}