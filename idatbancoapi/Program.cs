using Microsoft.EntityFrameworkCore;
using idatbancoapi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IdatBankContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdatBankDb")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
