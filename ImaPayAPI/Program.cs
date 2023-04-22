using ImaPayAPI.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Models.Profiles;
using ImaPayAPI.Services.DTO;
using ImaPayAPI.Services;
using ImaPayAPI.Services.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ImayPayContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ImaPayContext") ?? throw new InvalidOperationException("Connection string 'webApiProcessosContext' not found.")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddScoped(typeof(DtoService));
builder.Services.AddScoped(typeof(GenerateTokenService));
builder.Services.AddScoped(typeof(LinkUserToTokenService));
builder.Services.AddScoped(typeof(ValidateAndReturnUserService));
builder.Services.AddScoped(typeof(LoginService));
builder.Services.AddScoped(typeof(RegisterUserService));
builder.Services.AddScoped(typeof(TransferHistoryService));
builder.Services.AddScoped(typeof(TransferService));



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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
