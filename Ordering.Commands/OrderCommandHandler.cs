using Ordering.Domain.Repositories;
using Ordering.Domain.Services;

namespace Ordering.Commands
{
	public class OrderCommandHandler : IOrderCommandHandler
	{
		private readonly ISupplierRepository supplierRepository;
		private readonly IStroopwafelSupplierServiceFactory stroopwafelSupplierServiceFactory;

		public OrderCommandHandler(ISupplierRepository supplierRepository,
			IStroopwafelSupplierServiceFactory stroopwafelSupplierServiceFactory)
		{
			this.supplierRepository = supplierRepository;
			this.stroopwafelSupplierServiceFactory = stroopwafelSupplierServiceFactory;
		}

		public void Handle(OrderCommand command)
		{
			var supplier = supplierRepository.FindSupplierByName(command.Supplier);
			var service = stroopwafelSupplierServiceFactory.GetSupplierService(supplier);

			service.Order(command.OrderLines);
		}
	}
}
