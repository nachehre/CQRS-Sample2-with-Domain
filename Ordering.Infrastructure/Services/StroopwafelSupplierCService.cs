using Microsoft.Extensions.Options;
using Ordering.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ordering.Services
{
	public class StroopwafelSupplierCService : StroopwafelSupplierServiceBase, IStroopwafelSupplierService
    {
		private readonly IOptions<SupplierCSettings> settings;

        public StroopwafelSupplierCService(IHttpClientWrapper httpClientWrapper, IOptions<SupplierCSettings> settings) : base(httpClientWrapper)
        {
			this.settings = settings;
		}

        public Quote GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var result = ExecuteGet(new Uri(settings.Value.ProductsUri));
            var json = result.RootElement.GetRawText();
            var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
            var stroopwafels = JsonSerializer.Deserialize<IList<Stroopwafel>>(json, options);

            var builder = new QuoteBuilder();

            return builder.CreateOrder(orderDetails, stroopwafels!, new SupplierC());
        }

        public void Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrder(quoteLines);
            ExecutePost(new Uri(settings.Value.OrderUri), order);
        }
    }
}
