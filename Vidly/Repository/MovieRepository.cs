using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Common;
using Vidly.Contracts;
using Vidly.Data;
using Vidly.Exceptions;
using Vidly.Models;

namespace Vidly.Repository
{
    public class MovieRepository : IMovieRepository, IDisposable
    {
        private readonly DataContext _dataContext;
        public MovieRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Paginated<Movie> GetMovies(int page, int pageSize, string filter = "all")
        {

            var currentPage = ((page <= 0 ? 1 : page) - 1) * pageSize;

            var count = _dataContext.Movies.Where(c => c.DeletedAt == null).Count();

            var query = _dataContext.Movies.AsQueryable();
        

            if (filter != "all")
            {              
                query = query.Where(m => m.Genre == filter);
            }

            var record = query
                        .OrderByDescending(m => m.MovieId)
                        .Where(m => m.DeletedAt == null)                        
                        .Skip(currentPage)
                        .Take(pageSize)
                        .ToList();

         

            return new Paginated<Movie>()
            {
                Page = page,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize),
                Data = record,
            };

        }

        public Movie Update(int id, Movie movie)
        {
            var entity = GetById(id);

            entity.Title = movie.Title;
            entity.Description = movie.Description;
            entity.Price = movie.Price;
            entity.Genre = movie.Genre;
            entity.Copies = movie.Copies;
            entity.UpdatedAt = DateTime.Now;

            _dataContext.SaveChanges();

            return entity;
        }

        public Movie Create(Movie movie)
        {
            _dataContext.Movies.Add(movie);
            _dataContext.SaveChanges();
            return movie;
        }

        public bool Delete(int id)
        {
            var entity = this.GetById(id);

            if(entity == null)
            {
                throw new MovieNotFoundException("Movie not found");                
            }

            entity.DeletedAt = DateTime.Now;

            return _dataContext.SaveChanges() > 0;
        }

        public bool ForceDelete(int id)
        {
            var entity = GetById(id);
            _dataContext.Movies.Remove(entity);
            return _dataContext.SaveChanges() > 0 ? true : false;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Movie GetById(int id)
        {
            Movie movie  = _dataContext.Movies.Where(m => m.MovieId == id).FirstOrDefault();

            if (movie == null)
            {
                throw new MovieNotFoundException("Movie not found");
            }

            return movie;
        }

        public Movie GetByTitle(string title)
        {
            Movie movie = _dataContext.Movies.Where(m => m.Title == title).FirstOrDefault();

            if (movie == null)
            {
                throw new MovieNotFoundException("Movie not found");
            }

            return movie;
        }

       

        public SearchResult<Movie> Search(string keyword, int page, int pageSize)
        {          

            var record = _dataContext.Movies
                         .Where(m => m.Title.Contains(keyword ?? string.Empty)) 
                         .OrderByDescending(m => m.MovieId)                  
                         .ToList();

            var count = record.Count();

            return new SearchResult<Movie>()
            {
                Keyword = keyword,
                ItemFoundCount = count,
                Page = page,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize),
                Data = record,
            };


        }

        public bool Exist(string title)
        {
            var movie = _dataContext.Movies
                        .Where(m => m.Title == title)
                        .FirstOrDefault();          

            return movie != null;

        }

       
    }
}