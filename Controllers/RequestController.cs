using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandyMan.Dtos;
using HandyMan.Interfaces;
using HandyMan.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace HandyMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository _requestRepository;
        
        private readonly IMapper _mapper;

        public RequestController(IRequestRepository requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
            
        }


        //General 

        // GET: api/Request
        [HttpGet]
        [Authorize(Policy = "Admin")] // tested
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequests()
        {
            try
            {
                var requests = await _requestRepository.GetRequestsAsync();
                var requestsToReturn = _mapper.Map<IEnumerable<RequestDto>>(requests);
                return Ok(requestsToReturn);
            }
            catch
            {
                return NotFound(new { message = "Empty!" });
            }
        }
        


        // GET: api/Request/5
        [HttpGet("{id:int}")]
        [Authorize(Policy = "Request")] // tested
        public async Task<ActionResult<RequestDto>> GetRequest(int id, [FromHeader] string Authorization)
        {
            JwtSecurityToken t = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Authorization.Substring(7));
            var x = t.Claims.ToList();

            
            try
            {
                var request = await _requestRepository.GetRequestByIdAsync(id);
                if (request == null)
                {
                    return NotFound(new { message = "Request Is Not Found!" });
                }
                if (x[2].Value == "Client" && x[0].Value == request.Client_ID.ToString())
                {
                    if (request.Request_Status == 2 && request.Request_Date >= DateTime.Now)
                    {
                        var requestToReturn = _mapper.Map<RequestDto>(request);
                        var handyman =await _requestRepository.GetHandymanFromRequestByIdAsync(request.Handyman_SSN);
                        return Ok(new { request = requestToReturn, handymanPhone = handyman.Handyman_Mobile });
                    }
                    
                    return _mapper.Map<RequestDto>(request);
                }


                if (x[2].Value == "Handyman" && x[0].Value == request.Handyman_SSN.ToString())
                {
                    if (request.Request_Status == 2 && request.Request_Date >= DateTime.Now)
                    {
                        var requestToReturn = _mapper.Map<RequestDto>(request);
                        var client = await _requestRepository.GetClientFromRequestByIdAsync(request.Client_ID);
                        return Ok(new { request = requestToReturn, clientPhone = client.Client_Mobile, clientAdress = client.Client_Address }); ;
                    }

                    return _mapper.Map<RequestDto>(request);
                }
                if (x[2].Value == "Admin")
                    return _mapper.Map<RequestDto>(request);
                else
                    return Unauthorized();
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet("cancel/{id}")]
        [Authorize(Policy = "Request")] // tested
        public async Task<ActionResult<RequestDto>> CancelRequest(int id, [FromHeader] string Authorization)
        {
            JwtSecurityToken t = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Authorization.Substring(7));
            var x = t.Claims.ToList();


            try
            {
                var request = await _requestRepository.GetRequestByIdAsync(id);
                if (request == null)
                {
                    return NotFound(new { message = "Request Is Not Found!" });
                }
                if (request.Request_Status == 3 || request.Request_Status == 4)
                    return BadRequest(new { Message = "Request already canceled !!" });
                if (x[2].Value == "Client" && x[0].Value == request.Client_ID.ToString())
                {
                    _requestRepository.CancelByRequestId(id,0);
                    return Ok(new { message = "Canceled Successfully by Client" });
                }


                if (x[2].Value == "Handyman" && x[0].Value == request.Handyman_SSN.ToString())
                {
                    _requestRepository.CancelByRequestId(id,1);
                    return Ok(new { message = "Canceled Successfully by Handyman"});
                }
                if (x[2].Value == "Admin")
                {
                    _requestRepository.CancelByRequestId(id, 2);
                    return Ok(new { message = "Canceled Successfully by Admin" });
                }
                    
                else
                    return Unauthorized();
            }
            catch
            {
                return NotFound();
            }
        }


        //Client Functions

        [HttpGet("client/{id}")]
        [Authorize(Policy ="Client")] // tested
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequestsByClientId(int id, [FromHeader] string Authorization)
        {
            JwtSecurityToken t = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Authorization.Substring(7));
            var x = t.Claims.ToList();

            if (x[0].Value != id.ToString() && x[2].Value != "Admin")
            {
                return Unauthorized();
            }
            try
            {
                var requests = await _requestRepository.GetRequestsByClientIdAsync(id);
                var requestsToReturn = _mapper.Map<IEnumerable<RequestDto>>(requests);
                return Ok(requestsToReturn);
            }
            catch
            {
                return NotFound(new { message = "Empty!" });
            }
        }



                    //Handyman Functions


        [HttpGet("handyman/{handymanSsn}")]
        [Authorize(Policy = "Handyman")] // tested
        //function to get all requests of a handyman
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequestsByHandymanSsn(int handymanSsn, [FromHeader] string Authorization)
        {
            JwtSecurityToken t = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Authorization.Substring(7));
            var x = t.Claims.ToList();

            if (x[0].Value != handymanSsn.ToString() && x[2].Value != "Admin")
            {
                return Unauthorized();
            }
            try
            {
                var requests = await _requestRepository.GetRequestsByHandymanSsnAsync(handymanSsn);
                var requestsToReturn = _mapper.Map<IEnumerable<RequestDto>>(requests);
                return Ok(requestsToReturn);
            }
            catch
            {
                return NotFound(new { message = "Empty!" });
            }
        }


        [HttpGet("handyman/pending/{handymanSsn}")]
        [Authorize(Policy = "Handyman")] // tested
        //function to get all Pending requests of a handyman
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetActiveRequestsByHandymanSsn(int handymanSsn, [FromHeader] string Authorization)
        {
            JwtSecurityToken t = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Authorization.Substring(7));
            var x = t.Claims.ToList();

            if (x[0].Value != handymanSsn.ToString() && x[2].Value != "Admin")
            {
                return Unauthorized();
            }
            try
            {
                var requests = await _requestRepository.GetActiveRequestsByHandymanSsnAsync(handymanSsn);
                var requestsToReturn = _mapper.Map<IEnumerable<RequestDto>>(requests);
                return Ok(requestsToReturn);
            }
            catch
            {
                return NotFound(new { message = "Empty!" });
            }
        }



        [HttpGet("accept/{id}")]
        [Authorize(Policy ="Handyman")] // tested
        public async Task<ActionResult<RequestDto>> AcceptPendingRequest(int id, [FromHeader] string Authorization)
        {
            JwtSecurityToken t = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Authorization.Substring(7));
            var x = t.Claims.ToList();

            
            if (x[0].Value != id.ToString() && x[2].Value != "Handyman")
            {
                return Unauthorized();
            }

            var request = await _requestRepository.GetRequestByIdAsync(id);
            if (request == null)
                return BadRequest(new {message="Request not Found !"});
            
            
            //check status of request
            if ((request.Request_Status !=1) && (request.Request_Status!=0))
                return BadRequest(new { message = "This Request is Not Pending" });

            //check if handyman reviewed last request
            var prevRequest = await _requestRepository.GetPrevRequest(request.Handyman_SSN, 1);
            if (prevRequest != null)
            {
                var mappedPrevRequest = _mapper.Map<RequestDto>(prevRequest);
                return CreatedAtAction("GetRequest", new { id = mappedPrevRequest.Request_ID }, mappedPrevRequest);
            }
            if (! await _requestRepository.CheckRequestTimeDuplicate(request))
            {
                return BadRequest(new {message ="There is another Request with same Time Accepted!"});
            }
            request.Request_Status = 2;
            _requestRepository.EditRequest(request);
            
            try
            {
                
                await _requestRepository.SaveAllAsync();
                var clientToReturn= _mapper.Map<ClientDto>(await _requestRepository.GetClientFromRequestByIdAsync(request.Client_ID));
                return Ok(new {phone=clientToReturn.Client_Mobile , address=clientToReturn.Client_Address});
            }
            catch
            {
                return BadRequest(new {message= "Can Not Accept This Request !"});
            }
        }


        // PUT: api/Request/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")] // tested
        public async Task<IActionResult> EditRequest(int id, RequestDto requestDto)
        {
            if (id != requestDto.Request_ID)
            {
                return NotFound(new { message = "Request Is Not Found!" });
            }

            var request = _mapper.Map<Request>(requestDto);
            _requestRepository.EditRequest(request);
            try
            {
                await _requestRepository.SaveAllAsync();
            }

            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Request
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "Client")] // tested
        public async Task<ActionResult<RequestDto>> PostRequest(RequestDto requestDto, [FromHeader] string Authorization)
        {
            JwtSecurityToken t = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Authorization.Substring(7));
            var x = t.Claims.ToList();


            if (x[0].Value != requestDto.Client_ID.ToString() && x[2].Value != "Client")
            {
                return Unauthorized();
            }

            if (requestDto == null)
            {
                return NotFound(new { message = "Request Is Not Found!" });
            }
            if (ModelState.IsValid)
            {
                var request = _mapper.Map<Request>(requestDto);
                if (!await _requestRepository.CheckRequestsByClienttoHandyman(request.Client_ID, request.Handyman_SSN))
                    return BadRequest(new {message="Request already Exists !"});
                var prevRequest = await _requestRepository.GetPrevRequest(request.Client_ID,0);
                if (prevRequest != null)
                {
                    var mappedPrevRequest = _mapper.Map<RequestDto>(prevRequest);
                    return CreatedAtAction("GetRequest", new { id = mappedPrevRequest.Request_ID }, mappedPrevRequest);
                }
                //request_date is the time for handyman arrival while Request_order_date is the time when request is made
                if(request.Request_Date < DateTime.Now.AddHours(2) && request.Request_Order_Date < DateTime.Now)
                    return BadRequest(new { message = "Invalid Request time!"});
                
                
                //check if request is from schedule or Now 
                if (request.Request_Date.Day >= DateTime.Now.AddDays(1).Day)
                    request.Request_Status = 0;

                _requestRepository.CreateRequest(request);
                try
                {
                    _requestRepository.CreatePaymentByRequestId(request);
                    await _requestRepository.SaveAllAsync();
                }
                catch
                {
                    return BadRequest(new { Error = "Can't Add This Request!" });
                }

                return CreatedAtAction("GetRequest", new { id = requestDto.Request_ID }, requestDto);
                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Request/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")] // tested
        public async Task<IActionResult> DeleteRequest(int id)
        {
            try
            {
               // _requestRepository.EditRequest(_mapper.Map<Request>(id));
                _requestRepository.DeleteRequestById(id);
                await _requestRepository.SaveAllAsync();
            }
            catch
            {
                return NotFound(new { message = "Request Is Not Found!" });
            }

            return NoContent();
        }
    }
}
