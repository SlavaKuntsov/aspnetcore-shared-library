using System.Net;
using System.Net.Mail;
using System.Text;
using Domain.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Utilities.Services;

public class EmailService(
	IOptions<EmailOptions> emailOptions,
	ILogger<EmailService> logger) : IEmailService
{
	private readonly EmailOptions _emailOptions = emailOptions.Value;

	public async Task SendEmailAsync(string recipient, string subject, string body)
	{
		var email = _emailOptions.Email != ""
			? _emailOptions.Email
			: Environment.GetEnvironmentVariable("EMAIL_EMAIL");

		var password = _emailOptions.Password != ""
			? _emailOptions.Email
			: Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

		using SmtpClient smtpClient = new(_emailOptions.Server, _emailOptions.Port);
		smtpClient.Credentials = new NetworkCredential(email, password);
		smtpClient.EnableSsl = true;

		using MailMessage mailMessage = new();
		mailMessage.From = new MailAddress(email!);
		mailMessage.To.Add($"{recipient}");
		mailMessage.Subject = subject;

		mailMessage.IsBodyHtml = true;
		StringBuilder htmlBody = new();
		htmlBody.Append("<!DOCTYPE html>");
		htmlBody.Append("<html lang=\"en\">");
		htmlBody.Append("<head><meta charset=\"UTF-8\"><title>Email Template</title></head>");
		htmlBody.Append("<body>");
		htmlBody.Append($"<h2>{body}</h2>");
		htmlBody.Append("</body>");
		htmlBody.Append("</html>");

		mailMessage.Body = htmlBody.ToString();

		try
		{
			await smtpClient.SendMailAsync(mailMessage);
		}
		catch (Exception ex)
		{
			logger.LogError("Ошибка отправки сообщения: {Message}", ex.Message);
		}
	}
}