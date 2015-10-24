using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookByText.Models
{
    public class SubscriptionModel
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public int BookId { get; set; }
    }
}