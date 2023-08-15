using Application.UseCases.Docs.Responses;
using DocumentFormat.OpenXml.Vml.Office;
using Domein.Entities;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;
using System.Runtime.CompilerServices;

namespace Infrustructure.Persistence;

public partial class NewdatabaseContext : DbContext, IApplicationDbContext
{
    public NewdatabaseContext()
    {
    }

    public NewdatabaseContext(DbContextOptions<NewdatabaseContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientAnswer> ClientAnswers { get; set; }

    public virtual DbSet<ClientType> ClientTypes { get; set; }

    public virtual DbSet<DefaultAnswer> DefaultAnswers { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Doc> Docs { get; set; }

    public virtual DbSet<DocChangeLog> DocChangeLogs { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Quarter> Quarters { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionType> QuestionTypes { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SysTable> SysTables { get; set; }

    public virtual DbSet<TranslateCategory> TranslateCategories { get; set; }

    public virtual DbSet<TranslateClientType> TranslateClientTypes { get; set; }

    public virtual DbSet<TranslateDefaultAnswer> TranslateDefaultAnswers { get; set; }

    public virtual DbSet<TranslatePermission> TranslatePermissions { get; set; }

    public virtual DbSet<TranslateQuestion> TranslateQuestions { get; set; }

    public virtual DbSet<TranslateQuestionType> TranslateQuestionTypes { get; set; }

    public virtual DbSet<TranslateRole> TranslateRoles { get; set; }

    public virtual DbSet<TranslateUserType> TranslateUserTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAction> UserActions { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=Jam2001!!!;Database=newdatabase")
        .EnableSensitiveDataLogging();

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primarykey_of_categories");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.ClientType).WithMany(p => p.Clients).HasConstraintName("client_type_id_fkey");

            entity.HasOne(d => d.Person).WithMany(p => p.Clients).HasConstraintName("person_client_fk");

            entity.HasMany(d => d.Categories).WithMany(p => p.Clients)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("category_fk"),
                    l => l.HasOne<Client>().WithMany()
                        .HasForeignKey("ClientId")
                        .HasConstraintName("client_fk"),
                    j =>
                    {
                        j.HasKey("ClientId", "CategoryId").HasName("client_category_pkey");
                        j.ToTable("client_category");
                        j.IndexerProperty<Guid>("ClientId").HasColumnName("client_id");
                        j.IndexerProperty<Guid>("CategoryId").HasColumnName("category_id");
                    });
        });

        modelBuilder.Entity<ClientAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_answer_pk");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.DefaultAnswer).WithMany(p => p.ClientAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("default_answer_fk");

            entity.HasOne(d => d.Doc).WithMany(p => p.ClientAnswers).HasConstraintName("doc_fk");

            entity.HasOne(d => d.Question).WithMany(p => p.ClientAnswers).HasConstraintName("question_fk");
        });

        modelBuilder.Entity<ClientType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserType_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<DefaultAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("default_answers_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Question).WithMany(p => p.DefaultAnswers).HasConstraintName("question_fk");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primarykey_of_districts");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Region).WithMany(p => p.Districts).HasConstraintName("districts_region_id_fkey");
        });

        modelBuilder.Entity<Doc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("docs_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Client).WithMany(p => p.Docs).HasConstraintName("cleint_doc_fk");

            entity.HasOne(d => d.User).WithMany(p => p.Docs).HasConstraintName("user_doc_fk");
        });

        modelBuilder.Entity<DocChangeLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("doc_change_log_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Doc).WithMany(p => p.DocChangeLogs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("doc_fk");

            entity.HasOne(d => d.Table).WithMany(p => p.DocChangeLogs).HasConstraintName("table_id");

            entity.HasOne(d => d.User).WithMany(p => p.DocChangeLogs).HasConstraintName("user_fk");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("translate_bindings_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primarykey_of_permissions");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Quarter).WithMany(p => p.People)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("quarter_fk");
        });

        modelBuilder.Entity<Quarter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primarykey_of_quarters");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.District).WithMany(p => p.Quarters).HasConstraintName("quarters_district_id_fkey");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primarykey_of_questions");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Category).WithMany(p => p.Questions).HasConstraintName("questions_category_id_fkey");

            entity.HasOne(d => d.CreatorUser).WithMany(p => p.Questions).HasConstraintName("questions_creator_user_id_fkey");

            entity.HasOne(d => d.QuestionType).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("questions_question_type_id_fkey");
        });

        modelBuilder.Entity<QuestionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("question_type_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primarykey_of_regions");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primarykey_of_roles");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("role_permissions_permission_id_fkey"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("role_permissions_role_id_fkey"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("primarykey_of_role_permissions");
                        j.ToTable("role_permissions");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<Guid>("PermissionId").HasColumnName("permission_id");
                    });
        });

        modelBuilder.Entity<SysTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sys_tables_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TranslateCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tr_categories_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Langauge).WithMany(p => p.TranslateCategories).HasConstraintName("language_fk");

            entity.HasOne(d => d.Owner).WithMany(p => p.TranslateCategories).HasConstraintName("owner_fk");
        });

        modelBuilder.Entity<TranslateClientType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("en_client_type_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Language).WithMany(p => p.TranslateClientTypes).HasConstraintName("language_fk");

            entity.HasOne(d => d.Owner).WithMany(p => p.TranslateClientTypes).HasConstraintName("owner_fk");
        });

        modelBuilder.Entity<TranslateDefaultAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("en_default_answers_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Language).WithMany(p => p.TranslateDefaultAnswers).HasConstraintName("language_fk");

            entity.HasOne(d => d.Owner).WithMany(p => p.TranslateDefaultAnswers).HasConstraintName("owner_fk");
        });

        modelBuilder.Entity<TranslatePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("translate_permissions_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Language).WithMany(p => p.TranslatePermissions).HasConstraintName("language_fk");

            entity.HasOne(d => d.Owner).WithMany(p => p.TranslatePermissions).HasConstraintName("owner_fk");
        });

        modelBuilder.Entity<TranslateQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("translate_question_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Language).WithMany(p => p.TranslateQuestions).HasConstraintName("language_fk");

            entity.HasOne(d => d.Owner).WithMany(p => p.TranslateQuestions).HasConstraintName("owner_fk");
        });

        modelBuilder.Entity<TranslateQuestionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("en_question_type_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Language).WithMany(p => p.TranslateQuestionTypes).HasConstraintName("langauge_fk");

            entity.HasOne(d => d.Owner).WithMany(p => p.TranslateQuestionTypes).HasConstraintName("owner_fk");
        });

        modelBuilder.Entity<TranslateRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("translate_role_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Language).WithMany(p => p.TranslateRoles).HasConstraintName("language_fk");

            entity.HasOne(d => d.Owner).WithMany(p => p.TranslateRoles).HasConstraintName("owner_fk");
        });

        modelBuilder.Entity<TranslateUserType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("translate_user_type_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Language).WithMany(p => p.TranslateUserTypes).HasConstraintName("language_id");

            entity.HasOne(d => d.Owner).WithMany(p => p.TranslateUserTypes).HasConstraintName("owner_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_id_pr");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Person).WithMany(p => p.Users).HasConstraintName("user_person_id");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_type_fk");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("user_roles_role_id_fkey"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("user_roles_user_id_fkey"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("primarykey_of_user_roles");
                        j.ToTable("user_roles");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<UserAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_actions_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Table).WithMany(p => p.UserActions).HasConstraintName("table_action");

            entity.HasOne(d => d.User).WithMany(p => p.UserActions).HasConstraintName("user_fk");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_type_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);

    }

    

    public IQueryable<FilterByUserResponse> GetFilteredUsers(
Guid? RegionId,
Guid? DistrictId,
Guid? QuarterId,
bool ByRegion,
bool ByDistrict,
bool ByQuarter
)
=> FromExpression(() => GetFilteredUsers(
 RegionId,
 DistrictId,
 QuarterId,
  ByRegion,
 ByDistrict,
 ByQuarter
 )
);

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
