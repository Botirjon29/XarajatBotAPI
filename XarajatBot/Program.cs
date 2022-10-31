using Microsoft.EntityFrameworkCore;
using Xarajat.Data.Context;
using XarajatBot.Options;
using XarajatBot.Repositories;
using XarajatBot.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<XarajatDbContext>(
    opt => opt.UseSqlite(builder.Configuration.GetConnectionString("XarajatBotDb"))
);

builder.Services.Configure<XarajatBotOptions>(
    builder.Configuration.GetSection(nameof(XarajatBotOptions)));

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoomRepository>();
builder.Services.AddScoped<TelegramBotService>();
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
