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
                    UserId = p.UserId,
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
                UserId = payment.UserId,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                Amount = payment.Amount
            };
        }

        public async Task<string> AddPaymentAsync(PaymentDto paymentDto)
        {
            var order = await _context.Orders.FindAsync(paymentDto.OrderId);
            if (order == null) return "Order not found";

            // إضافة الدفع إلى قاعدة البيانات
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = paymentDto.OrderId,
                UserId = paymentDto.UserId,
                PaymentMethod = paymentDto.PaymentMethod,
                Amount = paymentDto.Amount,
                PaymentDate = DateTime.Now
            };

            await _context.Payments.AddAsync(payment);

            // التحقق إذا كان المبلغ المدفوع يساوي المبلغ الكلي للطلب
            var totalPaid = await _context.Payments
                                           .Where(p => p.OrderId == paymentDto.OrderId)
                                           .SumAsync(p => p.Amount);

            if (totalPaid >= order.TotalPrice)
            {
                // إذا تم دفع المبلغ بالكامل، نقوم بتحديث حالة الطلب إلى "مدفوع"
                order.Status = "Paid";
            }
            else
            {
                // إذا لم يتم دفع المبلغ بالكامل، يبقى "معلق"
                order.Status = "Pending";
            }

            await _context.SaveChangesAsync();

            return "Payment processed successfully";
        }


        public async Task<string> UpdatePaymentAsync(Guid id, PaymentDto paymentDto)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return "Payment not found";

            payment.OrderId = paymentDto.OrderId;
            payment.UserId = paymentDto.UserId;
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

        public async Task<List<PaymentDto>> GetPaymentsByUserIdAsync(int userId)
        {
            return await _context.Payments
                .Where(p => p.UserId == userId)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    OrderId = p.OrderId,
                    UserId = p.UserId,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    Amount = p.Amount
                })
                .ToListAsync();
        }

    }
}