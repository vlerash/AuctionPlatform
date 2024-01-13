using AuctionPlatform.Business._01_Common;
using AuctionPlatform.Domain.Entities;
using AuctionPlatform.Extensions;
using AuctionPlatforn.Infrastructure;
using AutoMapper;
using BlogManagementSystem.Business._00_Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

// Add services to the container. 
//builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();