using System.ComponentModel.DataAnnotations;

namespace Domain.Concrete.Dtos.Role;

public class AddRoleDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
}
