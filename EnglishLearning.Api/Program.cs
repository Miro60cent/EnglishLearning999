using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using EnglishLearning.Api.Data;
using EnglishLearning.Api.Repositories;
using EnglishLearning.Api.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<TopicDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TopicConnection"))
);

builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<IResourceProfileRepository, ResourceProfileRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserProfileActivitiesRepository, UserProfileActivitiesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:7122", "https://localhost:7122")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();