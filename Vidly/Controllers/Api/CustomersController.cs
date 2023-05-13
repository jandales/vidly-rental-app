using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Contracts;
using Vidly.Common;
using Vidly.Models;
using Vidly.Repository;
using Vidly.Data;
using System.Web.Http.Cors;
using Vidly.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;

namespace Vidly.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomersController : BaseController
    {
        private readonly CustomerRepository _repository;
        public CustomersController()
        {
            _repository = new CustomerRepository(new DataContext());
        }

        [HttpGet]
        public IHttpActionResult GetCustomers([FromUri] PaginatedRequest request)
        {
            try
            {
                var customers = _repository.GetAll(
                    page: request.Page,
                    pageSize: PaginatedRequest.Page_Size
                );

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }
            
        }

        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {

            try
            {                
                return Ok(new Response
                {
                    Success = true,
                    Data = _repository.GetById(id)
                });
            }
            catch (CustomerNotFoundException ex)
            {
                return BadRequest(ex.Message);          
            }

          
        }

        [Route("api/customers/create")]
        [HttpPost]
        public IHttpActionResult AddCustomer([FromBody] CustomerRequest request)
        { 
            try
            { 
                if (_repository.Exist(request.Email))
                    return BadRequest("Email Already Exist");

                var customer = _repository.Create(new Customer()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Gender = request.Gender,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    BirthDate = request.BirthDate
                });               

                return Ok(customer);

            }
            catch (DbEntityValidationException ex)
            {
                return DbEntityValidationErrors(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }

        [Route("api/customers/update/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateCustomer([FromUri] int id, [FromBody] CustomerRequest request)
        {
            try
            {
                var customer = _repository.Update(id, new Customer()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Gender = request.Gender,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    BirthDate = request.BirthDate,
                    UpdatedAt = DateTime.Now,
                }); 

                return Ok(customer);

            }
            catch (DbEntityValidationException ex)
            {
                return DbEntityValidationErrors(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("api/customers/delete/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteCustomer([FromUri] int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok(new { message = "Successfully Deleted" });
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
                return Ok(new { message = "Successfully Deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [Route("api/customers/search")]
        [HttpGet]
        public IHttpActionResult SearchCustomer([FromUri] string keyword, [FromUri] PaginatedRequest request)
        {
            try
            {
                var customers = _repository.Search(
                    keyword: keyword,
                    page: request.Page,
                    pageSize: PaginatedRequest.Page_Size);

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        
    }


}
