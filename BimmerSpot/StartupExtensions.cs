using BimmerSpot.Components;
using BimmerSpot.Components.Account;
using BimmerSpot.Data;
using BimmerSpot.Data.Models;
using BimmerSpot.Models.Eums;
using BimmerSpot.Services;
using BimmerSpot.Utilities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BimmerSpot;

public static class StartupExtensions
{
    public static void AddDefaults(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddHttpContextAccessor();

        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto;
        });
    }

    public static void ConfigureAuth(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
            .AddFacebook(options =>
                {
                    options.AppId = builder.Configuration["Meta:AppId"]!;
                    options.AppSecret = builder.Configuration["Meta:AppSecret"]!;
                })
            .AddIdentityCookies();
    }

    public static void ConfigureDataBase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                sqlOptions => sqlOptions.EnableRetryOnFailure()));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services
            .AddIdentityCore<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
                })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
        builder.Services.AddScoped<ISpotService, SpotService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddSingleton<IFeatureFlagService, FeatureFlagService>();
        builder.Services.AddSingleton<TextDescriptionService>();
        builder.Services.AddScoped<IMessageService, MessageService>();
    }

    public static void AddUtilities(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IdentityErrorDescriber, PolishIdentityErrorDescriber>();
    }

    public static void ConfigureAppDefaults(this WebApplication app)
    {
        app.UseForwardedHeaders();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();
        app.UseAntiforgery();
        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        app.MapAdditionalIdentityEndpoints();
    }

    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        db.Database.Migrate();
    }

    public async static void SeedRoles(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        var adminRole = UserRole.Admin.ToString();

        //Create role
        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            var result = await roleManager.CreateAsync(new IdentityRole(adminRole));

            if (!result.Succeeded)
            {
                throw new Exception(
                    $"Nie udało się utworzyć roli: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        //Grant admin
        var user = await userManager.FindByEmailAsync("portalus2@tutanota.com");

        if (user is not null && !await userManager.IsInRoleAsync(user, "Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }

}
