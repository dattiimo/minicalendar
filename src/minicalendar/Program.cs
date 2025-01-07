using Blazored.LocalStorage;
using minicalendar;
using minicalendar.Common.Calendars;
using minicalendar.Common.Calendars.Publishing;
using minicalendar.Common.Core;
using minicalendar.Common.Privacy.CookiePolicy;
using minicalendar.Common.ViewOrientation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Compression - https://learn.microsoft.com/en-us/aspnet/core/performance/response-compression?view=aspnetcore-9.0#risk
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddScoped<ICookiePolicyManager, CookiePolicyManagerForLocalStorage>();
builder.Services.AddScoped<ICalendarRepository, LocalStorageCalendarRepository>();
builder.Services.AddScoped<ICalendarStorage, CalendarStorageForBlobStorage>();
builder.Services.AddScoped<IViewOrientationService, ViewOrientationServiceForServer>();

builder.Services.AddScoped<DateDistance, DateDistance>();
builder.Services.AddScoped<TrackFactory, TrackFactory>();

builder.Services.AddSingleton<IBlobStorageConnectionString, BlobStorageConnectionStringFromConfig>();


var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapStaticAssets();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(minicalendar.Client._Imports).Assembly);

app.Run();
