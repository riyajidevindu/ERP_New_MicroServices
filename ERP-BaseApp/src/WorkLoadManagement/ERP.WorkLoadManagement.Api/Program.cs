
using ERP.WorkLoadManagement.DataService.Data;
using ERP.WorkLoadManagement.DataService.Repositories;
using ERP.WorkLoadManagement.DataService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Get Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Intilizing the DB Context inside the Dependency Injection Container
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

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
