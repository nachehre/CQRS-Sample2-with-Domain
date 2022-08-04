using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ordering.Domain.Services;
using Ordering.Infrastructure.Services;
using Ordering.Services;
using System;

namespace Ordering
{
	internal class StroopwafelSupplierServiceFactory : IStroopwafelSupplierServiceFactory
	{
		private readonly IServiceProvider serviceProvider;

		public StroopwafelSupplierServiceFactory(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public IStroopwafelSupplierService GetSupplierService(ISupplier supplier)
		{
			var htmlWrapper = serviceProvider.GetRequiredService<IHttpClientWrapper>();

			if (supplier is SupplierA)
				return new StroopwafelSupplierAService(htmlWrapper, serviceProvider.GetRequiredService<IOptions<SupplierASettings>>());
			else if (supplier is SupplierB supplierB)
				return new StroopwafelSupplierBService(htmlWrapper, supplierB, serviceProvider.GetRequiredService<IOptions<SupplierBSettings>>());
			else if (supplier is SupplierC)
				return new StroopwafelSupplierCService(htmlWrapper, serviceProvider.GetRequiredService<IOptions<SupplierCSettings>>());
			else
				throw new InvalidOperationException($"Invalid supplier type: {supplier?.GetType()}");
		}
	}
}
