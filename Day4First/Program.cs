using AspNetCoreHero.ToastNotification;
using Day4First.Data;
using Day4First.Models;
using Day4First.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAuthor, AuthorRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});
builder.Services.AddTransient<SessionService>(); // Register SessionService
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 10;
    config.IsDismissable = true;
    config.Position = NotyfPosition.BottomRight;
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Home}/{id?}");

app.Run();
