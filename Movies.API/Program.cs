using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.API.Services;
using MovieScout.DbContexts;
using MovieScout.Services;
using Refit;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieContext>(dbContextOptions => dbContextOptions.UseSqlite("Data Source = MovieFavourites.Db"));
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Authentication:Issuer"],
                ValidAudience = builder.Configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
            };
        }
    );

builder.Services.AddTransient<ApiKeyHandler>();
builder.Services.AddRefitClient<IMovieDataService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.themoviedb.org/3"))
    .AddHttpMessageHandler<ApiKeyHandler>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7091")
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});
builder.Services.AddScoped<IMovieInfoRepository, MovieInfoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors("_myAllowSpecificOrigins");

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

class ApiKeyHandler : DelegatingHandler
{
    private readonly string _apiKey;
    public ApiKeyHandler(IConfiguration configuration)
    {
        _apiKey = configuration["ApiKey"];
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var apiKeyString = $"api_key={_apiKey}";
        var uri = new UriBuilder(request.RequestUri);
        var query = string.Empty;
        if (request.RequestUri.Query.Contains('?'))
        {
            query = request.RequestUri.Query + "&";
        }
        else
        {
            query = "?";
        }

        query += apiKeyString;

        uri.Query = query;
        request.RequestUri = uri.Uri;
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}