using RentACarCleanArchitecture.Domain.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace RentACarCleanArchitecture.Infrastructure.Authentication;


public class HasPermissionAttribute : AuthorizeAttribute
{

    public HasPermissionAttribute(PermissionEnum permission)
    : base(policy: permission.ToString())
    {

    }

}