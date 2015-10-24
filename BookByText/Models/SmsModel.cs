using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookByText.Models
{
    public class SmsModel
    {
        [Required]
        public long to { get; set; }
        [Required]
        public long from { get; set; }
        [Required]
        public string content { get; set; }
    }
}