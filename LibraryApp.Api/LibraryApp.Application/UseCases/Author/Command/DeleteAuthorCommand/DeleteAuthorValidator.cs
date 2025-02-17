﻿using FluentValidation;
using LibraryApp.Application.Dto;
using LibraryApp.Application.Validators;

namespace LibraryApp.Application.UseCases.Author.Command.DeleteAuthorCommand;

public class DeleteAuthorValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorValidator()
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