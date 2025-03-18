using System.Text; //for encoding the JWT Key //it sets up the application's configuration & services
using Microsoft.AspNetCore.Authentication.JwtBearer; //for JWT authentication
using Microsoft.EntityFrameworkCore;//for Entity Framework Core
using Microsoft.IdentityModel.Tokens;//for jWT Token Valodation
using Microsoft.OpenApi.Models;//for Swagger documentation
using StudyHiveSync2.Controllers;//for accessing controllers
using StudyHiveSync2.Data;//for accessing db context
using System.Text.Json.Serialization;//for json serialization
using NETCore.MailKit.Core;//for email services
using StudyHiveSync2.Services;//for custom services
//using StudyHiveSync2.Services;


var builder = WebApplication.CreateBuilder(args); // Initializes a new instance of the WebApplication builder

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));// Configures the application to use SQL Server with the connection string from the configuration

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

//// Register custom services
builder.Services.AddScoped<PaymentService>(); // Registers PaymentService with a scoped lifetime
builder.Services.AddScoped<EmailService>();  // Registers EmailService with a scoped lifetime
// other services


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    }); // Configures the application to use JSON serialization with reference handling

//singletononly one instance of the service is created and shared across the application.
// Register EmailService as Singleton
// add this line in Program.cs                                                                                                               
builder.Services.AddSingleton<EmailService>(); 



// Configure JWT authentication
var jwtKey = builder.Configuration["jwtSettings:key"];                                                       //to retrieve the JWT key from the configuration.
if (string.IsNullOrEmpty(jwtKey))
{
    throw new ArgumentNullException("jwtSettings:key", "JWT Key is missing in configuration.");                 // Throws an exception if the JWT key is missing
}

var key = Encoding.ASCII.GetBytes(jwtKey);                                                          //encodes jwt key(To convert secret key used to sign the JWT token.
builder.Services.AddSingleton<IJwtTokenGenerator>(new JwtTokenGenerator(key));                       // Registers JwtTokenGenerator as a singleton

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;                          // Sets the default authentication scheme to JWT
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;                             // Sets the default challenge scheme to JWT
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;                                                                // Disables HTTPS metadata requirement
    options.SaveToken = true;                                                                            // Saves the token
    options.TokenValidationParameters = new TokenValidationParameters                                    // Configures the token validation parameters
    {
        ValidateIssuerSigningKey = true,                                                                  // Validates the issuer signing key
        IssuerSigningKey = new SymmetricSecurityKey(key),                                                  // Sets the issuer signing key
        ValidateIssuer = false,                                                                            // Disables issuer validation
        ValidateAudience = false                                                                           // Disables Audeince validation
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudyHiveSync2 API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

builder.Logging.ClearProviders(); // Optional: Clears default loggingproviders

builder.Logging.AddConsole(); // Adds console logging provider

builder.Logging.AddDebug(); // Adds Debug logging provider


// Set specific log levels for different libraries

builder.Logging.SetMinimumLevel(LogLevel.Information); // Default minimum level

builder.Logging.AddFilter("Microsoft", LogLevel.Warning); // For Microsoft.* namespaces

builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning); // For ASP.NET Core

builder.Logging.AddFilter("System", LogLevel.Warning); // For System.* namespaces

builder.Logging.AddFilter("OnlineCommunityPlatform", LogLevel.Trace);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyHiveSync Web API"));
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
////app.UseEndpoints(endpoints =>
////{
//    endpoints.MapControllers();
////});

app.MapControllers();

app.Run();








//using System.Text;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using StudyHiveSync2.Controllers;
//using StudyHiveSync2.Data;
//using System.Text.Json.Serialization;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//    });

//// Configure JWT authentication
//var jwtKey = builder.Configuration["jwtSettings:key"];
//if (string.IsNullOrEmpty(jwtKey))
//{
//    throw new ArgumentNullException("jwtSettings:key", "JWT Key is missing in configuration.");
//}

//var key = Encoding.ASCII.GetBytes(jwtKey);
//builder.Services.AddSingleton<IJwtTokenGenerator>(new JwtTokenGenerator(key));

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false
//    };
//});

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("User", policy => policy.RequireRole("User"));
//});

//// Add Swagger services
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudyHiveSync2 API", Version = "v1" });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Description = "Please enter a valid token",
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        BearerFormat = "JWT",
//        Scheme = "Bearer"
//    });
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] { }
//        }
//    });
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyHiveSync Web API"));
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();
