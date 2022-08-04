using System.Collections.Generic;
using System.Linq;

namespace Ordering
{
    public class Quote
    {
        //Added peter 1 euro earned here
        public decimal TotalPrice => TotalWithoutShippingCost + Supplier.GetShippingCost(this)+1;

        public string TotalPricePresentation => TotalPrice.ToString("C");

        public decimal TotalWithoutShippingCost
        {
            get { return OrderLines.Sum(o => o.Price); }
        }

        public IList<QuoteLine> OrderLines { get; }

        public ISupplier Supplier { get; }

        public Quote(IList<QuoteLine> orderLines, ISupplier supplier)
        {
            OrderLines = orderLines;
            Supplier = supplier;
        }
    }
}
