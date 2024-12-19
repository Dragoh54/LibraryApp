using LibraryApp.Application.Validators;

namespace LibraryApp.Application.UseCases.Author.Command.AddAuthorCommand;

public class AddAuthorValidator : CreateAuthorDtoValidator<AddAuthorCommand>
{
    public AddAuthorValidator() : base()
    {
        
    }
}