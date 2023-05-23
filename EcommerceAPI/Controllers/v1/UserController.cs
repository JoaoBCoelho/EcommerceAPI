using EcommerceAPI.Application.DTOs.Identity;
using EcommerceAPI.Application.Interfaces;
using EcommerceAPI.Controllers.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UserController : ApiControllerBase
    {
        private readonly IIdentityService _identityService;

        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<CreateUserResponseDTO>> CreateUser(CreateUserRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _identityService.CreateUserAsync(request);

            return ResolveHttpResponse(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _identityService.LoginAsync(request);

            return ResolveHttpResponse(result);
        }

        private ActionResult ResolveHttpResponse(BaseResponseDTO response)
        {
            if (response.Success)
                return Ok(response);
            else if (response.Errors.Any())
                return BadRequest(response);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
