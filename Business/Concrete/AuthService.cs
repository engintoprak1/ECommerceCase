using Business.Abstract;
using Domain.Concrete.Dtos.Auth;
using Domain.Concrete.Dtos.Role;
using Domain.Concrete.Entities.User;
using Domain.Entities.User;
using Domain.Results;
using Microsoft.AspNetCore.Identity;

namespace Business.Concrete;

public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService) : IAuthService
{
    public async Task<IResult> RegisterAsync(RegisterDto request)
    {
        var user = new ApplicationUser { UserName = request.Email, FirstName = request.Firstname, LastName = request.Lastname, Email = request.Email, PhoneNumber = request.PhoneNumber, EmailConfirmed = true };
        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return new SuccessResult("User has been created is successfully.");
        }
            
        return new ErrorResult("Registration failed: " + string.Join(" ", result.Errors.Select(x => x.Description).ToList()));
    }

    public async Task<IDataResult<TokenResponse>> LoginAsync(LoginDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email) ?? throw new Exception("Invalid username or password.");

        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return new ErrorDataResult<TokenResponse>("Invalid username or password.");

        return new SuccessDataResult<TokenResponse>(await tokenService.CreateToken(user));
    }

    public async Task<IResult> AddRoleAsync(AddRoleDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return new ErrorResult($"No Accounts Registered with {request.Email}.");
        }

        if (await userManager.CheckPasswordAsync(user, request.Password))
        {
            var roleExists = Enum.GetNames(typeof(Authorization.Roles))
                .Any(x => x.ToLower() == request.Role.ToLower());

            if (roleExists)
            {
                var validRole = Enum.GetValues(typeof(Authorization.Roles)).Cast<Authorization.Roles>()
                    .Where(x => x.ToString().ToLower() == request.Role.ToLower())
                    .FirstOrDefault();

                await userManager.AddToRoleAsync(user, validRole.ToString());

                return new SuccessResult($"Added {request.Role} to user {request.Email}.");
            }

            return new ErrorResult($"Role {request.Role} not found.");
        }

        return new ErrorResult($"Incorrect Credentials for user {user.Email}.");
    }
}
