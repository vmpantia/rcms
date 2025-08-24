using RCMS.Domain.Entities;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Infrastructure.DataAccess.Contexts;

namespace RCMS.Infrastructure.DataAccess.Repositories;

public sealed class StudentRepository(RCMSDbContext context) : BaseRepository<Student>(context), IStudentRepository;