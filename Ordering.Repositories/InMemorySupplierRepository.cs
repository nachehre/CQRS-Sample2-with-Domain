using Ordering.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Repositories
{
	internal class InMemorySupplierRepository  : ISupplierRepository
	{
		private readonly ISupplier[] suppliers;

		public InMemorySupplierRepository(params ISupplier[] suppliers)
		{
			this.suppliers = suppliers;
		}

		public IEnumerable<ISupplier> GetAvailableSuppliers()
		{
			var now = DateTime.Now;

			return suppliers.Where(x => x.IsAvailableAt(now));
		}
		public ISupplier FindSupplierByName(string supplierName)
			=> suppliers.FirstOrDefault(x => 
				string.Equals(x.Name, supplierName, StringComparison.InvariantCultureIgnoreCase));
	}
}