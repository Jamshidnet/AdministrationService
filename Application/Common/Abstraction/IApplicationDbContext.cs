using Domein.Entities;
using Microsoft.EntityFrameworkCore;

namespace NewProject.Abstraction
{
    public interface IApplicationDbContext
    {
        DbSet<DefaultAnswer> DefaultAnswers { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<Client> Clients { get; set; }

        DbSet<ClientType> ClientTypes { get; set; }

        DbSet<District> Districts { get; set; }

        DbSet<Doc> Docs { get; set; }

        DbSet<Permission> Permissions { get; set; }

        DbSet<Person> People { get; set; }

        DbSet<Quarter> Quarters { get; set; }

        DbSet<Question> Questions { get; set; }

        DbSet<QuestionType> QuestionTypes { get; set; }

        DbSet<Region> Regions { get; set; }

        DbSet<Role> Roles { get; set; }

        DbSet<User> Users { get; set; }

          DbSet<ClientAnswer> ClientAnswers { get; set; }
         DbSet<UserType> UserTypes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
