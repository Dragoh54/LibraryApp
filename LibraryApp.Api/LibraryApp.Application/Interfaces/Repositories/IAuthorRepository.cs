using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Interfaces.Repositories;

public interface IAuthorRepository : IRepository<AuthorEntity>
{
    public Task<IEnumerable<BookEntity>?> GetAuthorBooks(AuthorEntity item);
}
