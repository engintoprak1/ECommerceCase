namespace Domain.Concrete.Dtos.Auth;

public class TokenResponse
{
    public string token_type { get; set; }
    public DateTime? expires_in { get; set; }
    public string access_token { get; set; }
}
