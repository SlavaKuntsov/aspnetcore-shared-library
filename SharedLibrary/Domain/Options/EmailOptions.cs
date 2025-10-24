namespace Domain.Options;

public class EmailOptions
{
	public string Server { get; set; } = string.Empty;
	public int Port { get; set; }
	public string? Email { get; set; } 
	public string? Password { get; set; }
}