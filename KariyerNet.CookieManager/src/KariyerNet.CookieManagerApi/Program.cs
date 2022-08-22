using KariyerNet.CookieManager.Data.Context;
using Microsoft.EntityFrameworkCore;
using KariyerNet.CookieManager.Business.ServiceRegistration;
using KariyerNet.CookieManager.Data.DataAccessRegistration;
using KariyerNet.CookieManager.Common.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<ICookieEngine, CookieEngine>

builder.Services.AddBusinessServices();
builder.Services.AddDataAccessServices();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<CookieSettingsContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("CookieSettingsCon")));

//builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

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