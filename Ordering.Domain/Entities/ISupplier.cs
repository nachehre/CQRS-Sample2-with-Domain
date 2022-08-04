using System;

namespace Ordering
{
    public interface ISupplier
    {
        decimal GetShippingCost(Quote order);

        string Name { get; }

        bool IsAvailableAt(DateTime dateTime);
    }
}
