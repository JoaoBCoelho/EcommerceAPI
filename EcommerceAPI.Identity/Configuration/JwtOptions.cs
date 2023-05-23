using Microsoft.IdentityModel.Tokens;

namespace EcommerceAPI.Identity.Configuration
{
    public class JwtOptions
    {
        public string JwtOptionsIssuer { get; set; }
        public string JwtOptionsAudience { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public int JwtOptionsExpiration { get; set; }
    }
}
