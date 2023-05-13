using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Common;
using Vidly.Data;
using Vidly.Repository;

namespace Vidly.Controllers.Api
{
    public class RentedMovieController : ApiController
    {
        private readonly RentedMoviesRepository _repository;

        public RentedMovieController()
        {
            _repository = new RentedMoviesRepository(new DataContext());
        }


        [Route("api/rentedmovies/transaction/{id}")]
        [HttpGet]

        public IHttpActionResult GetRentedMovies([FromUri] int id)
        {
            try
            {             

                return Ok(new Response
                {
                    Success = true,
                    Data = _repository.GetByTransaction(id)
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
               
            }
           
        }


        [Route("api/rentedmovies/{id}")]
        [HttpPut]
        public IHttpActionResult ReturnMovie([FromUri] int id)
        {
            try
            {
                return Ok(new Response
                {
                    Success = true,
                    Message = "successfully returned",
                    Data = _repository.ReturnMovie(id)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }

        }


    }
}
