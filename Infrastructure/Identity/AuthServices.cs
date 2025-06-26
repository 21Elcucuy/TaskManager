using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTO;
using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.JWT;

namespace Infrastructure.Identity;

public class AuthServices : IAuthServices
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IOptions<JWT> _jwt;

    public AuthServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager , IOptions<JWT> jwt)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwt = jwt;
    }
    public async Task<AuthModel> LoginAsync(LoginRequest loginRequest)
    {
        
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if(user == null && ! await _userManager.CheckPasswordAsync(user,loginRequest.Password))
        {
            return new AuthModel()
            {
                IsAuthenticated = false,
                Message = "Invalid Email or password"
            };
        }        
        var jwtToken = await GetToken(user);
        var roles = await _userManager.GetRolesAsync(user);
        return new AuthModel()
        {
            IsAuthenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            Message = "User login successful",
            Username = user.UserName,
            Email = user.Email,
            Roles = roles.ToList(),
            ExpiresOn = jwtToken.ValidTo,
        };
    }

    public async Task<AuthModel> RegisterAsync(RegisterRequest registerRequest)
    {
        if (_userManager.FindByEmailAsync(registerRequest.Email).Result != null)
        {
            return new AuthModel() { Message = "Email Already Rgisterd", IsAuthenticated = false };

        }

        if (_userManager.FindByNameAsync(registerRequest.UserName).Result is not null)
        {
            return new AuthModel() { Message = "UserName Already Used", IsAuthenticated = false };
        }

        var User = new ApplicationUser()
        {
            Email = registerRequest.Email,
            UserName = registerRequest.UserName,
            FullName = registerRequest.FirstName + " " + registerRequest.LastName,
        };
        var Result = await _userManager.CreateAsync(User, registerRequest.Password);
        if (!Result.Succeeded)
        {
            return new AuthModel() { Message = "Somthing went Wrong", IsAuthenticated = false };
        }

        var jwtToken = await GetToken(User);
        return new AuthModel() 
            { Message = "User Register Successful", 
                IsAuthenticated = true ,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Username = User.UserName,
                Email = User.Email,
                Roles = new List<string>{"User"},
                ExpiresOn = jwtToken.ValidTo,
            };
        
    }

    public async Task<string> AddRoleAsync(RoleRequest roleRequest)
    {
        var user = await _userManager.FindByIdAsync(roleRequest.UserId);
        if (user is null || ! await _roleManager.RoleExistsAsync(roleRequest.RoleName))
            return "Invalid user ID or Role";
        if(await _userManager.IsInRoleAsync(user,roleRequest.RoleName))
            return "Role already exists";
        var Result = await _userManager.AddToRoleAsync(user, roleRequest.RoleName);
        if (Result.Succeeded)
            return string.Empty;
           
        return "something went wrong";
    }

    private async Task<JwtSecurityToken> GetToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaim = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaim.Add(new Claim("roles", role));
            
        }

        var Claim = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("uid", user.Id),
        }.Union(roleClaim).Union(userClaims);
        var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Value.Key));
        var SigningCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityKey = new JwtSecurityToken(
            issuer: _jwt.Value.Issuer,
            audience: _jwt.Value.Audience,
            claims: Claim,
            expires: DateTime.UtcNow.AddDays(Convert.ToDouble(_jwt.Value.DurationInDays)),
            signingCredentials: SigningCredentials
        );
        return jwtSecurityKey;
    }
}