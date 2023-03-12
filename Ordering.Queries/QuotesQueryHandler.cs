using Ordering.Domain.Repositories;
using Ordering.Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Queries
{
	public class QuotesQueryHandler : IQuotesQueryHandler
	{
		private readonly IStroopwafelSupplierServiceFactory stroopwafelSupplierServiceFactory;
		private readonly ISupplierRepository supplierRepository;

		public QuotesQueryHandler(ISupplierRepository supplierRepository,
							IStroopwafelSupplierServiceFactory stroopwafelSupplierServiceFactory)
		{
			this.supplierRepository = supplierRepository;
			this.stroopwafelSupplierServiceFactory = stroopwafelSupplierServiceFactory;
		}

		public IList<Quote> Handle(QuotesQuery query)
		{
			var suppliers = supplierRepository.GetAvailableSuppliers();
//To DO : Read From ..
			return suppliers
				.Select(supplier => stroopwafelSupplierServiceFactory
					.GetSupplierService(supplier)
					.GetQuote(query.OrderLines))
				.ToList();
		}
	}
}
