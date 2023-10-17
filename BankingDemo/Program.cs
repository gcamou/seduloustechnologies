using BankingDemo.Domain.Abstractions;
using BankingDemo.Domain.Abstractions.Services;
using BankingDemo.Domain.Abstractions.Services.IValidators;
using BankingDemo.Domain.Entities;
using BankingDemo.Infrastructure;
using BankingDemo.Infrastructure.Repositories;
using BankingDemo.Service;
using BankingDemo.Service.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies()
        .UseInMemoryDatabase("DbDemo"));

builder.Services.AddScoped<IAccountRespository<Account>, AccountRepository>();
builder.Services.AddScoped<IUserRepository<User>, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountValidator, AccountValidator>();

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
