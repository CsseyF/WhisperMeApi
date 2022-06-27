using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WhisperMe.Database;
using WhisperMe.Helpers;
using WhisperMe.Repository.Interfaces;
using WhisperMe.Repository;
using WhisperMe.Services.Interfaces;
using WhisperMe.Services;


string AllowEverything = "AllowEverything";
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
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IWhisperService, WhisperService>();

//Others
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddAutoMapper(typeof(Mappers));

var app = builder.Build();


app.UseMiddleware<JwtMiddleware>();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.WithOrigins("https://vercel.app", "https://whisperme.vercel.app", "localhost:3000");
    builder.AllowCredentials();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
    builder.Build();
});

app.UseCors(AllowEverything);

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
