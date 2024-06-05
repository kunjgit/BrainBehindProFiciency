using Microsoft.EntityFrameworkCore;
using Proficiency.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("OurPolicy",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000",
                              "https://myawesomesite")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      }));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
var app = builder.Build();




app.UseCors("OurPolicy");



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
