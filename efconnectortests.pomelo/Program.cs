using efconnectortests.pomelo;
using efconnectortests.pomelo.EF.Entities;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<TestContext>(options =>
{
    options.UseMySql(
        "server=localhost;port=3306;database=efconntest;user=root;password=strong_password",
        new MySqlServerVersion(new Version(8, 4, 0))
    );
});

var host = builder.Build();
host.Run();
