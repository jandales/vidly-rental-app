using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Common;
using Vidly.Contracts;
using Vidly.Data;
using Vidly.Repository;
using Vidly.Models;
using Vidly.Common.Request;
using Vidly.Exceptions;
using System.Data.Entity;
using Newtonsoft.Json;

namespace Vidly.Controllers.Api
{
    public class TransactionsController : ApiController
    {
        private readonly TransactionRepository _repository;

        public TransactionsController()
        {
            _repository = new TransactionRepository(new DataContext());
        }

        [HttpGet]
        public IHttpActionResult GetAll([FromUri] PaginatedRequest request)
        {            
            try
            {
                return Ok(  _repository.GetAll(
                      page : request.Page,
                      pageSize : PaginatedRequest.Page_Size,
                      filter : request.Filter
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var transaction = _repository.GetById(id);

                var response = new Response
                {
                    Data = transaction,
                    Success = transaction != null,
                };

                return Ok(response);
            }
            catch (TransactionNotFoundExeption ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Route("api/transactions/create")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] TransactionRequest request)
        {
            try
            {
                var transaction = new Transaction()
                {
                    TransactionNumber = DateTime.Now.ToFileTimeUtc().ToString(),
                    CustomerId = request.CustomerId,
                    RentDate = request.RentDate,
                    DueDate = request.DueDate,
                    TotalAmount = request.TotalAmount,
                    TotalItems = request.TotalItems,
                    CreatedDate = DateTime.Now,
                    Status = "issued"
                };

                var result = _repository.Add(transaction, request.Movies);

                var response = new Response {
                   Success = result,
                   Message = result ? "Transaction Successfully Created" : "Transaction failed to create"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
           
        }
  

        [Route("api/transactions/cancel/{id}")]
        [HttpPut]
        public IHttpActionResult Cancel([FromUri] int id)
        {
            try
            {
                var result = _repository.Cancel(id);

                var response = new Response
                {
                    Success = result,
                    Message = result ? "Transaction Successfully Cancelled" : "Transaction failed to update"
                };

                return Ok(response);
            }
            catch (TransactionNotFoundExeption e)
            {
                return BadRequest(e.Message);              
            }
            catch (TransactionNotIssuedException e)
            {
                return BadRequest(e.Message);
            }


        }

        [Route("api/transactions/delete/force/{id}")]
        [HttpDelete]
        public IHttpActionResult ForceDelete(int id)
        {
            _repository.ForceDelete(id);
            return Ok("Successfully Deleted");
        }

        [HttpGet]
        [Route("api/transactions/search")]
        public IHttpActionResult Search([FromUri] string keyword, [FromUri] PaginatedRequest request)
        {
            try
            {
                return Ok(_repository.Search(
                   keyword,
                   request.Page,
                   PaginatedRequest.Page_Size
               ));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("api/transactions/movie")]
        public IHttpActionResult GetMovie([FromUri] string keyword)
        {
            try
            {
                var movie = _repository.GetMovie(keyword);

                if (movie.Copies <= 0)
                    throw new MovieOutOfStockException("Movie is out of stock");

                var response = new Response {
                    Success =  true,
                    Data = _repository.GetMovie(keyword)
                };

                return Ok(response);
            }
            catch (MovieNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (MovieOutOfStockException ex)
            {
                return BadRequest(ex.Message);
            }

        } 

    }
}
