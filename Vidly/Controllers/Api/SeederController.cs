using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vidly.Common;
using Vidly.Data;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class SeederController : ApiController
    {
        private readonly DataContext _dataContext;
        public SeederController()
        {
            _dataContext = new DataContext();
        }
       
        [Route("api/seeder/movies/{count}")]
        [HttpPost]
        public IHttpActionResult SeedMovie([FromUri] int count)
        {


            for (int i = 0; i < count; i++)
            {
                var movie = new Movie()
                {
                    Title = $"Movie {i}",
                    Description = $"description {i}",
                    Genre = "Action",
                    Price = 500,
                    Copies = 100,
                    ReleaseDate = DateTime.Now,
                    CreatedAt = DateTime.Now,
                };

                _dataContext.Movies.Add(movie);
                _dataContext.SaveChanges();

            }

            return Ok();
        }

        [Route("api/seeder/customers/{count}")]
        [HttpPost]
        public IHttpActionResult SeedCustomers([FromUri] int count)
        {

            for (int i = 0; i < count; i++)
            {

                var customer = new Customer()
                {
                    LastName = $"Andales {i}",
                    FirstName = $"Jesus {i}",
                    Gender = "Male",
                    Email = $"jesusandales{i}@gmail.com",
                    Address = $"Purok {i} Allen Nothern Samar",
                    PhoneNumber = $"0934141111{i}",
                    BirthDate = DateTime.Now,
                    CreatedAt = DateTime.Now,
                };

                _dataContext.Customers.Add(customer);
                _dataContext.SaveChanges();

            }

            return Ok();
        }

        [Route("api/seeder/transactions/{count}")]
        [HttpPost]
        public IHttpActionResult SeedTransactions([FromUri] int count)
        {

            for (int i = 0; i < count; i++)
            {

                var transaction = new Transaction()
                {
                    TransactionNumber = DateTime.Now.ToString("yyyyMMddHHmmss")+$"{i}",
                    CustomerId = new Random().Next(1, 100),
                    TotalAmount = 200,
                    TotalItems = 1,
                    RentDate = DateTime.Now,
                    ReturnDate = DateTime.Now.AddDays(5),
                    CreatedDate = DateTime.Now,
                };      

                _dataContext.Transactions.Add(transaction);
                _dataContext.SaveChanges();

            }

            return Ok();
        }

        public IHttpActionResult  GetMovies([FromUri] int page = 1, int pageSize = 10)
        {
            var currentPage = ((page <= 0 ? 1 : page) - 1) * pageSize;

            var count = _dataContext.Movies.Count();

            var record = _dataContext.Movies
                        .OrderByDescending(m => m.MovieId)                     
                        .Skip(currentPage)
                        .Take(pageSize)
                        .ToList();

            return Ok( new 
            {
                Page = page,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize),
                Data = record,
            });
         
        }
    }
}