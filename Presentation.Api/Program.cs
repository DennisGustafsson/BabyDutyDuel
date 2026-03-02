using Application.Extensions;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddApplication(builder.Configuration, builder.Environment);
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.MapOpenApi();
app.UseHttpsRedirection();

app.Run();