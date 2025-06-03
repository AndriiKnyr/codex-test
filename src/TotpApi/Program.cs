using MediatR;
using Microsoft.AspNetCore.Mvc;
using TotpApi.Application;
using TotpApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddScoped<ITotpService, TotpService>();
builder.Services.AddScoped<IActiveDirectoryService, ActiveDirectoryService>();

var app = builder.Build();

app.MapControllers();

app.Run();

public partial class Program { }
