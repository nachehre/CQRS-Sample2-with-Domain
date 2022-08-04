using System;
using System.Linq;

namespace Ordering
{
	public class SupplierB : ISupplier
    {
        private const decimal ShippingCostLimit = 50m;
        private const decimal ShippingCostAboveLimit = 0m;
        private const decimal ShippingCostUnderLimit = 5m;
        private DateTime[] PublicHolidays { get; set; } = new[] 
        {
            new DateTime(2016, 1, 1),
            new DateTime(2016, 12, 25),
            new DateTime(2016, 12, 26)
        };
        public decimal GetShippingCost(Quote order)
        {
            return order.TotalWithoutShippingCost > ShippingCostLimit ? ShippingCostAboveLimit : ShippingCostUnderLimit;
        }

        public string Name => "Leverancier B";

		public bool IsAvailableAt(DateTime dateTime)
        {
            if (dateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            var isHoliday = PublicHolidays.Any(h => h == dateTime);
            return !isHoliday;
        }
    }
}
