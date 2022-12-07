using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RecipeRestService.Security;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using RecipeRestService.Businesslogic;
using RecipesData.Database;

//using RecipesData.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    }
);
});

// Configure the JWT Authentication Service
builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = "JwtBearer";
        options.DefaultChallengeScheme = "JwtBearer";
    })
    .AddJwtBearer("JwtBearer", jwtOptions => {
        jwtOptions.TokenValidationParameters = new TokenValidationParameters()
        {
            // The SigningKey is defined in the TokenController class
            ValidateIssuerSigningKey = true,
            // IssuerSigningKey = new SecurityHelper(configuration).GetSecurityKey(),
            IssuerSigningKey =  new SecurityHelper(builder.Configuration).GetSecurityKey(),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "https://localhost:7088",
            ValidAudience = "https://localhost:7088",
            ValidateLifetime = true
        };
    });

builder.Services.AddSingleton<IUserData, UserDataControl>();
builder.Services.AddSingleton<IUserAccess, UserDatabaseAccess>();
builder.Services.AddSingleton<IAuthenticationData, AuthenticationDataControl>();
builder.Services.AddSingleton<IBambooSessionData, BambooSessionDataControl>();
builder.Services.AddSingleton<IBambooSessionAccess, BambooSessionDatabaseAccess>();
builder.Services.AddSingleton<IRecipeData, RecipedataControl>();
builder.Services.AddSingleton<IRecipeAccess, RecipeDatabaseAccess>();
builder.Services.AddSingleton<ISecurityHelper, SecurityHelper>();

var app = builder.Build();

app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    //.WithOrigins("https://localhost:3000") // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials()
                    );

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();