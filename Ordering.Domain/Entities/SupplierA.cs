﻿using System;

namespace Ordering
{
	public class SupplierA : ISupplier
    {
        public decimal GetShippingCost(Quote order)
        {
            return 5m;
        }

        public string Name => "Leverancier A";

        public bool IsAvailableAt(DateTime dateTime) => true;
	}
}
