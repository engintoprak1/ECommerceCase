using Domain.Concrete.Dtos.Auth;
using Domain.Concrete.Entities.User;

namespace Business.Abstract;

public interface ITokenService
{
    Task<TokenResponse> CreateToken(ApplicationUser user);
}
