using System.Reflection;
using AutoWarden.Database;
using AutoWarden.Database.Configuration;
using AutoWarden.Api.Json;
using AutoWarden.Api.Middleware;
using AutoWarden.Api.Swagger;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(
    builder.Configuration.GetSection("MongoDB").Get<MongoDbSettings>()!
);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new MergePatchJsonConverter());    
});

builder.Services.AddLogging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AutoWarden API",
        Description = "API for management of AutoWarden actions.",
        Contact = new OpenApiContact
        {
            Name = "Kamil Pfaff",
            Url = new Uri("https://pfaff.app/"),
            Email = "kamil@pfaff.app"
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    // options.SchemaFilter<UnpatchableItemSchemaFilter>();
    options.OperationFilter<PatchMethodOperationFilter>();
    options.OperationFilter<GenericDescriberOperationFilter>();
});
builder.Services.AddSwaggerGenNewtonsoftSupport();
var app = builder.Build();

// Middleware stack
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.MapControllers();
app.Run();
