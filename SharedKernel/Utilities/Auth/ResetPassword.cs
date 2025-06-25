using System.Security.Cryptography;
using Domain.Options;
using Microsoft.Extensions.Options;

namespace Utilities.Auth;

public class ResetPassword(IOptions<JwtOptions> jwtOptions) : IResetPassword
{
	private readonly JwtOptions _jwtOptions = jwtOptions.Value;
	
	// public bool ValidateTokenAsync(PasswordResetToken? token)
	// {
		// return token != null && token.ExpiresAt > System.DateTime.UtcNow;
	// }
	
	public string GenerateResetToken()
	{
		return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
	}
	
	public int GetResetTokenExpirationMinutes()
	{
		return _jwtOptions.AccessTokenExpirationMinutes;
	}
}