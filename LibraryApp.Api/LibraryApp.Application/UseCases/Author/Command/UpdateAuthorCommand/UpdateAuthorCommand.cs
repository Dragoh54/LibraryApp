using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Command.UpdateAuthorCommand;

public record UpdateAuthorCommand : IRequest<AuthorDto>
{
    public Guid Id { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string Country {  get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    
    public UpdateAuthorCommand() {}

    public UpdateAuthorCommand(Guid id, string surname, string country, DateTime birthDate)
    {
        Id = id;
        Surname = surname;
        Country = country;
        BirthDate = birthDate;
    }
}