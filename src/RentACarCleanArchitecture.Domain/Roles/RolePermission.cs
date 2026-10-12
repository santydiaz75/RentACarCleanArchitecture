using RentACarCleanArchitecture.Domain.Permissions;

namespace RentACarCleanArchitecture.Domain.Roles;

public sealed class RolePermission
{

    public int RoleId {get;set;}
    public PermissionId? PermissionId {get;set;}

}