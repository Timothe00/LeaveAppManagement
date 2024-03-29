﻿using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Interfaces.AuthInterface;
using LeaveAppManagement.businessLogic.Interfaces.EmailModelService;
using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.businessLogic.Services.AuthService;
using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<LeaveAppManagementDbContext>(Option =>
{
    Option.UseSqlServer(builder.Configuration.GetConnectionString("LinkCs"));
});



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "LeaveAppManagement",
        Version = "1.0.0",
        Description = "Documentation d'une plateforme de gestion de congé"
    });

    string fichier = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string fichierXml = Path.Combine(AppContext.BaseDirectory, fichier);
    c.IncludeXmlComments(fichierXml);
});


//Gesttion des erreurs d'ent�te CORS
string? corsOrigin = builder.Configuration.GetSection("CorsOrigin").Get<string>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();
builder.Services.AddScoped<ILeaveReportingService, LeaveReportingService>();
builder.Services.AddScoped<ILeaveBalenceService, LeaveBalanceService>();
builder.Services.AddScoped<IAllRequestAcceptedService, AllRequestAcceptedService>();
builder.Services.AddScoped< IEmailModelService, EmailModelService>();
builder.Services.AddScoped<IExportExcelService, ExportExcelService>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
builder.Services.AddScoped<ILeaveReportingRepository, LeaveReportingRepository>();
builder.Services.AddScoped<ILeaveBalanceRepository, LeaveBalanceRepository>();
builder.Services.AddScoped<IAllRequestsAcceptedRepository, AllRequestAcceptedRepository>();
// Add services to the container.


//injection de jwtBearer(Authentication)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            SaveSigninToken = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = string.Empty;
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseCors();
//ajouter UseAuthentication() pour dire qu'avant toute authorisation il faut se connecter
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
