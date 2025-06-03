using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using System.Security.Claims;
using ReactApp1.Server.Models; // if ApplicationUser is in Models folder

var builder = WebApplication.CreateBuilder(args);

// 1. Database
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Identity (with Roles)
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddRoles<IdentityRole>() // <-- Required to assign/check roles
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 3. Other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Middleware
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();

// 5. Identity API
app.MapIdentityApi<ApplicationUser>();

// 6. Custom endpoints
app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();

app.MapGet("/pingauth", (ClaimsPrincipal user) =>
{
    var email = user.FindFirstValue(ClaimTypes.Email);
    return Results.Json(new { Email = email });
}).RequireAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

// 7. Swagger (for dev)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 8. Seed static employee user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    string email = "admin@gmail.com";
    string password = "Admin@1234";

    // Ensure employee role exists
    if (!await roleManager.RoleExistsAsync("employee"))
    {
        await roleManager.CreateAsync(new IdentityRole("employee"));
    }

    // Ensure user exists
    var user = await userManager.FindByEmailAsync(email);
    if (user == null)
    {
        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "employee");
        }
    }
}

app.Run();
