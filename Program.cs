using MeuPonto.Data;
using MeuPonto.Repositories.Interface;
using MeuPonto.Repositories.Repository;
using MeuPonto.Services.Interface;
using MeuPonto.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adiciona o DapperContext como Singleton (ou Scoped, dependendo do seu uso)
builder.Services.AddSingleton<DapperContext>();

// Adiciona o Repositorio, registrando a interface e a implementacao (Scoped eh o ideal para Repositorios)
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
