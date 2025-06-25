namespace Domain.Options;

public class JwtOptions
{
	public string SecretKey { get; set; } = string.Empty;
	public int AccessTokenExpirationMinutes { get; set; }
	public int RefreshTokenExpirationDays { get; set; }
}