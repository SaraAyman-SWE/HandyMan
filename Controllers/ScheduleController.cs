using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandyMan.Data;
using HandyMan.Models;
using HandyMan.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using HandyMan.Dtos;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;

namespace HandyMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public ScheduleController(IScheduleRepository scheduleRepository , IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        // GET: api/Schedule
        [HttpGet]
        [Authorize(Policy ="Admin")] // tested
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedules()
        {
            try
            {
                var schedules = await _scheduleRepository.GetScheduleAsync();
                var shedulesReturn = _mapper.Map<IEnumerable<ScheduleDto>>(schedules);
                return Ok(shedulesReturn);
            }
            catch
            {
                return BadRequest("Schedules are Empty !");
            }
            
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "Request")] // tested
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedulesByHandymanSsn(int id)
        {
            try
            {
                var schedules = await _scheduleRepository.GetSchedulesByHandymanSsnAsync(id);
                
                if (schedules == null)
                    return BadRequest(new {message = "Handyman Not Found"});
                
                var shedulesReturn = _mapper.Map<IEnumerable<ScheduleDto>>(schedules);
                return Ok(shedulesReturn);
            }
            catch
            {
                return BadRequest("Schedules are Empty !");
            }

        }

        // POST: api/Schedule
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy ="Handyman")] // tested
        public async Task<ActionResult<ScheduleDto>> PostSchedule(ScheduleDto scheduleDto , [FromHeader] string Authorization)
        {
            JwtSecurityToken t = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Authorization.Substring(7));
            var x = t.Claims.ToList();

            if (x[0].Value != scheduleDto.Handy_SSN.ToString())
            {
                return Unauthorized();
            }
            if (scheduleDto == null)
            {
                return Problem("Schedule is Empty");
            }
            if(scheduleDto.Schedule_Date < DateTime.Now.AddDays(1) || scheduleDto.Schedule_Date > DateTime.Now.AddDays(2))
            {
                return BadRequest(new { message = "Can't add this schedule" });
            }

            if (scheduleDto.Time_From > scheduleDto.Time_To)
            {
                return BadRequest(new { message = "Time From Must be Before Time To !!" });
            }
            var schedule = _mapper.Map<Schedule>(scheduleDto);
            _scheduleRepository.CreateSchedule(schedule);
            try
            {
                await _scheduleRepository.SaveAllAsync();
            }
            catch
            {
                return BadRequest(new { message = "Can't Save!" });
            }

            return Ok(new { message = "Schedule Added Successfully" });
        }

        // DELETE: api/Schedule/5
        [HttpDelete]
        [Authorize(Policy ="Handyman")] // tested
        public async Task<IActionResult> DeleteSchedule(ScheduleDto scheduleDto , [FromHeader] string Authorization)
        {
            JwtSecurityToken t = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Authorization.Substring(7));
            var x = t.Claims.ToList();

            if (x[0].Value != scheduleDto.Handy_SSN.ToString())
            {
                return Unauthorized();
            }
            if (scheduleDto == null)
            {
                return BadRequest(new { message="Sent Schedule is Null !!"});
            }
            try
            {
                var schedule =_mapper.Map<Schedule>(scheduleDto);
                _scheduleRepository.DeleteSchedule(schedule);
                await _scheduleRepository.SaveAllAsync();
            }
            catch
            {
                return NotFound(new { message = "Schedule not Found !!"});
            }
            

            return Ok(new {message ="Schedule Deleted Sucessfully"});
        }
    }
}
