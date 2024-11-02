using BaseLibrary.Entities;

namespace BaseLibrary.Responses
{
    public record RolesResponse(bool flag, string Message = null! , List<Roles> roles = null!);
}
