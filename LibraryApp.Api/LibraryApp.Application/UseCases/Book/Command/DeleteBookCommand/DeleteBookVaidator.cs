﻿using FluentValidation;
using LibraryApp.Application.Validators;

namespace LibraryApp.Application.UseCases.Book.Command.DeleteBookCommand;

public class DeleteBookVaidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookVaidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Id model is required");
        
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotNull()
            .WithMessage("Id is required.");
    }
}