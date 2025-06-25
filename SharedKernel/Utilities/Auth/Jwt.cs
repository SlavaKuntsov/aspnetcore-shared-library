using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Common.Authorization;
using Common.Enums;
using Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Domain.Entities;

namespace Utilities.Auth;

public class Jwt(IOptions<JwtOptions> jwtOptions) : IJwt
{
	private readonly JwtOptions _jwtOptions = jwtOptions.Value;

	public string GenerateAccessToken(Guid id, Role role)
	{
		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, id.ToString()),
			new Claim(ClaimTypes.Role, role.GetDescription())
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			claims: claims,
			expires: System.DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpirationMinutes),
			signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public string GenerateRefreshToken()
	{
		var randomBytes = new byte[64];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomBytes);

		return Convert.ToBase64String(randomBytes);
	}

	public int GetRefreshTokenExpirationDays()
	{
		return _jwtOptions.RefreshTokenExpirationDays;
	}

	public Guid ValidateRefreshTokenAsync(RefreshToken? existRefreshToken)
	{
		if (existRefreshToken?.UserId == null || existRefreshToken.IsRevoked || existRefreshToken.ExpiresAt < System.DateTime.UtcNow)
			return Guid.Empty;

		return existRefreshToken.UserId.Value;
	}
}