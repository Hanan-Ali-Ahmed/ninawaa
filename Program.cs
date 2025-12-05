using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ninwa_Employee.Data;
using Ninwa_Employee.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Ninwa_EmployeeContext")
    ?? throw new InvalidOperationException("Connection string 'Ninwa_EmployeeContext' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddScoped<UserImportService>();

var app = builder.Build();

// ✅ هنا نستخدم Scope بشكل صحيح مع await
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userImportService = services.GetRequiredService<UserImportService>();

    await SeedRolesAndUsers(roleManager, userManager);

    string excelFilePath = Path.Combine(app.Environment.ContentRootPath, "Users.xlsx");
    if (File.Exists(excelFilePath))
    {
        await userImportService.ImportUsersFromExcelAsync(excelFilePath);
    }
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

// ✅ استخدم RunAsync هنا
await app.RunAsync();

async Task SeedRolesAndUsers(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
{
    string[] roleNames = { "Admin", "User" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
            await roleManager.CreateAsync(new IdentityRole(roleName));
    }
    string UserFullName = "Admin";
    string adminUserName = "admin@gmail.com";
    string adminEmail = "admin@gmail.com";
    string adminPassword = "Admin@123";
    int Depart = 39;
    string Permissions = "EditUser,AddUser,CheckForm,Reports,Statistics,Settings";

    var adminUser = await userManager.FindByNameAsync(adminUserName);
    if (adminUser == null)
    {
        var newAdmin = new ApplicationUser
        {
            UserFullName = UserFullName,
            UserName = adminUserName,
            Email = adminEmail,
            EmailConfirmed = true,
            Depart = Depart, // القسم المخصص
            Permissions = Permissions // الصلاحيات المحددة
        };

        var result = await userManager.CreateAsync(newAdmin, adminPassword);
        if (result.Succeeded)
            await userManager.AddToRoleAsync(newAdmin, "Admin");
    }
}
