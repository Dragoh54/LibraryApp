using LibraryApp.Application.Validators;
using LibraryApp.DataAccess.Dto;

namespace LibraryApp.Application.UseCases.Author.Command.AddAuthorCommand;

public class AddAuthorValidator : CreateAuthorDtoValidator
{
    public AddAuthorValidator() : base()
    {
        
    }
}