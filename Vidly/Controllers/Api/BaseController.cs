using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Vidly.Controllers.Api
{
    public class BaseController : ApiController
    {
        public IHttpActionResult DbEntityValidationErrors(DbEntityValidationException ex)
        {
            var errorMessages = ex.EntityValidationErrors
                     .SelectMany(validationErrors => validationErrors.ValidationErrors)
                     .Select(validationError => new { name = validationError.PropertyName, message = validationError.ErrorMessage })
                     .ToArray();

            return Content(HttpStatusCode.BadRequest, new { errors = errorMessages });
        }
    }
}
