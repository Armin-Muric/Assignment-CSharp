using Assignment-CSharp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Interfaces;
using Shared.Models;
using Shared.Services;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddSingleton<ContactService>();
    services.AddSingleton<FileService>();
    services.AddSingleton<List<ContactModel>>();
    services.AddSingleton<MenuService>();
}).Build();

// var contactService = builder.Services.GetRequiredService<IContactService>();
// contactService.GetAllContactsFromList();

Console.Clear();
builder.Start();

var menuService = builder.Services.GetRequiredService<MenuService>();
menuService.ShowMainMenu();