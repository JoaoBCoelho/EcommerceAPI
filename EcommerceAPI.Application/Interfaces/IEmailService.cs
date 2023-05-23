using EcommerceAPI.Application.DTOs;

namespace EcommerceAPI.Application.Interfaces
{
    public interface IEmailService
    {
        void SendOrderConfirmationEmail(string to, OrderDTO orderDTO);
    }
}
