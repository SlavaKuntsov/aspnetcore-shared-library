using System.Text;
using Domain.Enums;
using Common.Enums;
using Domain.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Common.Authorization;

public static class AuthorizationExtension
{
	public static IServiceCollection AddAuthorization(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
		services.Configure<EmailOptions>(configuration.GetSection(nameof(EmailOptions)));
		
		var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
		
		services
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
				options =>
				{
					options.RequireHttpsMetadata = true;
					options.SaveToken = true;
		
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
					};
		
					options.Events = new JwtBearerEvents
					{
						OnAuthenticationFailed = _ => Task.CompletedTask,
						OnTokenValidated = _ => Task.CompletedTask
					};
				});
		
		services.Configure<AuthorizationOptions>(
			configuration.GetSection(nameof(AuthorizationOptions)));

		services.AddCors(options =>
		{
			options.AddDefaultPolicy(policy =>
			{
				policy.AllowAnyHeader();
				policy.AllowAnyMethod();
				policy.AllowCredentials();
			});
		});

		services.AddAuthorizationBuilder()
			.AddPolicy(
				"Admin",
				policy =>
				{
					policy.RequireRole(Role.Admin.GetDescription());
					policy.AddRequirements(new ActiveAdminRequirement());
				})
			.AddPolicy(
				"User",
				policy =>
				{
					policy.RequireRole(Role.User.GetDescription());
				})
			.AddPolicy(
				"Guest",
				policy =>
				{
					policy.RequireRole(Role.Guest.GetDescription());
				})
			.AddPolicy(
				"All",
				policy =>
				{
					policy.RequireRole(
						Role.Admin.GetDescription(), 
						Role.User.GetDescription(),
						Role.Guest.GetDescription());
					policy.AddRequirements(new ActiveAdminRequirement());
				});

		services.AddScoped<IAuthorizationHandler, ActiveAdminHandler>();

		return services;
	}
}