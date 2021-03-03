using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Website
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri("http://localhost:5000/")});

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("api://0bc1357a-e507-44f0-9d96-94dda6ef48ec/Api");
            });

            builder.Services.AddApiAuthorization()
                .AddAccountClaimsPrincipalFactory<CustomUserFactory>();
            
            await builder.Build().RunAsync();
        }
    }
}
