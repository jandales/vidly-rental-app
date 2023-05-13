using System.Web.Http;
using Vidly.Common;
using Vidly.Contracts;
using Vidly.Data;
using Vidly.Repository;
using Vidly.Models;
using System;

namespace Vidly.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _repository;
        public CustomerController()
        {
            _repository = new CustomerRepository(new DataContext());
        }

        [HttpGet]
        public IHttpActionResult GetCustomers([FromUri] PaginatedRequest request)
        {
            return Ok(_repository.GetAll(request.Page, PaginatedRequest.Page_Size));
        }

        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            return Ok(_repository.GetById(id));
        }

        [HttpPost]
        public IHttpActionResult AddCustomer(CustomerRequest request)
        {
            try
            {
                _repository.Create(new Customer()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Gender = request.Gender,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    BirthDate = request.BirthDate
                });

                return Ok("Successfully Added");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerRequest request)
        {
            try
            {
                _repository.Update(id, new Customer()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Gender = request.Gender,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    BirthDate = request.BirthDate,
                    UpdatedAt = DateTime.Now,
                });

                return Ok("Successfully Added");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok("Successfuly Delete");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        [Route("api/customers/delete/{id}/force")]
        [HttpDelete]
        public IHttpActionResult ForceDeleteCustomer(int id)
        {
            try
            {
                _repository.ForceDelete(id);
                return Ok("Successfuly Delete");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [Route("api/customers/search")]
        [HttpGet]
        public IHttpActionResult SearchCustomer([FromUri] string keyword)
        {
            var data = _repository.Search(keyword);
            return Ok(data);
        }
    }
}
