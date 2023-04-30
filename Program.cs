using System.Reflection;
using KaizenCase.Interfaces;
using KaizenCase.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddScoped<IJsonService, JsonService>();
builder.Services.AddSingleton<ICodeGeneratorService, CodeGeneratorService>();
var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();