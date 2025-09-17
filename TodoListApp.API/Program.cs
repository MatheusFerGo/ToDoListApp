using TodoListApp.Application.Interfaces;
using TodoListApp.Application.Services;
using TodoListApp.Domain.Interfaces;
using TodoListApp.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// 1. Ler as configurações do Supabase do appsettings.json
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseApiKey = builder.Configuration["Supabase:ApiKey"];

// 2. Criar uma instância do cliente do Supabase
var supabaseOptions = new Supabase.SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
};
var supabase = new Supabase.Client(supabaseUrl, supabaseApiKey, supabaseOptions);

// 3. Adicionar o cliente como um serviço Singleton para que ele possa ser injetado
builder.Services.AddSingleton(supabase);

// Agora sim, o resto das nossas injeções de dependência
builder.Services.AddScoped<IItemsRepository, SupabaseItemsRepository>();
builder.Services.AddScoped<IItemServices, ItemServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();