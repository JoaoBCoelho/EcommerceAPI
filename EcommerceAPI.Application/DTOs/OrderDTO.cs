using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.DTOs
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public Guid CartId { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
    }
}
