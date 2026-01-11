    using Microsoft.EntityFrameworkCore;
using SmartWallet.Repositories;
using SmartWallet.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<MyApi.Data.AppDbContext>(options =>
    options.UseSqlServer(builder?.Configuration.GetConnectionString("DefaultConnection")));

// DI: repository + service
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:3000",
                    "http://localhost:4200"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
