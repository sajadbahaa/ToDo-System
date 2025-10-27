using BussinesLayer.ServiceCollection;
using BussinesLayer.Services.Jwt;
using DataLayer.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Repositary.Services;

using ValiadtionLayer;
using WebApi.Middlewares;
using static DTLayer.Services.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IDatabaseConfigurator configurator = new DatabaseConfigurator();
configurator.Configure(builder.Services, builder.Configuration);
builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidation>();
builder.Services.AddRepoServices();
builder.Services.addBussinesServices();
builder.Services.AddJwtServices(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.ConfigureDatabase(builder.Configuration);
var app = builder.Build();
// ✅ أضف Middleware هنا
app.UseMiddleware<GlobalExceptionMiddleware>();
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
