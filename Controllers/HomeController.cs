using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuoteTracker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //add context variable
        private QuoteDbContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, QuoteDbContext con)
        {
            _logger = logger;
            context = con;
        }


        //return index view with all quotes from the db
        public IActionResult Index()
        {
            return View(context.Quotes);
        }



        //add quote actions
        [HttpGet]
        public IActionResult AddQuote()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddQuote(Quote quote)
        {
            if (ModelState.IsValid)
            {
                context.Quotes.Add(quote);
                context.SaveChanges();
                return View("Index", context.Quotes);
            }
            else
            {
                return View();
            }
        }



        //edit quote actions
        [HttpGet]
        public IActionResult EditQuote(int quoteid)
        {
            Quote quote = context.Quotes.FirstOrDefault(q => q.QuoteId == quoteid);

            return View(quote);
        }

        [HttpPost]
        public IActionResult EditQuote(Quote quote)
        {
            if (ModelState.IsValid)
            {
                context.Quotes.Update(quote);
                context.SaveChanges();

                return View("Index", context.Quotes);
            }
            else
            {
                return View();
            }
        }



        //delete quote action
        public IActionResult DeleteQuote(int quoteid)
        {
            context.Quotes.Remove(context.Quotes.FirstOrDefault(q => q.QuoteId == quoteid));
            context.SaveChanges();

            return View("Index", context.Quotes);
        }




        //random quote action
        public IActionResult RandomQuote()
        {
            //get random entry from table
            List<Quote> quoteList = new List<Quote>(context.Quotes);
            int listLength = quoteList.Count();

            Random r = new Random();
            int rInt = r.Next(0, listLength);

            Quote randomQuote = quoteList[rInt];

            //return view with selected random quote
            return View(randomQuote);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
