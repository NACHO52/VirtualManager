using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VirtualManager.Client;
using VirtualManager.Client.Services;
using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//My services
builder.Services.AddScoped<ISystemUserService, SystemUserService>();
builder.Services.AddScoped<IResourceService, ResourceService>();

builder.Services.AddMudServices();



await builder.Build().RunAsync();
