using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vidly.Common;
using Vidly.Data;

namespace Vidly.Controllers.Api
{
    public class HomeController : ApiController
    {

        private readonly DataContext _dataContext;


        public HomeController()
        {
            _dataContext = new DataContext();
        }

        [HttpGet]
        public IHttpActionResult Index()
        {
            var customersCount = _dataContext.Customers.Count(); /// customer count
            var moviesCount = _dataContext.Movies.Count(); /// movies count
            var transactionsCount = _dataContext.Transactions.Count(); /// transaction all count
            var transactionsActiveCount = _dataContext.Transactions.Where(t => t.Status == "issued").Count(); /// transaction active count

            var recentTransactions = _dataContext.Transactions
                        .OrderByDescending(t => t.TransactionId)
                        .Skip(0)
                        .Take(10)
                        .ToList();

            var response = new
            {
                success = true,
                customersCount,
                moviesCount,
                transactionsCount,
                transactionsActiveCount,
                data = recentTransactions
            };
          

            return Ok(response); 
        }

        [Route("api/test")]
        [HttpGet]
        public IHttpActionResult Test()
        {

            return Ok(_dataContext.Transactions.Where(t => t.TransactionId == 1).FirstOrDefault());
            var record = _dataContext.Transactions
                .Where(t => t.DeletedDate == null)
                .Include(t => t.RentedMovies)
                .OrderByDescending(t => t.TransactionId)
                .Skip(0)
                .Take(2)
                .AsNoTracking()
                .ToList();

            return Ok(record);
        }
    }
}