using Application.DTO;

namespace Application.Interface;

public interface IAuthServices
{
    public Task<AuthModel> LoginAsync(LoginRequest loginRequest);
    public Task<AuthModel> RegisterAsync(RegisterRequest registerRequest);
    public Task<string> AddRoleAsync(RoleRequest roleRequest);
    
}