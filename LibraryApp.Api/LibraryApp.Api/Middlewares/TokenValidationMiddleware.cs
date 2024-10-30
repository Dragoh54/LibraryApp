namespace LibraryApp.Api.Middlewares;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var accessToken = context.Request.Cookies["tasty-cookies"];
        var refreshToken = context.Request.Cookies["not-a-refresh-token-cookies"];
        
        var isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;
        
        if (isAuthenticated && string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
        {
            context.Response.Redirect("/refresh");
            return;
        }

        await _next(context);
    }
}