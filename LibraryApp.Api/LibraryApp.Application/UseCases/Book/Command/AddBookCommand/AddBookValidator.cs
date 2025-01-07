using FluentValidation;
using LibraryApp.Application.UseCases.Author.Command.AddAuthorCommand;
using LibraryApp.Application.Validators;

namespace LibraryApp.Application.UseCases.Book.Command.AddBookCommand;

public class AddBookValidator : AbstractValidator<AddBookCommand>
{
    public AddBookValidator()
    {
        RuleFor(book => book)
            .NotNull()
            .WithMessage("The book object cannot be null.");

        RuleFor(book => book.ISBN)
            .NotEmpty()
            .WithMessage("This book must have an ISBN.")
            .Must(isbn => !string.IsNullOrWhiteSpace(isbn))
            .WithMessage("ISBN cannot contain only whitespace.");

        RuleFor(book => book.Title)
            .NotEmpty()
            .WithMessage("This book must have a title.")
            .Must(title => !string.IsNullOrWhiteSpace(title))
            .WithMessage("Title cannot contain only whitespace.");

        RuleFor(book => book.Description)
            .NotEmpty()
            .WithMessage("This book must have a description.")
            .Must(description => !string.IsNullOrWhiteSpace(description))
            .WithMessage("Description cannot contain only whitespace.");

        RuleFor(book => book.Genre)
            .NotEmpty()
            .WithMessage("This book must have a genre.")
            .Must(genre => !string.IsNullOrWhiteSpace(genre))
            .WithMessage("Genre cannot contain only whitespace.");

        RuleFor(book => book.AuthorId)
            .NotEmpty()
            .WithMessage("This book must have an author.")
            .Must(authorId => authorId != Guid.Empty)
            .WithMessage("AuthorId cannot be an empty GUID.");
    }
}