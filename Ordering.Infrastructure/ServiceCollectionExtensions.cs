using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Services;
using Ordering.Infrastructure.Services;

namespace Ordering
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddOrderingInfrastructure(this IServiceCollection services,
											   IConfigurationSection supplierASection,
											   IConfigurationSection supplierBSection,
											   IConfigurationSection supplierCSection) 
		{
			services.AddScoped<IStroopwafelSupplierServiceFactory, StroopwafelSupplierServiceFactory>();

			services.Configure<SupplierASettings>(supplierASection);
			services.Configure<SupplierBSettings>(supplierBSection);
			services.Configure<SupplierCSettings>(supplierCSection);

			return services;
		}
	}
}
