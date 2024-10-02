using Common.AppSettings.TestAPI;
using DBContext.SQLServer;
using Microsoft.EntityFrameworkCore;
using Domain.EmployeeWage.UnitOfWorks;
using Common.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<ILogicUnitOfWork, LogicUnitOfWork>();
//builder.Services.AddScoped<IRepositoryUnit, RepositoryUnit>();
//builder.Services.Configure<MongoDBOptions>(builder.Configuration.GetSection("MongoDBOptions"));
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
