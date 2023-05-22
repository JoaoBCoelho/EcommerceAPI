using EcommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendOrderConfirmationEmailAsync(string email);
    }
}
