using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.DataAccess.Dto;

namespace LibraryApp.Application.Interfaces.Repositories;

public interface IBookRepository : IRepository<BookEntity>
{
    public Task<BookEntity?> GetByISBN(string ISBN);
    
    public Task<PaginatedPagedResult<BookEntity>?> GetBooks(int page, int pageSize);
}
