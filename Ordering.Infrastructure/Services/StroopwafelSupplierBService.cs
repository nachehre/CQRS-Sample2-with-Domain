using Microsoft.Extensions.Options;
using Ordering.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ordering.Services
{
    public class StroopwafelSupplierBService : StroopwafelSupplierServiceBase, IStroopwafelSupplierService
    {
		private readonly SupplierB supplier;
		private readonly IOptions<SupplierBSettings> settings;

        public StroopwafelSupplierBService(IHttpClientWrapper httpClientWrapper,
                                           SupplierB supplier,
										   IOptions<SupplierBSettings> settings) : base(httpClientWrapper)
        {
			this.supplier = supplier;
			this.settings = settings;
		}

        public Quote GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            //bug
            if (!supplier.IsAvailableAt(DateTime.Now))
            {
                return null!;
            }

            var result = ExecuteGet(new Uri(settings.Value.ProductsUri));
            var json = result.RootElement.GetRawText();
            var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
            var stroopwafels = JsonSerializer.Deserialize<IList<Stroopwafel>>(json, options);

            var builder = new QuoteBuilder();
            return builder.CreateOrder(orderDetails, stroopwafels!, new SupplierB());
        }

        public void Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrder(quoteLines);
            ExecutePost(new Uri(settings.Value.OrderUri), order);
        }
    }
}
