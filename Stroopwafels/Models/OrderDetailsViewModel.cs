using Ordering;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Stroopwafels.Models
{
    public class OrderDetailsViewModel
    {
        public IList<OrderRow> OrderRows { get; set; }

        //[Display(Name= "Full Name")]
        //public string FullName { get; set; }

        //[Display(Name = "Wish Date")]
        //public DateTime WishDate { get; set; }
    }
}
