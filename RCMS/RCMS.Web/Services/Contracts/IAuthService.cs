using RCMS.Shared.Models.Users;

namespace RCMS.Web.Services.Contracts;

public interface IAuthService
{
    Task LoginAsync(LoginUserDto login);
    Task LogoutAsync();
}