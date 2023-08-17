using Application.UseCases.Docs.Responses;
using Domein.Entities;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Category>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("primarykey_of_categories");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
        });

        _ = modelBuilder.Entity<Client>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("client_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.ClientType).WithMany(p => p.Clients).HasConstraintName("client_type_id_fkey");

            _ = entity.HasOne(d => d.Person).WithMany(p => p.Clients).HasConstraintName("person_client_fk");

            _ = entity.HasMany(d => d.Categories).WithMany(p => p.Clients)
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
                        _ = j.HasKey("ClientId", "CategoryId").HasName("client_category_pkey");
                        _ = j.ToTable("client_category");
                        _ = j.IndexerProperty<Guid>("ClientId").HasColumnName("client_id");
                        _ = j.IndexerProperty<Guid>("CategoryId").HasColumnName("category_id");
                    });
        });

        _ = modelBuilder.Entity<ClientAnswer>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("client_answer_pk");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.DefaultAnswer).WithMany(p => p.ClientAnswers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("default_answer_fk");

            _ = entity.HasOne(d => d.Doc).WithMany(p => p.ClientAnswers).HasConstraintName("doc_fk");

            _ = entity.HasOne(d => d.Question).WithMany(p => p.ClientAnswers).HasConstraintName("question_fk");
        });

        _ = modelBuilder.Entity<ClientType>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("UserType_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
        });

        _ = modelBuilder.Entity<DefaultAnswer>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("default_answers_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Question).WithMany(p => p.DefaultAnswers).HasConstraintName("question_fk");
        });

        _ = modelBuilder.Entity<District>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("primarykey_of_districts");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Region).WithMany(p => p.Districts).HasConstraintName("districts_region_id_fkey");
        });

        _ = modelBuilder.Entity<Doc>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("docs_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Client).WithMany(p => p.Docs).HasConstraintName("cleint_doc_fk");

            _ = entity.HasOne(d => d.User).WithMany(p => p.Docs).HasConstraintName("user_doc_fk");
        });

        _ = modelBuilder.Entity<DocChangeLog>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("doc_change_log_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Doc).WithMany(p => p.DocChangeLogs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("doc_fk");

            _ = entity.HasOne(d => d.Table).WithMany(p => p.DocChangeLogs).HasConstraintName("table_id");

            _ = entity.HasOne(d => d.User).WithMany(p => p.DocChangeLogs).HasConstraintName("user_fk");
        });

        _ = modelBuilder.Entity<Language>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("translate_bindings_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
        });

        _ = modelBuilder.Entity<Permission>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("primarykey_of_permissions");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
        });

        _ = modelBuilder.Entity<Person>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("person_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Quarter).WithMany(p => p.People)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("quarter_fk");
        });

        _ = modelBuilder.Entity<Quarter>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("primarykey_of_quarters");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.District).WithMany(p => p.Quarters).HasConstraintName("quarters_district_id_fkey");
        });

        _ = modelBuilder.Entity<Question>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("primarykey_of_questions");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Category).WithMany(p => p.Questions).HasConstraintName("questions_category_id_fkey");

            _ = entity.HasOne(d => d.CreatorUser).WithMany(p => p.Questions).HasConstraintName("questions_creator_user_id_fkey");

            _ = entity.HasOne(d => d.QuestionType).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("questions_question_type_id_fkey");
        });

        _ = modelBuilder.Entity<QuestionType>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("question_type_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
        });

        _ = modelBuilder.Entity<Region>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("primarykey_of_regions");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
        });

        _ = modelBuilder.Entity<Role>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("primarykey_of_roles");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
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
                        _ = j.HasKey("RoleId", "PermissionId").HasName("primarykey_of_role_permissions");
                        _ = j.ToTable("role_permissions");
                        _ = j.IndexerProperty<Guid>("RoleId").HasColumnName("role_id");
                        _ = j.IndexerProperty<Guid>("PermissionId").HasColumnName("permission_id");
                    });
        });

        _ = modelBuilder.Entity<SysTable>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("sys_tables_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
        });

        _ = modelBuilder.Entity<TranslateCategory>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("tr_categories_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Language).WithMany(p => p.TranslateCategories).HasConstraintName("language_fk");

            _ = entity.HasOne(d => d.Owner).WithMany(p => p.TranslateCategories).HasConstraintName("owner_fk");
        });

        _ = modelBuilder.Entity<TranslateClientType>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("en_client_type_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Language).WithMany(p => p.TranslateClientTypes).HasConstraintName("language_fk");

            _ = entity.HasOne(d => d.Owner).WithMany(p => p.TranslateClientTypes).HasConstraintName("owner_fk");
        });

        _ = modelBuilder.Entity<TranslateDefaultAnswer>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("en_default_answers_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Language).WithMany(p => p.TranslateDefaultAnswers).HasConstraintName("language_fk");

            _ = entity.HasOne(d => d.Owner).WithMany(p => p.TranslateDefaultAnswers).HasConstraintName("owner_fk");
        });

        _ = modelBuilder.Entity<TranslatePermission>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("translate_permissions_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Language).WithMany(p => p.TranslatePermissions).HasConstraintName("language_fk");

            _ = entity.HasOne(d => d.Owner).WithMany(p => p.TranslatePermissions).HasConstraintName("owner_fk");
        });

        _ = modelBuilder.Entity<TranslateQuestion>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("translate_question_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Language).WithMany(p => p.TranslateQuestions).HasConstraintName("language_fk");

            _ = entity.HasOne(d => d.Owner).WithMany(p => p.TranslateQuestions).HasConstraintName("owner_fk");
        });

        _ = modelBuilder.Entity<TranslateQuestionType>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("en_question_type_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Language).WithMany(p => p.TranslateQuestionTypes).HasConstraintName("langauge_fk");

            _ = entity.HasOne(d => d.Owner).WithMany(p => p.TranslateQuestionTypes).HasConstraintName("owner_fk");
        });

        _ = modelBuilder.Entity<TranslateRole>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("translate_role_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Language).WithMany(p => p.TranslateRoles).HasConstraintName("language_fk");

            _ = entity.HasOne(d => d.Owner).WithMany(p => p.TranslateRoles).HasConstraintName("owner_fk");
        });

        _ = modelBuilder.Entity<TranslateUserType>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("translate_user_type_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Language).WithMany(p => p.TranslateUserTypes).HasConstraintName("language_id");

            _ = entity.HasOne(d => d.Owner).WithMany(p => p.TranslateUserTypes).HasConstraintName("owner_fk");
        });

        _ = modelBuilder.Entity<User>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("user_id_pr");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Person).WithMany(p => p.Users).HasConstraintName("user_person_id");

            _ = entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_type_fk");

            _ = entity.HasMany(d => d.Roles).WithMany(p => p.Users)
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
                        _ = j.HasKey("UserId", "RoleId").HasName("primarykey_of_user_roles");
                        _ = j.ToTable("user_roles");
                        _ = j.IndexerProperty<Guid>("UserId").HasColumnName("user_id");
                        _ = j.IndexerProperty<Guid>("RoleId").HasColumnName("role_id");
                    });
        });

        _ = modelBuilder.Entity<UserAction>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("user_actions_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Table).WithMany(p => p.UserActions).HasConstraintName("table_action");

            _ = entity.HasOne(d => d.User).WithMany(p => p.UserActions).HasConstraintName("user_fk");
        });

        _ = modelBuilder.Entity<UserType>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("user_type_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
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
        {
            return FromExpression(() => GetFilteredUsers(
                 RegionId,
                 DistrictId,
                 QuarterId,
                  ByRegion,
                 ByDistrict,
                 ByQuarter
                 )
                );
        }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
