using Microsoft.EntityFrameworkCore;
using VEHICLE_MANAGEMENT_SYSTEM.Abstract;
using VEHICLE_MANAGEMENT_SYSTEM.Concreate;
using VEHICLE_MANAGEMENT_SYSTEM.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<vehicledb_context>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));
builder.Services.AddTransient<Interface,DemoRepository>();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Vehicle}/{id?}");

app.Run();
