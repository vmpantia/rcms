using RCMS.Domain.Entities;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Infrastructure.DataAccess.Contexts;

namespace RCMS.Infrastructure.DataAccess.Repositories;

public class UserRepository(RCMSDbContext context) : BaseRepository<User>(context), IUserRepository;