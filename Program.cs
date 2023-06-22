using Blazored.SessionStorage;
using BlazorTableOfMultiply;
using BlazorTableOfMultiply.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Third-party services
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddSpeechSynthesis();

builder.Services.AddScoped<CurrentVoiceOptions>();
builder.Services.AddScoped<VoicesService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
