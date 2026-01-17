using Impact.Services;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ITendersService, TendersService>();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();
try
{
    app.MapControllers();
}
catch (ReflectionTypeLoadException ex)
{
    foreach (var loaderException in ex.LoaderExceptions)
    {
        Console.WriteLine(loaderException?.Message);
    }
    throw;
}

app.Run();
