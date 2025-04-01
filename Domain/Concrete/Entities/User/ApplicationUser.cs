using Microsoft.AspNetCore.Identity;

namespace Domain.Concrete.Entities.User;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
