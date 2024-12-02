using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using minicalendar.Common.Calendars;
using minicalendar.Common.Calendars.Publishing;
using minicalendar.Common.Core;
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

builder.Logging.SetMinimumLevel(LogLevel.Trace);

builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddScoped<ISelectedCalendarDates, SelectedCalendarDatesForInMemory>();


builder.Services.AddScoped<ICookiePolicyManager, CookiePolicyManagerForLocalStorage>();

builder.Services.AddScoped<TemplateCalendarRepository, TemplateCalendarRepository>();
//builder.Services.AddScoped<ISelectedCalendarDates, SelectedCalendarDates>();


builder.Services.AddScoped<ICalendarRepository, LocalStorageCalendarRepository>();
builder.Services.AddScoped<IViewOrientationService, ViewOrientationServiceForLocalStorage>();

builder.Services.AddScoped<DateDistance, DateDistance>();
builder.Services.AddScoped<TrackFactory, TrackFactory>();
builder.Services.AddScoped<FileManagerService, FileManagerService>();
builder.Services.AddScoped<ICalendarStorage, CalendarStorageForBlobStorage>();

await builder.Build().RunAsync();
