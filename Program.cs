using NextechApi.Application.Services;
using NextechApi.Application.StoryService;
using NextechApi.Infrastructure.IStoryRepository;
using NextechApi.Infrastructure.StoryRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IStoryRepository<>), typeof(StoryRepository<>));
builder.Services.AddScoped<IStoryService, StoryService>();
var app = builder.Build();


app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
