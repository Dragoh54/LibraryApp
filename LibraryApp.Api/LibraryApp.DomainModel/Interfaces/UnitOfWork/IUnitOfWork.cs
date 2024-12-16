using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IAuthorRepository AuthorRepository { get; }
    IBookRepository BookRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
}
