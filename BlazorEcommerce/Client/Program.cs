global using Microsoft.AspNetCore.Components.Authorization;
using BlazorEcommerce.Client;
using BlazorEcommerce.Client.Stores;
using BlazorEcommerce.Client.Stores.Interfaces;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace BlazorEcommerce.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();

            builder.Services.AddScoped<IProductStore, ProductStore>();
            builder.Services.AddScoped<ICategoryStore, CategoryStore>();
            builder.Services.AddScoped<ICartStore, CartStore>();
            builder.Services.AddScoped<IAuthStore, AuthStore>();
            builder.Services.AddScoped<IOrderStore, OrderStore>();
            builder.Services.AddScoped<IAddressStore, AddressStore>();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


            await builder.Build().RunAsync();
        }
    }
}