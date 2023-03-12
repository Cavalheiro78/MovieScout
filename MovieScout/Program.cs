using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MovieScout;
using MovieScout.Services;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddRefitClient<IMovieDataService>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.themoviedb.org/3"));

await builder.Build().RunAsync();

