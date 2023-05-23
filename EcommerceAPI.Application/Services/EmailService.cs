using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Interfaces;
using HandlebarsDotNet;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EcommerceAPI.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _email;
        private readonly string _password;
        private static string _smtpServer = "smtp-mail.outlook.com";
        private static int _smtpPort = 587;

        public EmailService(IConfiguration configuration)
        {
            _email = configuration["Email"];
            _password = configuration["EmailPassword"];
        }

        public void SendOrderConfirmationEmail(string to, OrderDTO orderDTO)
        {
            string htmlBody = GenerateOrderEmailTemplate(orderDTO);

            // Send the email
            SendEmail(to, $"Order {orderDTO.Id} Confirmation", htmlBody);
        }
        private void SendEmail(string to, string subject, string body)
        {
            using var message = new MailMessage(_email, to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using var client = new SmtpClient(_smtpServer, _smtpPort);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_email, _password);

            client.Send(message);
        }

        public string GenerateOrderEmailTemplate(OrderDTO order)
        {
            string template = $@"
            <html>
            <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f5f5f5;
                        }}

                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                            background-color: #fff;
                            border-radius: 5px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}

                        h1 {{
                            text-align: center;
                        }}

                        h2 {{
                            margin-bottom: 10px;
                        }}

                        table {{
                            width: 100%;
                            border-collapse: collapse;
                            margin-top: 20px;
                        }}

                        th, td {{
                            padding: 10px;
                            border-bottom: 1px solid #ddd;
                        }}

                        th {{
                            text-align: left;
                        }}

                        .text-center {{
                            text-align: center;
                        }}

                        .text-right {{
                            text-align: right;
                        }}
                    </style>
                </head>
            <body>
                <h1>Order Confirmation</h1>
                <h2>Order Details</h2>
                <p><strong>Order ID:</strong> {order.Id}</p>
                <p><strong>Customer Email:</strong> {order.CustomerEmail}</p>
                <h3>Billing Information</h3>
                <p><strong>Full Name:</strong> {order.BillingInformation.FullName}</p>
                <p><strong>Address:</strong> {order.BillingInformation.Address}</p>
                <p><strong>City:</strong> {order.BillingInformation.City}</p>
                <p><strong>State:</strong> {order.BillingInformation.State}</p>
                <p><strong>Postal Code:</strong> {order.BillingInformation.PostalCode}</p>
                <p><strong>Country:</strong> {order.BillingInformation.Country}</p>
                <h3>Shipping Information</h3>
                <p><strong>Full Name:</strong> {order.ShippingInformation.FullName}</p>
                <p><strong>Address:</strong> {order.ShippingInformation.Address}</p>
                <p><strong>City:</strong> {order.ShippingInformation.City}</p>
                <p><strong>State:</strong> {order.ShippingInformation.State}</p>
                <p><strong>Postal Code:</strong> {order.ShippingInformation.PostalCode}</p>
                <p><strong>Country:</strong> {order.ShippingInformation.Country}</p>
                <h3>Cart</h3>
                <table>
                    <tr>
                        <th>Product</th>
                        <th>Quantity</th>
                        <th>Price</th>
                    </tr>
                    {GenerateCartItemsHtml(order.Cart.Products)}
                </table>
                <p><strong>Total Amount:</strong> {order.TotalAmount}</p>
            </body>
            </html>";

            return template;
        }

        public string GenerateCartItemsHtml(IEnumerable<CartProductDTO> products)
        {
            string itemTemplate = @"
                <tr>
                    <td>{product.Product.Name}</td>
                    <td>{product.Quantity}</td>
                    <td>{product.Product.Price}</td>
                </tr>";

            StringBuilder itemsHtml = new StringBuilder();

            foreach (var product in products)
            {
                string itemHtml = itemTemplate.Replace("{product.Product.Name}", product.Product.Name)
                                              .Replace("{product.Quantity}", product.Quantity.ToString())
                                              .Replace("{product.Product.Price}", product.Product.Price.ToString());

                itemsHtml.Append(itemHtml);
            }

            return itemsHtml.ToString();
        }
    }
}
