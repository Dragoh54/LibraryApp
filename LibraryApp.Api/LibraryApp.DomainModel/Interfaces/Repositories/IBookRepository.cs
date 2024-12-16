using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Interfaces.Repositories;

public interface IBookRepository : IBaseRepository<BookEntity>
{
    public Task<BookEntity?> GetByISBN(string ISBN);
    
    public Task<(List<BookEntity>?, int)> GetBooks(int page, int pageSize);
}
