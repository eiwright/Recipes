using Microsoft.EntityFrameworkCore;

using Recipe.Service.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Recipes")));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
