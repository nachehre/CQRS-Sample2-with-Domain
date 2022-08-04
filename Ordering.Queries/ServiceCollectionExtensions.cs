using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Queries
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddOrderingQueries(this IServiceCollection services)
		{
			services.AddScoped<IQuotesQueryHandler, QuotesQueryHandler>();

			return services;
		}
	}
}
