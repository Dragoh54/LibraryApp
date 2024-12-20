using MediatR;
using Microsoft.AspNetCore.Http;

namespace LibraryApp.Application.UseCases.User.Command.LogoutCommand;

public class LogoutCommand : IRequest<bool>
{
    public HttpContext HttpContext { get; set; }

    public LogoutCommand()
    {
    }

    public LogoutCommand(HttpContext httpContext)
    {
        HttpContext = httpContext;
    }
}