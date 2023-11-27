using Todo.Api;
using Todo.Api.Common.Settings;
using Todo.Application;
using Todo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();
await app
    .AddSettings()
    .RunAsync();