using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Repository;
using Vidly.Data;
using Vidly.Contracts;
using Vidly.Common;
using Vidly.Models;
using Vidly.Exceptions;

namespace Vidly.Controllers.Api
{
   
    public class MoviesController : ApiController
    {
      
        private readonly IMovieRepository _repository;
        public MoviesController()
        {
            _repository = new MovieRepository(new DataContext());
        }
      
        [HttpGet]        
        public IHttpActionResult Index([FromUri] PaginatedRequest request)
        {
            try
            {
               return Ok(_repository.GetMovies(
                   page     : request.Page,
                   pageSize : PaginatedRequest.Page_Size,
                   filter   : request.Filter
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
                var response = new Response{
                    Success = true,
                    Data = _repository.GetById(id)
                };

                return Ok(response);

            }
            catch (MovieNotFoundException e)
            {
                return BadRequest(e.Message);
            }
                
        }

        [Route("api/movies/getByTitle")]
        [HttpGet]
        public IHttpActionResult GetByTitle([FromUri]string title)
        {
            try
            { 
                return Ok(new Response
                {
                    Success = true,
                    Data = _repository.GetByTitle(title)
                });
            }
            catch (MovieNotFoundException e)
            {
                
                return BadRequest(e.Message); 
            }
            
        }

        [Route("api/movies/create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] MovieRequest request)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);



            if (_repository.Exist(request.Title))
            {
                return Content(
                    HttpStatusCode.Conflict,
                    new { Message = "Movie already Exist" }
                );
            }


            var movie = _repository.Create(new Movie()
            {
                Title = request.Title,
                Description = request.Description,
                Genre = request.Genre,
                Price = request.Price,
                Copies = request.Copies,
                ReleaseDate = request.ReleaseDate,
                CreatedAt = DateTime.Now,
            });

            return Ok( new Response { 
                Success = movie != null,
                Data = movie
            });
           
        }

        [Route("api/movies/update/{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromUri] int id, [FromBody] MovieRequest request)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var movie = _repository.Update(id, new Movie()
                {
                    Title = request.Title,
                    Description = request.Description,
                    Genre = request.Genre,
                    Price = request.Price,
                    Copies = request.Copies,
                    ReleaseDate = request.ReleaseDate,
                });

                return Ok(new Response { 
                    Success = true,
                    Data = movie
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
             
            }
            
        }

        [Route("api/movies/delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (_repository.Delete(id) == false)
                    return BadRequest("failed to delete this movie");             

                return Ok(new Response
                {
                    Success = true,
                    Message = "Successfully Deleted"
                });
            }
            catch (MovieNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [Route("api/movies/delete/force/{id}")]
        [HttpDelete]
        public IHttpActionResult ForceDelete(int id)
        {
            try
            {
                if (_repository.ForceDelete(id) == false)
                    return BadRequest("failed to delete this movie");

                var response = new Response
                {
                    Success = true,
                    Message = "Successfully Deleted"
                };

                return Ok(response);
            }
            catch (MovieNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [Route("api/movies/search")]
        [HttpGet]       
        public IHttpActionResult Search([FromUri] string keyword, [FromUri] PaginatedRequest request)
        {
            try
            {
                return Ok(_repository.Search(keyword, request.Page, PaginatedRequest.Page_Size));
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

    }
}
