using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tp_DWC.Shared.Services;
using Tp_DWC.Shared.Services.AssistenciaService;
using Tp_DWC.Shared.Services.ClienteService;
using Tp_DWC.Shared.Services.ContactoService;
using Tp_DWC.Shared.Services.EmailService;
using Tp_DWC.Shared.Services.MoradaService;
using Tp_DWC.Shared.Services.RegistoFotograficoService;
using Tp_DWC.Shared.Services.RegistoMaoDeObraService;
using Tp_DWC.Shared.Services.RegistoMaterialService;
using Tp_DWC.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the Tp_DWC.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

// Registra o HttpClient padrão
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7258/") // URL da API Backend
});

//Adcionar os services do Frontend
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IMoradaService, MoradaService>();
builder.Services.AddScoped<IContactoService, ContactoService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAssistenciaService, AssistenciaService>();
builder.Services.AddScoped<IRegFotService, RegFotService>();
builder.Services.AddScoped<IRegMaoService, RegMaoService>();
builder.Services.AddScoped<IRegMatService, RegMatService>();

await builder.Build().RunAsync();
