using AuctionPlatform.Domain.Entities;
using AuctionPlatform.Extensions;
using AuctionPlatforn.Infrastructure;
using AutoMapper;
using BlogManagementSystem.Business._00_Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuctionPlatformDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AuctionPlatformDbContext>()
    .AddDefaultTokenProviders();

builder.Services.RegisterBusinessLayerDependencies();
builder.Services.RegisterDataAccessLayerDependencies();

#region AutoMapper
var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfiles()); });
var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

// Register AutoMapper using the AddAutoMapper method
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
//        };

//        options.Events = new JwtBearerEvents
//        {
//            OnAuthenticationFailed = context =>
//            {
//                Log.Logger.Error("Authentication failed: {0}", context.Exception);
//                return Task.CompletedTask;
//            },
//            OnTokenValidated = context =>
//            {
//                Log.Logger.Information("Token validated: {0}", context.SecurityToken);
//                return Task.CompletedTask;
//            },
//            OnMessageReceived = context =>
//            {
//                Log.Logger.Information("Message received: {0}", context.Request.Headers);
//                return Task.CompletedTask;
//            },
//            // Add other event handlers if needed
//        };
//    });

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });

    // Configure JWT Bearer authentication for Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } }
    });
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/auctionplatform-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction Platform"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
