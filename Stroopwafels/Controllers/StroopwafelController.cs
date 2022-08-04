using Microsoft.AspNetCore.Mvc;
using Ordering;
using Ordering.Commands;
using Ordering.Queries;
using Ordering.Services;
using Stroopwafels.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Stroopwafels.Controllers
{
    public class StroopwafelController : Controller
    {
        private readonly IQuotesQueryHandler _quotesQueryHandler;
        private readonly IOrderCommandHandler _orderCommandHandler;

		public StroopwafelController(IQuotesQueryHandler quotesQueryHandler,
									 IOrderCommandHandler orderCommandHandler)
        {
			_quotesQueryHandler = quotesQueryHandler;
			_orderCommandHandler = orderCommandHandler;
		}

        public ActionResult Index()
        {
            var viewModel = new OrderDetailsViewModel
            {

                OrderRows = new List<OrderRow>
                {
                    new OrderRow(0, StroopwafelType.Gewoon),
                    new OrderRow(0, StroopwafelType.Suikervrij),
                    new OrderRow(0, StroopwafelType.Super)
                },
                //FullName = string.Empty,
               // WishDate= DateTime.Now
               
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GetQuotes(OrderDetailsViewModel formModel)
        {
            //bool dayIsHoliday = IsHoliday(formModel.WishDate);

            if (!ModelState.IsValid /*|| dayIsHoliday*/)
            {

                return RedirectToAction("Index");
            }
            //else
            //{
            //    ModelState.Clear();
            //}

            var orderDetails = GetOrderDetails(formModel.OrderRows);
            var quotes = GetQuotesFor(orderDetails);

            var viewModel = new QuoteViewModel();
            foreach (var quote in quotes)
            {
               
                viewModel.Quotes.Add(new Models.Quote
                {
                    SupplierName = quote.Supplier.Name,
                    TotalAmount = quote.TotalPricePresentation
                }); ;
            }

            viewModel.OrderRows = formModel.OrderRows;
            viewModel.SelectedSupplier = quotes.OrderBy(q => q.TotalPrice).First().Supplier.Name;

            return View(viewModel);
        }

        private IList<Ordering.Quote> GetQuotesFor(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var query = new QuotesQuery(orderDetails);
            var orders = _quotesQueryHandler.Handle(query);

            return orders;
        }


        private static IList<KeyValuePair<StroopwafelType, int>> GetOrderDetails(IEnumerable<OrderRow> orderRows)
        {
            return orderRows
                .Select(orderRow => new KeyValuePair<StroopwafelType, int>(orderRow.Type, orderRow.Amount))
                .ToList();
        }

        public ActionResult Order(QuoteViewModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return Index();
            }

            var orderDetails = GetOrderDetails(formModel.OrderRows);

            var command = new OrderCommand(orderDetails, formModel.SelectedSupplier);
            _orderCommandHandler.Handle(command);

            return View();

        }

        public bool IsHoliday(DateTime wishDate)
        {
            return wishDate.DayOfWeek == DayOfWeek.Sunday;
            

        }
    }
}
