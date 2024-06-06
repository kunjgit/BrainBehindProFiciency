using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Microsoft.EntityFrameworkCore;
using Proficiency.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Xml.Linq;
using DotNetEnv.Configuration;
using DotNetEnv;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddDotNetEnv(
    ".env", DotNetEnv.LoadOptions.TraversePath()
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Proficiency", Version = "0.1" });
    
});
builder.Services.AddCors(o => o.AddPolicy("OurPolicy",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000",
                              "https://myawesomesite")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      }));


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

app.UseCors("OurPolicy");

app.UseSwagger();
    
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
