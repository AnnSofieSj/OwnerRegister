
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Services;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{

    services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ECEducation\databaslagring\OwnerRegister\Infrastructure\Data\localdatabaseOwnerRegister.mdf;Integrated Security=True;Connect Timeout=30"));

    services.AddScoped<AddressesRepository>();
    services.AddScoped<BreedersRepository>();
    services.AddScoped<BreedsRepository>();
    services.AddScoped<HorsesRepository>();
    services.AddScoped<OwnersRepository>();

    services.AddScoped<HorsesService>();
    services.AddScoped<BreedsService>();
    services.AddScoped<BreedersService>();
    services.AddScoped<OwnersService>();
    services.AddScoped<AddressesService>();

    services.AddSingleton<MenuService>();



}).Build();
builder.Start();

var menuService = builder.Services.GetRequiredService<MenuService>();
menuService.ShowMainMenu();
Console.ReadKey();

