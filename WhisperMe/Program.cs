using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WhisperMe.Database;
using WhisperMe.Helpers;
using WhisperMe.Repository.Interfaces;
using WhisperMe.Repository;
using WhisperMe.Services.Interfaces;
using WhisperMe.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BaseContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Default"))
);


//Dependency Injection
//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWhisperRepository, WhisperRepository>();

//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWhisperService, WhisperService>();

//Others
builder.Services.AddScoped<IJwtUtils, JwtUtils>();


builder.Services.AddAutoMapper(typeof(Mappers));

var app = builder.Build();


app.UseMiddleware<JwtMiddleware>();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
    builder.Build();
});

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
