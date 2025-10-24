using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Common.Swagger;

public static class SwaggerExtension
{
	public static IServiceCollection AddSwagger(this IServiceCollection services)
	{
		var apiVersioningBuilder = services.AddApiVersioning(
			o =>
			{
				o.AssumeDefaultVersionWhenUnspecified = true;
				o.DefaultApiVersion = new ApiVersion(1, 0);
				o.ReportApiVersions = true;
				o.ApiVersionReader = new UrlSegmentApiVersionReader();
			});

		apiVersioningBuilder.AddApiExplorer(
			options =>
			{
				options.GroupNameFormat = "'v'VVV";
				options.SubstituteApiVersionInUrl = true;
			});
		
		services.AddSwaggerGen(
			options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API v1", Version = "v1" });

				options.ExampleFilters();

				options.AddSecurityDefinition(
					"Bearer",
					new OpenApiSecurityScheme
					{
						Name = "Authorization",
						In = ParameterLocation.Header,
						Type = SecuritySchemeType.Http,
						Scheme = "Bearer",
						BearerFormat = "JWT",
						Description = "Введите токен JWT в формате 'Bearer {токен}'"
					});

				options.AddSecurityRequirement(
					new OpenApiSecurityRequirement
					{
						{
							new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							Array.Empty<string>()
						}
					});
			});

		return services;
	}
}