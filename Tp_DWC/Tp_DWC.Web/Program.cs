global using Microsoft.EntityFrameworkCore;
using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Services;
using Tp_DWC.Web.Components;
using Tp_DWC.Web.Services;
using Tp_DWC.Web.Services.ClienteService;
using Tp_DWC.Web.Services.MoradaService;
using Tp_DWC.Web.Services.ContactoService;
using Tp_DWC.Web.Services.EmailService;
using System.Text.Json.Serialization;
using Tp_DWC.Web.Services.AssistenciaService;
using Tp_DWC.Web.Services.RegistoFotograficoService;
using Tp_DWC.Web.Services.RegistoMaoDeObraService;
using Tp_DWC.Web.Services.RegistoMaterialService;
using Tp_DWC.Web.Services.EstadoService;

var builder = WebApplication.CreateBuilder(args);
var apiBaseAddress = builder.Configuration["ApiBaseAddress"];
//variavel para a ConnectionString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add device-specific services used by the Tp_DWC.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

//add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//fim

//Adicionar os services do Backend -> Tp_DWC.Web.Services.ClienteService
//builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IMoradaService, MoradaService>();
builder.Services.AddScoped<IContactoService, ContactoService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAssistenciaService, AssistenciaService>();
builder.Services.AddScoped<IRegFotService, RegFotService>();
builder.Services.AddScoped<IRegMaoService, RegMaoService>();
builder.Services.AddScoped<IRegMatService, RegMatService>();
builder.Services.AddScoped<IEstadoService, EstadoService>();

//Adcionar os services do Frontend
builder.Services.AddScoped<Tp_DWC.Shared.Services.ClienteService.IClienteService, Tp_DWC.Shared.Services.ClienteService.ClienteService>();
builder.Services.AddHttpClient<Tp_DWC.Shared.Services.ClienteService.IClienteService, Tp_DWC.Shared.Services.ClienteService.ClienteService>(client =>
{
    //client.BaseAddress = new Uri(apiBaseAddress);
    client.BaseAddress = new Uri(apiBaseAddress ?? "https://localhost:7258/"); // Base URL do seu backend
});
builder.Services.AddScoped<Tp_DWC.Shared.Services.ContactoService.IContactoService, Tp_DWC.Shared.Services.ContactoService.ContactoService>();
builder.Services.AddHttpClient<Tp_DWC.Shared.Services.ContactoService.IContactoService, Tp_DWC.Shared.Services.ContactoService.ContactoService>(client =>
{
    client.BaseAddress = new Uri(apiBaseAddress ?? "https://localhost:7258/");
});
builder.Services.AddScoped<Tp_DWC.Shared.Services.MoradaService.IMoradaService, Tp_DWC.Shared.Services.MoradaService.MoradaService>();
builder.Services.AddHttpClient<Tp_DWC.Shared.Services.MoradaService.IMoradaService, Tp_DWC.Shared.Services.MoradaService.MoradaService>(client =>
{
    client.BaseAddress = new Uri(apiBaseAddress ?? "https://localhost:7258/");
});
builder.Services.AddScoped<Tp_DWC.Shared.Services.EmailService.IEmailService, Tp_DWC.Shared.Services.EmailService.EmailService>();
builder.Services.AddHttpClient<Tp_DWC.Shared.Services.EmailService.IEmailService, Tp_DWC.Shared.Services.EmailService.EmailService>(client =>
{
    client.BaseAddress = new Uri(apiBaseAddress ?? "https://localhost:7258/");
});
builder.Services.AddScoped<Tp_DWC.Shared.Services.AssistenciaService.IAssistenciaService, Tp_DWC.Shared.Services.AssistenciaService.AssistenciaService>();
builder.Services.AddHttpClient<Tp_DWC.Shared.Services.AssistenciaService.IAssistenciaService, Tp_DWC.Shared.Services.AssistenciaService.AssistenciaService>(client =>
{
    client.BaseAddress = new Uri(apiBaseAddress ?? "https://localhost:7258/");
});
builder.Services.AddScoped<Tp_DWC.Shared.Services.RegistoFotograficoService.IRegFotService, Tp_DWC.Shared.Services.RegistoFotograficoService.RegFotService>();
builder.Services.AddHttpClient<Tp_DWC.Shared.Services.RegistoFotograficoService.IRegFotService, Tp_DWC.Shared.Services.RegistoFotograficoService.RegFotService>(client =>
{
    client.BaseAddress = new Uri(apiBaseAddress ?? "https://localhost:7258/");
});
builder.Services.AddScoped<Tp_DWC.Shared.Services.RegistoMaoDeObraService.IRegMaoService, Tp_DWC.Shared.Services.RegistoMaoDeObraService.RegMaoService>();
builder.Services.AddHttpClient<Tp_DWC.Shared.Services.RegistoMaoDeObraService.IRegMaoService, Tp_DWC.Shared.Services.RegistoMaoDeObraService.RegMaoService>(client =>
{
    client.BaseAddress = new Uri(apiBaseAddress ?? "https://localhost:7258/");
});
builder.Services.AddScoped<Tp_DWC.Shared.Services.RegistoMaterialService.IRegMatService, Tp_DWC.Shared.Services.RegistoMaterialService.RegMatService>();
builder.Services.AddHttpClient<Tp_DWC.Shared.Services.RegistoMaterialService.IRegMatService, Tp_DWC.Shared.Services.RegistoMaterialService.RegMatService>(client =>
{
    client.BaseAddress = new Uri(apiBaseAddress ?? "https://localhost:7258/");
});
builder.Services.AddScoped<Tp_DWC.Shared.Services.EstadoService.IEstadoService, Tp_DWC.Shared.Services.EstadoService.EstadoService>();
builder.Services.AddHttpClient<Tp_DWC.Shared.Services.EstadoService.IEstadoService, Tp_DWC.Shared.Services.EstadoService.EstadoService>(client =>
{
    client.BaseAddress = new Uri(apiBaseAddress ?? "https://localhost:7258/");
});

// Adicionar suporte a controladores
//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();


var service = app.Services.GetService<Tp_DWC.Shared.Services.ClienteService.IClienteService>();
if (service == null)
{
    Console.WriteLine("Serviço IClienteService (Shared) não encontrado!");
}
else
{
    Console.WriteLine("Serviço IClienteService (Shared) registrado com sucesso!");
}

// Configure o Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.UseStaticFiles();
app.UseAntiforgery();

// Mapear APIs
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(Tp_DWC.Shared._Imports).Assembly,
        typeof(Tp_DWC.Web.Client._Imports).Assembly);

app.Run();
