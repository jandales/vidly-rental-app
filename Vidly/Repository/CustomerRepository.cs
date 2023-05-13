using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidly.Common;
using Vidly.Contracts;
using Vidly.Data;
using Vidly.Exceptions;
using Vidly.Models;

namespace Vidly.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly DataContext _dataContext;
        public CustomerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Customer Create(Customer Customer)
        {
            _dataContext.Customers.Add(Customer);
            _dataContext.SaveChanges();

            return Customer;

        }

        public bool Delete(int id)
        {
            var customer = GetById(id);
            if (customer == null)
                throw new CustomerNotFoundException("Customer not found");

            customer.DeletedAt = DateTime.Now;
           return  _dataContext.SaveChanges() > 0;
        }

        public bool ForceDelete(int id)
        {
            var customer = GetById(id);
            if (customer == null)
                throw new CustomerNotFoundException("Customer not found");

            _dataContext.Customers.Remove(customer);
            return _dataContext.SaveChanges() > 0;
        }

        public Customer GetById(int id)
        {
            var customer  = _dataContext.Customers
                    //.Include(c => c.Transactions)
                    .Where(c => c.CustomerId == id)
                    .FirstOrDefault();

            if (customer == null)
                throw new CustomerNotFoundException("Customer not found");

            return customer;
        }

        public Paginated<Customer> GetAll(int page, int pageSize)
        {
            var currentPage = ((page <= 0 ? 1 : page) - 1) * pageSize;

            var count = _dataContext.Customers.Where(c => c.DeletedAt == null).Count();

            var record = _dataContext.Customers
                     .Where(c => c.DeletedAt == null)
                     .OrderByDescending(c => c.CustomerId)
                     .Skip(currentPage)
                     .Take(pageSize)
                     .ToList();

            return new Paginated<Customer>
            {
                Page = page,
                PageSize = pageSize,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize),
                Data = record
            };

        }

        public Customer Update(int id, Customer model)
        {
            var entity = GetById(id);

            if (entity == null) return entity;

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Gender = model.Gender;
            entity.Address = model.Address;
            entity.PhoneNumber = model.PhoneNumber;
            entity.UpdatedAt = DateTime.Now;

            _dataContext.SaveChanges();

            return entity;

        }

        public SearchResult<Customer> Search(string keyword, int page, int pageSize)
        {

            var record = _dataContext.Customers
                        .Where(c => c.LastName.Contains(keyword ?? string.Empty) || c.FirstName.Contains(keyword ?? string.Empty))
                        .OrderByDescending(m => m.CustomerId)
                        .ToList();
            

            var count = record.Count();

            return new SearchResult<Customer>()
            {

                Keyword = keyword,
                ItemFoundCount = count,
                Page = page,
                PageSize = pageSize,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize),
                Data = record,

            };
        }

        public bool Exist(string email)
        {
            var customer =  _dataContext.Customers
                    .Where(c => c.Email == email)
                    .FirstOrDefault();

            return customer != null;

        }

    }
}