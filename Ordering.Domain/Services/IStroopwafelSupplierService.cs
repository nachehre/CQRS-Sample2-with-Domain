﻿using System.Collections.Generic;

namespace Ordering
{
    public interface IStroopwafelSupplierService
    {
        Quote GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails);

        void Order(IList<KeyValuePair<StroopwafelType, int>> quote);
    }
}
