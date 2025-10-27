using MeuPonto.Data;
using MeuPonto.Repositories.Interface;
using MeuPonto.Repositories.Repository;
using MeuPonto.Services.Interface;
using MeuPonto.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adiciona o DapperContext como Singleton (ou Scoped, dependendo do seu uso)
builder.Services.AddSingleton<DapperContext>();

// Adiciona o Repositorio, registrando a interface e a implementacao (Scoped eh o ideal para Repositorios)
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITimePunchService, TimePunchService>();
builder.Services.AddScoped<ITimePunchRepository, TimePunchRepository>();

//// Adicionar o serviço que GERA o token
//builder.Services.AddScoped<ITokenService, TokenService>();

//// --- 2. Adicionar o Middleware de Autenticação JWT Bearer ---

//// Lendo configurações do JWT
//var jwtSettings = builder.Configuration.GetSection("Jwt");
//// A chave é convertida em bytes para uso criptográfico
//var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key not configured in appsettings.json."));

//builder.Services.AddAuthentication(options =>
//{
//    // Define o JWT Bearer como o esquema padrão de autenticação
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    // Apenas para desenvolvimento (mude para TRUE em produção)
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        // Chave secreta que valida a assinatura
//        IssuerSigningKey = new SymmetricSecurityKey(key),

//        ValidateIssuer = true,
//        ValidIssuer = jwtSettings["Issuer"], // "MeuPontoAPI"

//        ValidateAudience = true,
//        ValidAudience = jwtSettings["Audience"], // "MeuPontoApp"

//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero // Sem tolerância de tempo
//    };
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
