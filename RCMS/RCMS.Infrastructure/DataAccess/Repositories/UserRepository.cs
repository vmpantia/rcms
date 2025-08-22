using RCMS.Infrastructure.DataAccess.Contexts;
using RCMS.Infrastructure.DataAccess.Entities;
using RCMS.Infrastructure.DataAccess.Repositories.Contracts;

namespace RCMS.Infrastructure.DataAccess.Repositories;

public class UserRepository(RCMSDbContext context) : BaseRepository<User>(context), IUserRepository;