using System.Windows.Input;
using LibraryApp.DataAccess.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LibraryApp.Application.UseCases.Author.Command.AddAuthorCommand;

public record AddAuthorCommand : IRequest<AuthorDto>
{
   public string Surname { get; set; } = string.Empty;
   public string Country {  get; set; } = string.Empty;
   public DateTime BirthDate { get; set; }
    
   public AddAuthorCommand() {}

   public AddAuthorCommand(string surname, string country, DateTime birthDate)
   {
      Surname = surname;
      Country = country;
      BirthDate = birthDate;
   }
}