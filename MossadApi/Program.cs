using Microsoft.EntityFrameworkCore;
using MossadApi.DAL;
using Microsoft.SqlServer;

using Microsoft.Extensions.Logging;
using System.Text.Json;
using MossadApi;

var builder = WebApplication.CreateBuilder(args);


string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Icalculatlocation, calculation>();
builder.Services.AddScoped<ISetmission, SetMission>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseWhen(
//    context =>
//        context.Request.Path.StartsWithSegments("/api/attacks"),
//    appBuilder =>
//    {
//        appBuilder.UseMiddleware<AttackLoggingMiddleware>();
//        // appBuilder.UseMiddleware<JwtValidationToken>();

//    });


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
