using Microsoft.EntityFrameworkCore;
using ERP.LabEquipmentManagement.DataService.Data;
using ERP.LabEquipmentManagement.DataService.Repositories.Interfaces;
using ERP.LabEquipmentManagement.DataService.Repositories;
using ERP.LabEquipmentManagement.Api.Services.Interfaces;
using ERP.LabEquipmentManagement.Api.Services;
using MassTransit;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//setconnectin string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ILabEquipmentNotificationPublisherService, LabEquipmentNotificationPublisherService>();

builder.Services.AddMassTransit(conf =>
{
    conf.UsingRabbitMq((ctx, cfg) => {
        cfg.Host("localhost", "/", h => {
            h.Username("myuser");
            h.Password("mypass");
        });
    });
});

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


