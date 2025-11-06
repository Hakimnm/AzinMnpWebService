using AzinMnpWebService.Filters;
using AzinMnpWebService.Middelware;
using AzinMnpWebService.Repositories;
using AzinMnpWebService.Services;
using AzinMnpWebService.Services.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddScoped<XmlConvertToModel>();
builder.Services.AddScoped<MnpSoapService>();
builder.Services.AddScoped<IMongoRepository, MongoRepository>();
builder.Services.AddScoped<LoggingService>();
builder.Services.AddScoped<IMnpOperationService, MnpOperationService>();
builder.Services.AddScoped<IAuthKeyService, AuthKeyService>();
builder.Services.AddScoped<AuthKeyValidationFilter>();

builder.Services.AddSwaggerGen(c => { c.OperationFilter<AuthKeyHeaderOperationFilter>(); });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<LogMiddelware>();

app.Run();