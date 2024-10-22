using Microsoft.AspNetCore.Authorization;

namespace LibraryApp.Api.Requirements;

public class RolePermissionRequirement(string role) 
    : IAuthorizationRequirement
{
    public string Role { get; } = role;
}
