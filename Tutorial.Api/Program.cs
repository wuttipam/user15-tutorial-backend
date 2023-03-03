using Tutorial.Api.Models;
using Tutorial.Api.Services;

var builder = WebApplication.CreateBuilder(args);

//CORS : https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                      });
});


// Add services to the container.

// add mapping config json to objects
builder.Services.Configure<TutorialDatabaseSettings>(builder.Configuration.GetSection("TutorialDatabase"));

builder.Services.AddSingleton<ITutorialService,TutorialService>();

builder.Services.AddControllers();

// The following line enables Application Insights telemetry collection.
builder.Services.AddApplicationInsightsTelemetry();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Health probe
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
