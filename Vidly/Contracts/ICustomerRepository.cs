using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidly.Common;
using Vidly.Models;

namespace Vidly.Contracts
{
    interface ICustomerRepository
    {
        Paginated<Customer> GetAll(int page, int pageSize);
        Customer GetById(int id);
        Customer Create(Customer model);
        Customer Update(int id, Customer model);
        bool Delete(int id);
        bool ForceDelete(int id);
        SearchResult<Customer> Search(string keyword, int page, int pageSize);
    }
}
