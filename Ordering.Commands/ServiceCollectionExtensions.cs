using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Commands
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddOrderingCommands(this IServiceCollection services)
		{
			services.AddScoped<IOrderCommandHandler, OrderCommandHandler>();

			return services;
		}
	}
}
