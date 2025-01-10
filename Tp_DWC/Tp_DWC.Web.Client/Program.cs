using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tp_DWC.Shared.Services;
using Tp_DWC.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the Tp_DWC.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
