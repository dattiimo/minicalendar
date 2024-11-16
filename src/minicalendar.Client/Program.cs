using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using minicalendar.Common.Calendars;
using minicalendar.Common.Privacy.CookiePolicy;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddScoped<ICookiePolicyManager, CookiePolicyManagerForLocalStorage>();
builder.Services.AddScoped<ICalendarRepository, LocalStorageCalendarRepository>();

await builder.Build().RunAsync();
