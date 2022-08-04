using System.Collections.Generic;

namespace Ordering.Domain.Repositories
{
	public interface ISupplierRepository
	{
		IEnumerable<ISupplier> GetAvailableSuppliers();
		ISupplier FindSupplierByName(string supplierName);
	}
}
