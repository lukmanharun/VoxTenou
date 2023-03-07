using Serilog;
using VoxTenouApp;
using VoxTenouApp.Interfaces;
using VoxTenouApp.Services;

var builder = WebApplication.CreateBuilder(args);

#region Serilog

string environment = string.Empty;
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == null) throw new Exception("Environtment object null");

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.File("bin/AppLog/Log.text",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
    .Enrich.WithProperty("Environment", environment)
    .ReadFrom.Configuration(new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{environment}.json",
        optional: true)
    .Build())
    .CreateLogger();
builder.Host.UseSerilog();
#endregion
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IHttpServices, HttpServices>(client =>
{
    client.BaseAddress = new Uri("https://api-sport-events.php6-02.test.voxteneo.com");
});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAutoMapper(typeof(MapperProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}"
    );

app.Run();
