using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Repositories;

public interface IBookRepository : IRepository<BookEntity>
{
    public Task<BookEntity?> GetByISBN(string ISBN);
}
