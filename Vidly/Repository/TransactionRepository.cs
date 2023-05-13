using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Vidly.Common;
using Vidly.Contracts;
using Vidly.Data;
using Vidly.Models;
using Vidly.Common.Request;
using Vidly.Exceptions;
using Newtonsoft.Json;

namespace Vidly.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _dataContext;
        public TransactionRepository(DataContext context)
        {
            _dataContext = context;
        }

        public bool Add(Transaction model, List<TransactionMovie> movies)
        {

            _dataContext.Transactions.Add(model);

            var result = _dataContext.SaveChanges() != 0;

            if (result == false) return result;

            foreach (var movie in movies)
            {
                var entity = _dataContext.Movies.Where(m => m.MovieId == movie.Id).FirstOrDefault();

                if (entity.Copies <= 0) return false;

                entity.Copies -= movie.Qty;

                _dataContext.SaveChanges(); 
               
                _dataContext.RentedMovies.Add(new RentedMovie() {
                    MovieId = movie.Id,
                    Qty = movie.Qty,
                    Price = movie.Price,
                    TransactionId = model.TransactionId,
                });

                _dataContext.SaveChanges();

            }




            return result;
        }

        public Paginated<Transaction> GetAll(int page, int pageSize, string filter = "all")
        {
            var currentPage = ((page <= 0 ? 1 : page) - 1) * pageSize;

            var count = _dataContext.Transactions.Count();

            var query = _dataContext.Transactions.AsQueryable();

            if (filter != "all")
            {
                query = query.Where(t => t.Status == filter);
            }

            var transactions = query               
                .Include(t => t.Customer)
                .Where(t => t.DeletedDate == null)                
                .OrderByDescending(t => t.TransactionId)
                .Skip(currentPage)
                .Take(pageSize)
                .ToList();



            return new Paginated<Transaction>
            {
                Page = page,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize),
                Data = transactions
            };

        }

        public Transaction GetById(int id)
        {
            return _dataContext.Transactions
                    .Where(x => x.TransactionId == id)
                    .Include(t => t.Customer)
                    .Include(t => t.RentedMovies)
                    .FirstOrDefault();
        }

        public bool Update(int id, Transaction model)
        {
            var transaction = GetById(id);

            transaction.CustomerId = model.CustomerId;

            return _dataContext.SaveChanges() > 0;
        }

        public bool Cancel(int id)
        {
            var transaction = this.GetById(id);

            if (transaction == null)
                throw new TransactionNotFoundExeption("Transaction not found");

            if (transaction.Status != "issued")
                throw new TransactionNotIssuedException("Transaction is issued status");

            transaction.Status = "cancelled";
            _dataContext.SaveChanges();

            var rentedMovies = this.GetRentedMovies(transaction.TransactionId);

            rentedMovies.ForEach(rm => {
                if(rm.ReturnDate == null) {
                    rm.ReturnDate = DateTime.Now;
                    _dataContext.SaveChanges();
                    this.UpdateMovieQuantity(rm.MovieId, rm.Qty);
                }
            });           

            return true;
        }

        public bool ForceDelete(int id)
        {
            var transaction = this.GetById(id);

            if (transaction == null)
                return false;

            _dataContext.Transactions.Remove(transaction);

            return _dataContext.SaveChanges() > 0;

        }

        public SearchResult<Transaction> Search(string keyword, int page, int pageSize)
        {
            var record = _dataContext.Transactions
                         .Where(m => m.TransactionNumber.Contains(keyword ?? string.Empty) || (m.Customer.LastName + " " + m.Customer.FirstName).Contains(keyword ?? string.Empty))
                         .OrderByDescending(m => m.TransactionId)
                         .ToList();
            
            var count = record.Count();

            return new SearchResult<Transaction>()
            {
                Keyword = keyword,
                ItemFoundCount = count,
                Page = page,
                PageSize = pageSize,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize),
                Data = record,
            };

        }

        public Movie GetMovie(string keyword)
        {
            var movie = _dataContext.Movies.Where(m => m.Title == keyword).FirstOrDefault();
            if (movie == null) throw new MovieNotFoundException("Movie not found");
            return movie;
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
                        id : transactionId, 
                    status : "returned"
                );
            } 

            return rentedMovie;
        }

        private Transaction SetTransactionStatus(int id, string status)
        {
            var transaction = _dataContext.Transactions.Where(t => t.TransactionId == id).FirstOrDefault();
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

        private List<RentedMovie> GetRentedMovies(int id)
        {
            return _dataContext.RentedMovies.Where(rm => rm.TransactionId == id).ToList();
        }

    }
}