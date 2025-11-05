using AzinMnpWebService.Middelware;
using AzinMnpWebService.Repositories;
using AzinMnpWebService.Services;
using AzinMnpWebService.Services.Authorization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<XmlConvertToModel>();
builder.Services.AddScoped<MnpSoapService>();
builder.Services.AddScoped<IMongoRepository,MongoRepository>();
builder.Services.AddScoped<LoggingService>();
builder.Services.AddScoped<IMnpOperationService,MnpOperationService>();
builder.Services.AddScoped<IAuthKeyService,AuthKeyService>();

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