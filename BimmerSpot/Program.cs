using BimmerSpot;

var builder = WebApplication.CreateBuilder(args);

builder.AddDefaults();
builder.ConfigureAuth();
builder.ConfigureDataBase();
builder.AddServices();



var app = builder.Build();

app.ConfigureAppDefaults();
app.Run();
