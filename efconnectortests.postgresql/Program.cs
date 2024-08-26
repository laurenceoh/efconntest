using efconnectortests.postgresql;
using efconnectortests.postgresql.EF.Entities;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<TestContext>(options =>
{
    options.UseNpgsql("host=localhost;port=54320;database=efconntest;username=postgres;password=strong_password");
});

var host = builder.Build();
host.Run();
