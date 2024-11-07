using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using NewsApp.Searches;
using NewsApp.AlertsSearches;
using NewsApp.Notifications;
using NewsApp.Articles;
using NewsApp.Themes;
using NewsApp.KeyWords;

namespace NewsApp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class NewsAppDbContext :
    AbpDbContext<NewsAppDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public NewsAppDbContext(DbContextOptions<NewsAppDbContext> options)
        : base(options)
    {

    }

    #region DB-SETS
    // DbSets de entidades
    public DbSet<Article> Articles { get; set; }
    public DbSet<Search> Searches { get; set; }
    public DbSet<AlertSearch> AlertsSearches { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Theme> Themes { get; set; }

    #endregion
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        #region builders entidades de dominio
        // Entidad Article
        builder.Entity<Article>(b =>
        {
            b.ToTable(NewsAppConsts.DbTablePrefix + "Articles", NewsAppConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Author).IsRequired().HasMaxLength(100);
            b.Property(x => x.Title).IsRequired().HasMaxLength(100);
            b.Property(x => x.Description).IsRequired();
            b.Property(x => x.Url).IsRequired();
            b.Property(x => x.UrlToImage);
            // b.Property(x => x.Language);
            b.Property(x => x.PublishedAt).IsRequired();
            b.Property(x => x.Content).IsRequired();
            b.Property(x => x.SourceName);
        });

        // Entidad Theme
        builder.Entity<Theme>(b =>
        {
            b.ToTable(NewsAppConsts.DbTablePrefix + "Themes", NewsAppConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);

            // relacion con KeyWord
            b.HasMany(x => x.KeyWords)
                .WithOne(x => x.Theme);

            // relacion para la lista de temas que el tema contiene
            b.HasMany<Theme>(t => t.Themes)
                .WithOne(t => t.ParentTheme);

            // relacion para la lista de articulos que el tema contiene
            b.HasMany<Article>(t => t.Articles)
                .WithOne(a => a.Theme);
        });

        // Entidad KeyWord
        builder.Entity<KeyWord>(b =>
        {
            b.ToTable(NewsAppConsts.DbTablePrefix + "KeyWords", NewsAppConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Keyword).IsRequired().HasMaxLength(150);
        });

        // Entidad busqueda
        builder.Entity<Search>(b =>
        {
            b.ToTable(NewsAppConsts.DbTablePrefix + "Searches", NewsAppConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.SearchString).IsRequired().HasMaxLength(200);
            b.Property(x => x.StartDateTime).IsRequired();
            b.Property(x => x.ResultsAmount).IsRequired();
            b.Property(x => x.EndDateTime).IsRequired();

            // definiendo relacion con Alert
            b.HasOne<AlertSearch>(s => s.AlertSearch)
                .WithOne(a => a.Search)
                .HasForeignKey<AlertSearch>(ad => ad.AlertOfSearchId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        // Entidad AlertSearch
        builder.Entity<AlertSearch>(b =>
        {
            b.ToTable(NewsAppConsts.DbTablePrefix + "AlertsSearches", NewsAppConsts.DbSchema);
            b.ConfigureByConvention();
        });

        // Entidad Notification
        builder.Entity<Notification>(b =>
        {
            b.ToTable(NewsAppConsts.DbTablePrefix + "Notifications",
                NewsAppConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Active).IsRequired();
            b.Property(x => x.Title).IsRequired().HasMaxLength(150);
            b.Property(x => x.DateTime).IsRequired();

            // relacion con Alert
            b.HasOne<AlertSearch>(s => s.Alert)
                .WithMany(g => g.Notifications)
                .HasForeignKey(s => s.AlertId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    #endregion
    }
}
