using ServiceContracts;
using Services;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ITimeReportService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var myNotSoSecretToken = configuration.GetValue<string>("MySettings:MyNotSoSecretToken");
    var baseAddress = configuration.GetValue<string>("MySettings:BaseAddress");

    return new TimeReportService(myNotSoSecretToken, baseAddress);
});
builder.Services.AddSingleton<IControllerService, ControllerService>();
builder.Services.AddSingleton<IDataAccessService, DataAccessService>(provider =>
{
    
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    
    return new DataAccessService(connectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();

app.Run();
