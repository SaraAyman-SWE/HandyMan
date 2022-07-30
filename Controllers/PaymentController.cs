using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandyMan.Data;
using HandyMan.Models;
using HandyMan.Dtos;
using AutoMapper;
using HandyMan.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HandyMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        // GET: api/Payment
        [HttpGet]
        [Authorize(Policy = "Admin")] // tested
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
        {
            try
            {
                var payments = await _paymentRepository.GetPaymentAsync();
                var paymentsToReturn = _mapper.Map<IEnumerable<PaymentDto>>(payments);
                return Ok(paymentsToReturn);
            }
            catch
            {
                return NotFound(new { message = "Empty!" });
            }
            
             
        }



        // GET: api/Payment/5
        [HttpGet("{id}")]
        [Authorize(Policy ="Request")] // tested
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentByIdAsync(id);
                var paymentToReturn = _mapper.Map<PaymentDto>(payment);
                return paymentToReturn;
            }
            catch
            {
                return NotFound();
            }
            
        }

        [HttpGet("Request/{id}")]
        [Authorize(Policy = "Request")] // tested
        public async Task<ActionResult<PaymentDto>> GetPaymentByRequestId(int id)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentByRequestIdAsync(id);

                if (payment == null)
                {
                    return NotFound();
                }

                var paymentToReturn = _mapper.Map<PaymentDto>(payment);
                return paymentToReturn;
            }
            catch
            {
                return NotFound();
            }

        }

        // PUT: api/Payment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy ="Admin")] // tested
        public async Task<IActionResult> EditPayment(int id, PaymentDto paymentDto)
        {
            if (id != paymentDto.Payment_ID)
            {
                return BadRequest();
            }
            var payment = _mapper.Map<Payment>(paymentDto);
            _paymentRepository.EditPayment(payment);

            try
            {
                await _paymentRepository.SaveAllAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Payment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy ="Admin")] // tested
        public async Task<ActionResult<PaymentDto>> PostPayment(PaymentDto paymentDto)
        {
            
            if (paymentDto == null)
                return NotFound(new { message = "Payment Is Not Found!" });
            if (ModelState.IsValid)
            {
                try
                {
                    var payment = _mapper.Map<Payment>(paymentDto);
                    _paymentRepository.CreatePayment(payment);
                    await _paymentRepository.SaveAllAsync();
                }
                catch
                {
                    return BadRequest(new { Error = "Can't Add This Payment!" });
                }
                return CreatedAtAction("GetPayment", new { id = paymentDto.Payment_ID }, paymentDto);
            }
            else return BadRequest(ModelState);

        }

        // DELETE: api/Payment/5
        [HttpDelete("{id}")]
        [Authorize(Policy ="Admin")] // tested
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                _paymentRepository.DeletePaymentById(id);
                await _paymentRepository.SaveAllAsync();
            }
            catch
            {
                return NotFound(new { message = "Payment Is Not Found!" });
            }
            return NoContent();
        }

        [HttpDelete("Request/{id}")]
        [Authorize(Policy ="Admin")] // tested
        public async Task<IActionResult> DeletePaymentByRequestId(int id)
        {
            try
            {
                _paymentRepository.DeletePaymentByRequestId(id);
                await _paymentRepository.SaveAllAsync();
            }
            catch
            {
                return NotFound(new { message = "Payment Is Not Found!" });
            }
            return NoContent();
        }
    }
}
