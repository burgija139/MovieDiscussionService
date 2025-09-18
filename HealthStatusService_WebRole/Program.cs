using HealthStatusService_WebRole.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Dodaj HttpClient
builder.Services.AddHttpClient<HealthMonitoringHttpService>();

// Ili direktno registruj servis
builder.Services.AddScoped<HealthMonitoringHttpService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Health}/{action=Index}/{id?}");

app.Run();
