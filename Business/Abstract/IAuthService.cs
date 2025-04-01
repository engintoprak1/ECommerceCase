using Domain.Concrete.Dtos.Auth;
using Domain.Concrete.Dtos.Role;
using Domain.Results;

namespace Business.Abstract;

public interface IAuthService
{
    Task<IResult> RegisterAsync(RegisterDto request);
    Task<IDataResult<TokenResponse>> LoginAsync(LoginDto request);
    Task<IResult> AddRoleAsync(AddRoleDto request);
}
