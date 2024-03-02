using Repository_Layer.Context;
using Microsoft.EntityFrameworkCore;
using Manager_Layer.Interfaces;
using Manager_Layer.Services;
using Repository_Layer.Interfaces;
using Repository_Layer.Services;
using MassTransit;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs"); // Change "LogFile" to "Logs" for better naming convention

GlobalDiagnosticsContext.Set("myvar", logPath);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<FundoContext>(x => x.UseSqlServer(builder.Configuration["ConnectionStrings:FundoAppdb"]));

// Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(config["JWT:Key"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Add MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.UseHealthCheck(provider);
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    }));
});
builder.Services.AddMassTransitHostedService();

// Add Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "FundoNotes API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Add dependencies
builder.Services.AddTransient<IUserManager, UserManager>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<INoteManager, NoteManager>();
builder.Services.AddTransient<INoteRepository, NoteRepository>();
builder.Services.AddTransient<ILabelManager, LabelManager>();
builder.Services.AddTransient<ILabelRepository, LabelRepository>();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
