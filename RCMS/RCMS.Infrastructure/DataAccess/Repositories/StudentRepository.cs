using RCMS.Infrastructure.DataAccess.Contexts;
using RCMS.Infrastructure.DataAccess.Entities;
using RCMS.Infrastructure.DataAccess.Repositories.Contracts;

namespace RCMS.Infrastructure.DataAccess.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    protected StudentRepository(RCMSDbContext context) : base(context) { }
}