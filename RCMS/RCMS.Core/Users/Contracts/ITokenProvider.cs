using RCMS.Infrastructure.DataAccess.Entities;
using RCMS.Shared.Models.Users;

namespace RCMS.Core.Users.Contracts;

public interface ITokenProvider
{
    UserTokenDto Create(User user);
}