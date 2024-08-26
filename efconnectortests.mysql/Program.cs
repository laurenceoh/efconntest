using efconnectortests.mysql;
using efconnectortests.mysql.EF.Entities;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<TestContext>(options =>
{
    options.UseMySQL("server=localhost;port=3306;database=efconntest;user=root;password=strong_password");
}); 

var host = builder.Build();
host.Run();
