using Kassandra.Models;
using Kassandra.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<KassandraDatabaseSettings>(
    builder.Configuration.GetSection("KassandraDatabase")
);
builder.Services.AddSingleton<ShortsService>();
builder.Services.AddSingleton<ShortMessagesService>();
builder.Services.AddSingleton<KeysService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
