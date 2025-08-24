using RCMS.Domain.Entities;
using RCMS.Shared.Models.Users;

namespace RCMS.Domain.Interfaces.Authentication;

public interface ITokenProvider
{
    UserTokenDto Create(User user);
}