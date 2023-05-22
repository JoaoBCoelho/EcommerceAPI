using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers.Shared
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase { }
}
