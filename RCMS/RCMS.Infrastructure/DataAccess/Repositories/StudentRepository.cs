using RCMS.Infrastructure.DataAccess.Contexts;
using RCMS.Infrastructure.DataAccess.Entities;
using RCMS.Infrastructure.DataAccess.Repositories.Contracts;

namespace RCMS.Infrastructure.DataAccess.Repositories;

public sealed class StudentRepository(RCMSDbContext context) : BaseRepository<Student>(context), IStudentRepository;