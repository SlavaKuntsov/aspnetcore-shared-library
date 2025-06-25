
namespace Utilities.Auth;

public interface IResetPassword
{
	// public bool ValidateTokenAsync(PasswordResetToken? token);
	public string GenerateResetToken();
	public int GetResetTokenExpirationMinutes();
}