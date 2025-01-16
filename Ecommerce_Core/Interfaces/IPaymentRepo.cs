using Ecommerce_Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.Interfaces
{
    public interface IPaymentRepo
    {
        Task<List<PaymentDto>> GetAllPaymentsAsync();
        Task<PaymentDto> GetPaymentByIdAsync(Guid id);
        Task<string> AddPaymentAsync(PaymentDto paymentDto);
        Task<string> UpdatePaymentAsync(Guid id, PaymentDto paymentDto);
        Task<string> DeletePaymentAsync(Guid id);
    }
}
