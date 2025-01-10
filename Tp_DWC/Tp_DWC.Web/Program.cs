global using Microsoft.EntityFrameworkCore;
using Tp_DWC.Shared.Data;
using Tp_DWC.Shared.Services;
using Tp_DWC.Web.Components;
using Tp_DWC.Web.Services;
using Tp_DWC.Web.Services.ClienteService;

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

//Adicionar os services do Backend
builder.Services.AddScoped<IClienteService, ClienteService>();

//Adcionar os services do Frontend
//builder.Services.AddScoped<Tp_DWC.Shared.Services.ClienteService.IClienteService, Tp_DWC.Shared.Services.ClienteService.ClienteService>();


builder.Services.AddHttpClient<Tp_DWC.Shared.Services.ClienteService.IClienteService, Tp_DWC.Shared.Services.ClienteService.ClienteService>(client =>
{
    client.BaseAddress = new Uri(apiBaseAddress);
});

// Adicionar suporte a controladores
builder.Services.AddControllers();

var app = builder.Build();

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
