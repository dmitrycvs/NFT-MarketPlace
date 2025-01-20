using Microsoft.AspNetCore.Components.Web;
using NFT.Client;
using NFT.Client.Services.CollectionServices;
using NFT.Client.Services.HistoryLogServices;
using NFT.Client.Services.NftServices;
using NFT.Client.Services.RoleServices;
using NFT.Client.Services.UserServices;
using MudBlazor.Services;
using NFT.Client.Services.MetaMaskServices;
using MudBlazor;


var builder = Microsoft.AspNetCore.Components.WebAssembly.Hosting.WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("NFT.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddMudServices();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("NFT.ServerAPI"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
//builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<INftService, NftService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IHistoryLogService, HistoryLogService>();
builder.Services.AddScoped<MetaMaskService>();

builder.Services.AddMudServices();

/*builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
    config.SnackbarConfiguration.PreventDuplicates = true;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000; // 5 seconds
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});*/



await builder.Build().RunAsync();
