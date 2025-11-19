using Groeiproject.BL;
using Groeiproject.DAL;
using Groeiproject.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Repository = Groeiproject.DAL.EF.Repository;

var builder = WebApplication.CreateBuilder(args);

//dependency injection
// Add services to the container.
builder.Services.AddDbContext<BakeryDbContext>(optionsBuilder => optionsBuilder.UseSqlite(@"Data source=..\GroeiprojectDatabase.db"));
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IManager, Manager>();

builder.Services.AddControllersWithViews();

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BakeryDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin += redirectContext =>
    {
        if (redirectContext.Request.Path.StartsWithSegments("/api"))
            redirectContext.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied += redirectContext =>
    {
        if (redirectContext.Request.Path.StartsWithSegments("/api"))
            redirectContext.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policyBuilder => policyBuilder.RequireRole("Admin"));
});

var app = builder.Build();

//init database
using (var scope = app.Services.CreateScope())
{
    BakeryDbContext ctx = scope.ServiceProvider.GetRequiredService<BakeryDbContext>();
    ctx.Database.EnsureDeleted();
    if (ctx.Database.EnsureCreated())
    {
        UserManager<IdentityUser> userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        RoleManager<IdentityRole> roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        IdentitySeeding(userMgr, roleMgr);
        DataSeeder.Seed(ctx);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void IdentitySeeding(UserManager<IdentityUser> userManager, RoleManager<IdentityRole>  roleManager)
{
    var user = new IdentityUser("user@app.com")
    {
        Email = "user@app.com"
    };
    userManager.CreateAsync(user, "Password1!").Wait();
    
    var admin = new IdentityUser("admin@app.com")
    {
        Email = "admin@app.com"
    };
    userManager.CreateAsync(admin, "Password1!").Wait();
    
    var rafael = new IdentityUser("rafael@app.com")
    {
        Email = "rafael@app.com"
    };
    userManager.CreateAsync(rafael, "Password1!").Wait();
    
    var kenneth = new IdentityUser("kenneth@app.com")
    {
        Email = "kenneth@app.com"
    };
    userManager.CreateAsync(kenneth, "Password1!").Wait();
    
    var secondUser = new IdentityUser("second.user@app.com")
    {
        Email = "second.user@app.com"
    };
    userManager.CreateAsync(secondUser, "Password1!").Wait();
    
    // roles
    const string adminRole = "Admin";
    roleManager.CreateAsync(new IdentityRole(adminRole)).Wait();
    // admin -> adminRole
    userManager.AddToRoleAsync(admin, adminRole).Wait();
    userManager.AddToRoleAsync(rafael, adminRole).Wait();
    userManager.AddToRoleAsync(kenneth, adminRole).Wait();
}

public partial class Program {}