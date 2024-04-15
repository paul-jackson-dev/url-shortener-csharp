using Microsoft.EntityFrameworkCore;
using url_shortner_api.Data;
using Microsoft.AspNetCore.Identity;
using url_shortner_api.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("UrlShortnerDbContextConnection") ?? throw new InvalidOperationException("Connection string 'UrlShortnerDbContextConnection' not found.");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UrlShortnerDbContext>(o => o.UseSqlite("Data Source=sqlite.db;")); // add DbContext to services and connect to SQLite // possibly consider adding options in the future


// default configuration for a cookie scheme
builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;

})
    .AddEntityFrameworkStores<UrlShortnerDbContext>()
    .AddApiEndpoints(); // allows api to create IdentityUsers

// Add Cookie Authentication
//builder.Services.AddAuthentication()
//    .AddIdentityCookies();

//builder.Services.AddAuthentication()
//    .AddBearerToken(IdentityConstants.BearerScheme); // we have a authentication scheme and want to support bearer tokens

// Add Authorization
//builder.Services.AddAuthorizationBuilder();



// Configure Identity Cookies
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
//    options.SlidingExpiration = true;
//});
//.AddBearerToken(IdentityConstants.BearerScheme); // we have a authentication scheme and want to support bearer tokens

//builder.Services.AddIdentityApiEndpoints<User>() // another way to add login Identity login endpoints
//    .AddEntityFrameworkStores<UrlShortnerDbContext>();

//builder.Services.AddAuthorizationBuilder() // add an auth policy to use with [Authorize]
//    .AddPolicy("api", policy =>
//    {
//        policy.RequireAuthenticatedUser();
//        policy.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme);
//        //policy.AddAuthenticationSchemes(IdentityConstants.BearerScheme);
//    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.MapIdentityApi<AppUser>();
app.MapGroup("-/api/auth") // create register, login, logout endpoints -/api/auth/register 
    .MapIdentityApi<AppUser>();

app.Run();
