using BimmerSpot;

var builder = WebApplication.CreateBuilder(args);

builder.AddDefaults();
builder.ConfigureAuth();
builder.ConfigureDataBase();
builder.AddServices();
builder.AddUtilities();



var app = builder.Build();

app.ConfigureAppDefaults();
app.SeedRoles();
app.Run();