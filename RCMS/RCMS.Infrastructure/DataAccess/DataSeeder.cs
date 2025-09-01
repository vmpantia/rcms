using Bogus;
using RCMS.Domain.Entities;
using RCMS.Infrastructure.DataAccess.Contexts;
using RCMS.Shared.Enumerations;

namespace RCMS.Infrastructure.DataAccess;

public abstract class DataSeeder
{
    public static async Task GenerateAsync(RCMSDbContext context)
    {
        if (!context.Students.Any())
        {
            var dummyStudents = GenerateStudents();
            await context.Students.AddRangeAsync(dummyStudents);
            await context.SaveChangesAsync();
        }

        if (!context.Instructors.Any())
        {
            var dummyInstructors = GenerateInstructors();
            await context.Instructors.AddRangeAsync(dummyInstructors);
            await context.SaveChangesAsync();
        }

        if (!context.CourseCategories.Any())
        {
            var dummyCourseCategories = GenerateCourseCategories();
            await context.CourseCategories.AddRangeAsync(dummyCourseCategories);
            await context.SaveChangesAsync();
        }
    }
    
    private static List<Student> GenerateStudents(int count = 100)
    {
        var faker = new Faker<Student>()
            .RuleFor(s => s.Id, f => Guid.NewGuid())
            .RuleFor(s => s.FirstName, f => f.Person.FirstName)
            .RuleFor(s => s.LastName, f => f.Person.LastName)
            .RuleFor(s => s.Gender, f => f.PickRandom<GenderType>())
            .RuleFor(s => s.BirthDate, f => f.Person.DateOfBirth)
            .RuleFor(s => s.PhoneNumber, f => f.Person.Phone)
            .RuleFor(s => s.EmailAddress, f => f.Person.Email)
            .RuleFor(s => s.Status, f => f.PickRandom<StudentStatus>());        
        
        return faker.Generate(count);
    }
    
    private static List<Instructor> GenerateInstructors(int count = 100)
    {
        var faker = new Faker<Instructor>()
            .RuleFor(s => s.Id, f => Guid.NewGuid())
            .RuleFor(s => s.FirstName, f => f.Person.FirstName)
            .RuleFor(s => s.LastName, f => f.Person.LastName)
            .RuleFor(s => s.Gender, f => f.PickRandom<GenderType>())
            .RuleFor(s => s.BirthDate, f => f.Person.DateOfBirth)
            .RuleFor(s => s.PhoneNumber, f => f.Person.Phone)
            .RuleFor(s => s.EmailAddress, f => f.Person.Email)
            .RuleFor(s => s.Status, f => f.PickRandom<InstructorStatus>());        
        
        return faker.Generate(count);
    }

    private static List<CourseCategory> GenerateCourseCategories(int count = 100)
    {
        var faker = new Faker<CourseCategory>()
            .RuleFor(s => s.Id, f => Guid.NewGuid())
            .RuleFor(s => s.Name, f => f.Company.CompanyName())
            .RuleFor(s => s.Status, f => f.PickRandom<CourseCategoryStatus>());        
        
        return faker.Generate(count);
    }
}