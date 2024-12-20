using MediatR;
using Microsoft.AspNetCore.Http;

namespace LibraryApp.Application.UseCases.User.Command.RefreshCommand;

public class RefreshCommand : IRequest<string>
{
    public HttpContext HttpContext { get; set; }

    public RefreshCommand()
    {
    }

    public RefreshCommand(HttpContext httpContext)
    {
        HttpContext = httpContext;
    }
}