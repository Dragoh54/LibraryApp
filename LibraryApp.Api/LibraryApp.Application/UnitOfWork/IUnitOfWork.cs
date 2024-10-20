using LibraryApp.Application.Repositories;
using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<UserEntity> UserRepository { get; }
    IAuthorRepository AuthorRepository { get; }
    IBookRepository BookRepository { get; }
}
