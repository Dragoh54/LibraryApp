﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Interfaces.Repositories;

public interface IBaseRepository<T> 
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T?> Get(Guid id);
    public Task<T> Add(T item);
    public Task<T?> Update(T item);
    public Task SaveAsync();
    public Task Delete(T item);
}
