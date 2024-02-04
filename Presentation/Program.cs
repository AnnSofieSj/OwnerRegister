
using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
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

    services.AddScoped<BreedsService>();
    services.AddSingleton<MenuService>();



}).Build();
builder.Start();

var menuService = builder.Services.GetRequiredService<MenuService>();
menuService.AddNewBreed();
Console.ReadKey();

//var breedsService = builder.Services.GetRequiredService<BreedsService>();
//await breedsService.CreateBreedAsync("Connemara");







//Console.ReadKey();