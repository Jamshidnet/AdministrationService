using Application.UseCases.Docs.Filters;
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
    public virtual DbSet<UserType> UserTypes { get; set; }


    public virtual DbSet<DefaultAnswer> DefaultAnswers { get; set; }
    public virtual DbSet<ClientType> ClientTypes { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Doc> Docs { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Quarter> Quarters { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionType> QuestionTypes { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=Jam2001!!!;Database=newdatabase");

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

            entity.HasOne(d => d.ClientType).WithMany(p => p.Clients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_type_id_fkey");

            entity.HasOne(d => d.Person).WithMany(p => p.Clients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_client_fk");

            entity.HasMany(d => d.Categories).WithMany(p => p.Clients)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("category_fk"),
                    l => l.HasOne<Client>().WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
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

            entity.HasOne(d => d.DefaultAnswer).WithMany(p => p.ClientAnswers).HasConstraintName("default_answer_fk");

            entity.HasOne(d => d.Doc).WithMany(p => p.ClientAnswers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doc_fk");

            entity.HasOne(d => d.Question).WithMany(p => p.ClientAnswers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("question_fk");
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

            entity.HasOne(d => d.Question).WithMany(p => p.DefaultAnswers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("question_fk");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primarykey_of_districts");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Region).WithMany(p => p.Districts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("districts_region_id_fkey");
        });

        modelBuilder.Entity<Doc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("docs_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Client).WithMany(p => p.Docs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cleint_doc_fk");

            entity.HasOne(d => d.User).WithMany(p => p.Docs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_doc_fk");
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

            entity.HasOne(d => d.Quarter).WithMany(p => p.People).HasConstraintName("quarter_fk");
        });

        modelBuilder.Entity<Quarter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primarykey_of_quarters");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.District).WithMany(p => p.Quarters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("quarters_district_id_fkey");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primarykey_of_questions");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Category).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("questions_category_id_fkey");

            entity.HasOne(d => d.CreatorUser).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("questions_creator_user_id_fkey");

            entity.HasOne(d => d.QuestionType).WithMany(p => p.Questions).HasConstraintName("questions_question_type_id_fkey");
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
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_permissions_permission_id_fkey"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_permissions_role_id_fkey"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("primarykey_of_role_permissions");
                        j.ToTable("role_permissions");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<Guid>("PermissionId").HasColumnName("permission_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_id_pr");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Person).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_person_id");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_type_fk");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
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

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_type_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    [DbFunction("filter_docs_by_user")]
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
