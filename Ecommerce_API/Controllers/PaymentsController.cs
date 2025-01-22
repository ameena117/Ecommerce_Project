using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRepo _paymentRepo;

        public PaymentsController(IPaymentRepo paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }

        [HttpGet("get-all-payments")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentRepo.GetAllPaymentsAsync();
            if (payments.Count == 0) return NoContent();

            return Ok(payments);
        }

        [HttpGet("get-payment-by-id/{id}")]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
            var payment = await _paymentRepo.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound("Payment not found");

            return Ok(payment);
        }

        [HttpPost("add-payment")]
        public async Task<IActionResult> AddPayment([FromBody] PaymentDto paymentDto)
        {
            var result = await _paymentRepo.AddPaymentAsync(paymentDto);
            return Ok(result);
        }

        [HttpPut("update-payment/{id}")]
        public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] PaymentDto paymentDto)
        {
            var result = await _paymentRepo.UpdatePaymentAsync(id, paymentDto);
            if (result == "Payment not found") return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("delete-payment/{id}")]
        public async Task<IActionResult> DeletePayment(Guid id)
        {
            var result = await _paymentRepo.DeletePaymentAsync(id);
            if (result == "Payment not found") return NotFound(result);

            return Ok(result);
        }

        [HttpGet("get-user-payments/{userId}")]
        public async Task<IActionResult> GetUserPayments(int userId)
        {
            var payments = await _paymentRepo.GetPaymentsByUserIdAsync(userId);
            if (payments.Count == 0) return NoContent();

            return Ok(payments);
        }

    }
}
