using LibraryApp.DataAccess.Dto;
using MediatR;

namespace LibraryApp.Application.UseCases.Book.Command.TakeBookCommand;

public record TakeBookCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string? UserClaimId { get; set; } = string.Empty;
    
    public TakeBookCommand()
    {
    }

    public TakeBookCommand(Guid id, string? userClaimId)
    {
        Id = id;
        UserClaimId = userClaimId;
    }
}