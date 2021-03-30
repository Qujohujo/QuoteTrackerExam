using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteTracker.Models
{
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string AuthorFirstName { get; set; }

        [Required]
        public string AuthorLastName { get; set; }

        //date field here
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string? Subject { get; set; }

        public string? Citation { get; set; }
    }
}
