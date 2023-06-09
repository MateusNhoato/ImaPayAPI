using ImaPayAPI.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Models.Profiles;
using ImaPayAPI.Services.DTO;
using ImaPayAPI.Services;
using ImaPayAPI.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiAuth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("ImaPayContext");
var sqlVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<ImayPayContext>(options =>
                options.UseMySql(connectionString, sqlVersion));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddScoped(typeof(DtoService));
builder.Services.AddScoped(typeof(TokenService));
builder.Services.AddScoped(typeof(LoginService));
builder.Services.AddScoped(typeof(RegisterUserService));
builder.Services.AddScoped(typeof(TransferHistoryService));
builder.Services.AddScoped(typeof(TransferService));

var key = Encoding.ASCII.GetBytes(TokenSettings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x => {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowAnyOrigin());

app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.Headers.Add("Acess-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Acess-Control-Allow-Headers", "*");
        context.Response.Headers.Add("Acess-Control-Allow-Methods", "*");
        context.Response.StatusCode = 200;
    }
    else
    {
        await next();
    }
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ImayPayContext>();
    context.Database.Migrate();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
