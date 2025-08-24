using RCMS.Shared.Models.Users;

namespace RCMS.Web.Interfaces.Services;

public interface IAuthService
{
    Task LoginAsync(LoginUserDto login);
    Task LogoutAsync();
}