using Application.UseCases.Docs.Responses;
using Domein.Entities;
using Microsoft.EntityFrameworkCore;

namespace NewProject.Abstraction
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }

        DbSet<Client> Clients { get; }

        DbSet<ClientAnswer> ClientAnswers { get; }

        DbSet<ClientType> ClientTypes { get; }

        DbSet<DefaultAnswer> DefaultAnswers { get; }

        DbSet<District> Districts { get; }

        DbSet<Doc> Docs { get; }

        DbSet<DocChangeLog> DocChangeLogs { get; }

        DbSet<Language> Languages { get; }

        DbSet<Permission> Permissions { get; }

        DbSet<Person> People { get; }

        DbSet<Quarter> Quarters { get; }

        DbSet<Question> Questions { get; }

        DbSet<QuestionType> QuestionTypes { get; }

        DbSet<Region> Regions { get; }

        DbSet<Role> Roles { get; }

        DbSet<SysTable> SysTables { get; }

        DbSet<TranslateCategory> TranslateCategories { get; }

        DbSet<TranslateClientType> TranslateClientTypes { get; }

        DbSet<TranslateDefaultAnswer> TranslateDefaultAnswers { get; }

        DbSet<TranslatePermission> TranslatePermissions { get; }

        DbSet<TranslateQuestion> TranslateQuestions { get; }

        DbSet<TranslateQuestionType> TranslateQuestionTypes { get; }

        DbSet<TranslateRole> TranslateRoles { get; }

        DbSet<TranslateUserType> TranslateUserTypes { get; }

        DbSet<User> Users { get; }

        DbSet<UserAction> UserActions { get; }

        DbSet<UserType> UserTypes { get; }
        ValueTask DisposeAsync();

        IQueryable<FilterByUserResponse> GetFilteredUsers(Guid? RegionId,
        Guid? DistrictId,
        Guid? QuarterId,
        bool ByRegion,
        bool ByDistrict,
        bool ByQuarter
        );

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
