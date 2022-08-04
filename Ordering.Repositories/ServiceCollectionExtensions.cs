using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Repositories;

namespace Ordering.Repositories
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddOrderingRepositories(this IServiceCollection services, params ISupplier[] validSuppliers)
		{
			services.AddScoped<ISupplierRepository>(sp => new InMemorySupplierRepository(validSuppliers));

			return services;
		}
	}
}
