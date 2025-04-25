using Microsoft.EntityFrameworkCore;
using System;
using DAL;
using BLL.ServiceInterfaces;
using BLL_DB.Services;
using BLL_EF.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

//builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentService, StudentServiceDB>();
//builder.Services.AddScoped<IHistoriaService, HistoriaService>();
builder.Services.AddScoped<IHistoriaService, HistoriaServiceDB>();
builder.Services.AddScoped<IGrupaService, GrupaServiceDB>();



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

app.UseAuthorization();

app.MapControllers();

app.Run();
