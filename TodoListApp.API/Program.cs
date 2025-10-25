using TodoListApp.Application.Interfaces;
using TodoListApp.Application.Services;
using TodoListApp.Domain.Interfaces;
using TodoListApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // A URL do seu projeto Supabase, que diz quem é destinado. Para o Supabase, é sempre "authenticated"
    options.Authority = builder.Configuration["Supabase:Url"];

    // O "público" para quem o token é destinado. Para o Supabase, é sempre "authenticated"
    options.Audience = "authenticated";

    // Configurações extra para garantir que a validação é feita corretamente
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false, // O Issuer do Supabase pode ser dinâmico, então não o validamos diretamente
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();