using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.TakeBookCommand;

public record TakeBookCommand : TakeBookDto, IRequest<bool>
{
    public TakeBookCommand()
    {
    }

    public TakeBookCommand(Guid id, string? userClaimId)
    {
        Id = id;
        UserClaimId = userClaimId;
    }
}