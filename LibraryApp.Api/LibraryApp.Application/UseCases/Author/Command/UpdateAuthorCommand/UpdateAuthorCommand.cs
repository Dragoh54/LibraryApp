using LibraryApp.Application.Dto;
using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Author.Command.UpdateAuthorCommand;

public record UpdateAuthorCommand : UpdateAuthorDto, IRequest<AuthorDto>
{
    public UpdateAuthorCommand()
    {
    }

    public UpdateAuthorCommand(Guid id, string surname, string country, DateTime birthDate) : base(id, country, surname, birthDate)
    {
    }
}