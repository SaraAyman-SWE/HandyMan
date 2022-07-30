using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandyMan.Data;
using HandyMan.Dtos;
using HandyMan.Interfaces;
using HandyMan.Models;
using Microsoft.AspNetCore.Authorization;

namespace HandyMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        // GET: api/Region
        [HttpGet]
        [AllowAnonymous] // tested
        public async Task<ActionResult<IEnumerable<RegionDto>>> GetRegions()
        {
            try
            {
                var regions = await _regionRepository.GetRegionAsync();
                var regionsToReturn = _mapper.Map<IEnumerable<RegionDto>>(regions);
                return Ok(regionsToReturn);
            }
            catch
            {
                return NotFound(new { message = "Empty!" });
            }
        }

        // GET: api/Region/5
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")] // tested
        public async Task<ActionResult<RegionAdminDto>> GetRegion(int id)
        {
            try
            {
                var region = await _regionRepository.GetRegionByIdAsync(id);
                var regionToReturn = _mapper.Map<RegionAdminDto>(region);
                return Ok(regionToReturn);
            }
            catch
            {
                return NotFound(new { message = "Empty!" });
            }
        }

        // PUT: api/Region/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")] // tested
        public async Task<IActionResult> PutRegion(int id, RegionDto regionDto)
        {
            if (id != regionDto.Region_ID)
            {
                return NotFound(new { message = "Region Is Not Found!" });
            }

            var region = _mapper.Map<Region>(regionDto);
            _regionRepository.EditRegion(region);
            try
            {
                await _regionRepository.SaveAllAsync();
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Region
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "Admin")] // tested
        public async Task<ActionResult<RegionDto>> PostRegion(RegionDto regionDto)
        {
            if (regionDto == null)
            {
                return NotFound(new { message = "Region Is Not Found!" });
            }
            if (ModelState.IsValid)
            {
                var region = _mapper.Map<Region>(regionDto);
                _regionRepository.CreateRegion(region);
                try
                {
                    await _regionRepository.SaveAllAsync();
                }
                catch
                {
                    return BadRequest(new { Error = "Can't Add This Region!" });
                }
                return CreatedAtAction("GetRegion", new { id = regionDto.Region_ID }, regionDto);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // DELETE: api/Region/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")] // tested -> Not working for cascading on client region id
        public async Task<IActionResult> DeleteRegion(int id)
        {
            try
            {
                _regionRepository.DeleteRegionById(id);
                await _regionRepository.SaveAllAsync();
            }
            catch
            {
                return NotFound(new { message = "Region Is Not Found!" });
            }

            return NoContent();
        }

    }
}
