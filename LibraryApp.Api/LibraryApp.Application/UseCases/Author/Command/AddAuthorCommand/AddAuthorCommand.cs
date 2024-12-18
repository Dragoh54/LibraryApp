using System.Windows.Input;
using LibraryApp.DataAccess.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LibraryApp.Application.UseCases.Author.Command.AddAuthorCommand;

public record AddAuthorCommand : CreateAuthorDto, IRequest<AuthorDto>
{
   public AddAuthorCommand()
   {
   }

   public AddAuthorCommand(string surname, string country, DateTime birthDate) : base(country, surname, birthDate)
   {
   }
}