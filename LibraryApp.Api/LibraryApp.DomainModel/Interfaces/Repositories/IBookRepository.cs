using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Application.Filters;

namespace LibraryApp.Application.Interfaces.Repositories;

public interface IBookRepository : IBaseRepository<BookEntity>
{
    public Task<BookEntity?> GetByISBN(string ISBN, CancellationToken cancellationToken);
    
    public Task<(List<BookEntity>?, int)> GetBooks(BookFilters filters, int page, int pageSize, CancellationToken cancellationToken);
}
