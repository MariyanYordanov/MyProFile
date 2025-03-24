
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;

var builder = WebApplication.CreateBuilder(args);

// Добавяме услугите
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyProFileDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Конфигурираме middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); // ЗАДЪЛЖИТЕЛНО
app.UseAuthorization();

app.MapControllers(); // ЗАДЪЛЖИТЕЛНО

app.Run();
