using System.Collections.Generic;
using Vidly.Common;
using Vidly.Common.Request;
using Vidly.Models;

namespace Vidly.Contracts
{
    interface ITransactionRepository
    {
        Paginated<Transaction> GetAll(int page, int pageSize, string filter);

        Transaction GetById(int id);

        bool Add(Transaction model, List<TransactionMovie> movies);

        bool Update(int id, Transaction transaction);

        bool Cancel(int id);

        bool ForceDelete(int id);

        SearchResult<Transaction> Search(string keyword, int page, int pageSize);

        Movie GetMovie(string keyword);
    }
}
