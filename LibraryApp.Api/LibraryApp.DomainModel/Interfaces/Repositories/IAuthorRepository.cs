using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Application.Filters;

namespace LibraryApp.Application.Interfaces.Repositories;

public interface IAuthorRepository : IBaseRepository<AuthorEntity>
{
    public Task<IEnumerable<BookEntity>?> GetAuthorBooks(Guid id, CancellationToken cancellationToken);
    
    public Task<(List<AuthorEntity>?, int)> GetAuthors(AuthorFilters filters, int page, int pageSize, CancellationToken cancellationToken);
}
