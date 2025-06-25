using Domain.Enums;
using OnlineStore.Domain.Entities;

namespace Utilities.Auth;

public interface IJwt
{
	public string GenerateAccessToken(Guid id, Role role);

	public string GenerateRefreshToken();

	public Guid ValidateRefreshTokenAsync(RefreshToken? existRefreshToken);

	public int GetRefreshTokenExpirationDays();
}