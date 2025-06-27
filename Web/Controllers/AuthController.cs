using Application.DTO;
using Application.Interface;

using Microsoft.AspNetCore.Mvc;
using LoginRequest = Application.DTO.LoginRequest;
using RegisterRequest = Application.DTO.RegisterRequest;

namespace Web.Controllers.AuthController;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthServices _authServices;

    public AuthController(IAuthServices authServices)
    {
        _authServices = authServices;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest  loginRequest)
    {
       var Response =  await _authServices.LoginAsync(loginRequest);
       if (!Response.IsAuthenticated)
       {
           return BadRequest(Response.Message);
           
       }

       return Ok(Response);
       
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var Response = await _authServices.RegisterAsync(registerRequest);
        if (!Response.IsAuthenticated)
        {
            return BadRequest();
           
        }

        return Ok(Response);
    }

    [HttpPost("addrole")]
    public async Task<IActionResult> AddRole([FromBody] RoleRequest roleRequest)
    {
        var Response = await _authServices.AddRoleAsync(roleRequest);
        if (Response == string.Empty)
        {
            return BadRequest();
        }
        return Ok(Response);
             
    }
}