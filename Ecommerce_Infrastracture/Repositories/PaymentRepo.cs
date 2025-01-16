using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Ecommerce_Core.Models;
using Ecommerce_Infrastracture.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Infrastracture.Repositories
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly AppDbContext _context;

        public PaymentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentDto>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    OrderId = p.OrderId,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    Amount = p.Amount
                })
                .ToListAsync();
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(Guid id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return null;

            return new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                Amount = payment.Amount
            };
        }

        public async Task<string> AddPaymentAsync(PaymentDto paymentDto)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = paymentDto.OrderId,
                PaymentDate = paymentDto.PaymentDate,
                PaymentMethod = paymentDto.PaymentMethod,
                Amount = paymentDto.Amount
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return "Payment added successfully";
        }

        public async Task<string> UpdatePaymentAsync(Guid id, PaymentDto paymentDto)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return "Payment not found";

            payment.OrderId = paymentDto.OrderId;
            payment.PaymentDate = paymentDto.PaymentDate;
            payment.PaymentMethod = paymentDto.PaymentMethod;
            payment.Amount = paymentDto.Amount;

            await _context.SaveChangesAsync();
            return "Payment updated successfully";
        }

        public async Task<string> DeletePaymentAsync(Guid id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return "Payment not found";

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return "Payment deleted successfully";
        }
    }
}
