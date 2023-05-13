using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidly.Models;
using Vidly.Common;

namespace Vidly.Contracts
{
    interface IMovieRepository :  IDisposable
    {
        Paginated<Movie> GetMovies(int page, int pageSize, string filter);
        Movie GetById(int id);
        Movie GetByTitle(string title);
        Movie Create(Movie Movie);
        Movie Update(int id, Movie Movie);
        bool Delete(int id);
        bool ForceDelete(int id);
        bool Exist(string title);
        SearchResult<Movie> Search(string keyword, int page, int pageSize);
        
    }
}
