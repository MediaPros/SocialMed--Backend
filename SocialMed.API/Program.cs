using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SocialMed.API.Forums.Domain.Repositories;
using SocialMed.API.Forums.Domain.Services;
using SocialMed.API.Forums.Persistence.Repositories;
using SocialMed.API.Forums.Services;
using SocialMed.API.Groups.Domain.Repositories;
using SocialMed.API.Groups.Domain.Services;
using SocialMed.API.Groups.Persistence.Repositories;
using SocialMed.API.Groups.Services;
using SocialMed.API.Medical_Interconsultation.Domain.Repositories;
using SocialMed.API.Medical_Interconsultation.Domain.Services;
using SocialMed.API.Medical_Interconsultation.Persistence.Repositories;
using SocialMed.API.Medical_Interconsultation.Services;
using SocialMed.API.Reports.Domain.Repositories;
using SocialMed.API.Reports.Domain.Services;
using SocialMed.API.Reports.Persistence.Repositories;
using SocialMed.API.Reports.Services;
using SocialMed.API.Security.Authorization.Handlers.Implementations;
using SocialMed.API.Security.Authorization.Handlers.Interfaces;
using SocialMed.API.Security.Authorization.Middleware;
using SocialMed.API.Security.Authorization.Settings;
using SocialMed.API.Security.Domain.Repositories;
using SocialMed.API.Security.Domain.Services;
using SocialMed.API.Security.Persistence.Repositories;
using SocialMed.API.Security.Services;
using SocialMed.API.Shared.Domain.Repositories;
using SocialMed.API.Shared.Mapping;
using SocialMed.API.Shared.Persistence.Context;
using SocialMed.API.Shared.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


builder.Services.AddSwaggerGen(options =>
{
    // Add API Documentation Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MediaPros Socialmed API",
        Description = "MediaPros Socialmed RESTful API",
        TermsOfService = new Uri("https://socialmed.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "Mediapros",
            Url = new Uri("https://mediapros.com")
        },
        License = new OpenApiLicense
        {
            Name = "MediaPros Socialmed Resources License",
            Url = new Uri("https://socialmed.com/license")
        }
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer Scheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            Array.Empty<string>()
        }
    });
});



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());


builder.Services.AddRouting(options => options.LowercaseUrls = true);



//////////

builder.Services.AddScoped<IForumRepository,ForumRepository>();
builder.Services.AddScoped<IForumService, ForumService>();

builder.Services.AddScoped<ICommentRepository,CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<IChatRepository,ChatRepository>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddScoped<IMessageRepository,MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddScoped<IRatingRepository,RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();



builder.Services.AddScoped<IRecommendationRepository,RecommendationRepository>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtHandler, JwtHandler>();

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();



// AutoMapper Configuration

builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile), 
    typeof(ResourceToModelProfile));




var app = builder.Build();






// Validation for ensuring Database Objects are created


using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();