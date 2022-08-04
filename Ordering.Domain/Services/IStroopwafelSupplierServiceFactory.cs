namespace Ordering.Domain.Services
{
	public interface IStroopwafelSupplierServiceFactory
	{
		IStroopwafelSupplierService GetSupplierService(ISupplier supplier);
	}
}
