using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Interfaces.Repositories;

public interface IBaseRepository<T> 
{
    public Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
    public Task<T?> Get(Guid id, CancellationToken cancellationToken);
    public Task<T> Add(T item, CancellationToken cancellationToken);
    public Task<T?> Update(T item, CancellationToken cancellationToken);
    public Task SaveAsync(CancellationToken cancellationToken);
    public Task Delete(T item, CancellationToken cancellationToken);
}
