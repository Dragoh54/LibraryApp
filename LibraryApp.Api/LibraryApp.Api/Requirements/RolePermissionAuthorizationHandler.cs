using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LibraryApp.Api.Requirements;

public class RolePermissionAuthorizationHandler
    : AuthorizationHandler<RolePermissionRequirement>
{

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolePermissionRequirement requirement)
    {
        var roleClaim = context.User
            .FindFirst(claim => claim.Type == ClaimTypes.Role);

        if (roleClaim == null)
        {
            context.Fail(new AuthorizationFailureReason(this, "This token has no role"));
            return Task.CompletedTask;
        }

        string role = roleClaim.Value;

        string[] requireRoles = context.Requirements
            .OfType<RolePermissionRequirement>()
            .Select(r => r.Role)
            .ToArray();

        bool hasRequiredRole = requireRoles
            .Any(r => role.Equals(r, StringComparison.OrdinalIgnoreCase));

        if(!hasRequiredRole)
        {
            context.Fail(new AuthorizationFailureReason(this, "This user doesn't has require role"));
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
