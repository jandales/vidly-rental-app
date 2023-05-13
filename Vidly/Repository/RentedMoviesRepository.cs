using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Data;
using Vidly.Models;

namespace Vidly.Repository
{
    public class RentedMoviesRepository
    {
        private readonly DataContext _dataContext;
        public RentedMoviesRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<RentedMovie> GetByTransaction(int transactionId)
        {
            return _dataContext.RentedMovies.Where(rm => rm.TransactionId == transactionId).ToList();
        }

        public RentedMovie ReturnMovie(int id)
        {
            var rentedMovie = _dataContext.RentedMovies.Where(rm => rm.RentedMovieId == id).FirstOrDefault();

            if (rentedMovie == null || rentedMovie.ReturnDate != null) return rentedMovie;

            rentedMovie.ReturnDate = DateTime.Now;

            _dataContext.SaveChanges();


            this.UpdateMovieQuantity(rentedMovie.MovieId, rentedMovie.Qty);

            var transactionId = rentedMovie.TransactionId;

            if (this.GetUnReturnMovies(transactionId) == 0)
            {
                this.SetTransactionStatus(
                     transactoinId : transactionId,
                            status : "returned"
                );
            }

            return rentedMovie;
        }

        private Transaction SetTransactionStatus(int transactoinId, string status)
        {
            Transaction transaction = _dataContext.Transactions.Where(t => t.TransactionId == transactoinId).FirstOrDefault();
            transaction.Status = status;
            transaction.UpdatedDate = DateTime.Now;
            _dataContext.SaveChanges();

            return transaction;
        }

        private int GetUnReturnMovies(int transactionId)
        {
            return _dataContext.RentedMovies
               .Where(rm => rm.TransactionId == transactionId && rm.ReturnDate == null).Count();

        }

        private void UpdateMovieQuantity(int id, int qty = 1)
        {
            Movie movie = _dataContext.Movies.Where(m => m.MovieId == id).FirstOrDefault();
            movie.Copies += qty;
            _dataContext.SaveChanges();
        }
    }
}