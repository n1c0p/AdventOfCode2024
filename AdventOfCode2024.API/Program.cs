var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Automatic DI with Scrutor
builder.Services.DependecyInjection();
#endregion

builder.Services.AddOperationResult(options =>
{
    options.ErrorResponseFormat = ErrorResponseFormat.Default;
}, updateModelStateResponseFactory: true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger Configuration
builder.Services.SwaggerConfiguration();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
