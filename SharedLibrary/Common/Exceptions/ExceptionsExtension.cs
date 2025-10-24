using Common.Exceptions.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Exceptions;

public static class ExceptionsExtension
{
	public static IServiceCollection AddExceptions(this IServiceCollection services)
	{
		services.AddExceptionHandler<GlobalExceptionHandler>();

		return services;
	}
}