using Example.Controllers;
using ThereFox.JsonRPC.AspNet.Register.DIRegister;
using ThereFox.JsonRPC.AspNet.Register.Filtrs;
using ThereFox.JsonRPC.Core.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegistrateJSONRpcClient($"http://localhost:5223/test");

builder.Services
    .AddJsonRPC()
    .AddJsonRPCStandartResponseFormatter();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.Services.RegistrateActionController<SimpleController>();
app.Services.RegistrateActionControllersFromAssembly(typeof(Program).Assembly);

app.MapJsonRPCRoute("test");

app.Run();