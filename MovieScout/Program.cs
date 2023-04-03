using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MovieScout;
using MovieScout.Services;
using MovieScoutShared;
using Refit;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services);

var app = builder.Build();


await app.RunAsync();

void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<UserInfoGlobalClass>();
    services.AddSingleton<AppState>();
    //services.AddTransient<ApiKeyHandler>();
    services.AddRefitClient<IMovieDataService>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7178/api"));
        //.AddHttpMessageHandler<ApiKeyHandler>();
}
/*
class ApiKeyHandler : DelegatingHandler
{
    private readonly string _apiKey;
    public ApiKeyHandler(IConfiguration configuration)
    {
        _apiKey = configuration["ApiKey"];
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}*/