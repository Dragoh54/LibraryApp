using LibraryApp.Application.Validators;

namespace LibraryApp.Application.UseCases.Book.Command.AddBookCommand;

public class AddBookValidator : CreateBookDtoValidator<AddBookCommand>
{
    public AddBookValidator()
    {
    }
}