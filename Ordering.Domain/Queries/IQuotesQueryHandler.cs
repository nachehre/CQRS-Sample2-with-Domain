using System.Collections.Generic;

namespace Ordering.Queries
{
	public interface IQuotesQueryHandler
	{
		IList<Quote> Handle(QuotesQuery query);
	}
}