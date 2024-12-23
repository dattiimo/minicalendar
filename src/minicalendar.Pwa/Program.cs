using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using minicalendar.Common.Calendars;
using minicalendar.Common.Calendars.Publishing;
using minicalendar.Common.Core;
using minicalendar.Common.FeatureSwitch;
using minicalendar.Common.FileManager;
using minicalendar.Common.Privacy.CookiePolicy;
using minicalendar.Common.ViewOrientation;
using minicalendar.Pwa;
using minicalendar.Pwa.Calendars;
using minicalendar.Pwa.Calendars.Templates;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

// Enable trace logging in the development environment.
// Level is set in appsettings.json.
switch (builder.Configuration["Logging:LogLevel:Default"])
{
    case "Trace": builder.Logging.SetMinimumLevel(LogLevel.Trace);
        break;
}


builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddScoped<IFeatureSettings, FeatureSettingsForLocalStorage>();


builder.Services.AddScoped<ISelectedCalendarDates, SelectedCalendarDatesForInMemory>();


builder.Services.AddScoped<ICookiePolicyManager, CookiePolicyManagerForLocalStorage>();

builder.Services.AddScoped<TemplateCalendarRepository, TemplateCalendarRepository>();


builder.Services.AddScoped<ICalendarRepository, LocalStorageCalendarRepository>();
builder.Services.AddScoped<IViewOrientationService, ViewOrientationServiceForLocalStorage>();

builder.Services.AddScoped<DateDistance, DateDistance>();
builder.Services.AddScoped<TrackFactory, TrackFactory>();
builder.Services.AddScoped<FileManagerService, FileManagerService>();
builder.Services.AddScoped<ICalendarStorage, CalendarStorageForBlobStorage>();
builder.Services.AddScoped<CalendarViewOptionsStore, CalendarViewOptionsStore>();

builder.Services.AddScoped<IBlobStorageConnectionString, BlobStorageConnectionStringFromLocalStorage>();

await builder.Build().RunAsync();
