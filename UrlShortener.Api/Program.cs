using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UrlShortener.Api;
using UrlShortener.Api.BLL.Services;
using UrlShortener.Api.BLL.Utils;
using UrlShortener.Api.BLL.Validators;
using UrlShortener.Api.DAL;
using UrlShortener.Api.DAL.Repositories;
using UrlShortener.Api.DTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("AllowAll", options =>
    {
        options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtConfiguration:SecretKey"])),
        ValidIssuer = builder.Configuration["JwtConfiguration:Issuer"],
        ValidAudience = builder.Configuration["JwtConfiguration:Audience"]
    };
    x.MapInboundClaims = false;
});
builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<UrlsDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUrlsRepository, UrlsRepository>();

builder.Services.AddScoped<AbstractValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<AbstractValidator<LoginUserDto>, LoginUserDtoValidator>();
builder.Services.AddScoped<AbstractValidator<UrlCreateDto>, UrlCreateDtoValidator>();

builder.Services.AddSingleton<JwtGenerator>();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUrlsService, UrlsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();