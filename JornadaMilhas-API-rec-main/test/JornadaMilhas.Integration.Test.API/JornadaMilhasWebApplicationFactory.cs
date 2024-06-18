using JornadaMilhas.API.DTO.Auth;
using JornadaMilhas.Dados;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace JornadaMilhas.Integration.Test.API;

public class JornadaMilhasWebApplicationFactory : WebApplicationFactory<Program>
{
    public JornadaMilhasContext Context { get; }

    private IServiceScope _scope;

    public JornadaMilhasWebApplicationFactory()
    {
        _scope = Services.CreateScope();
        Context = _scope.ServiceProvider.GetRequiredService<JornadaMilhasContext>();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<JornadaMilhasContext>));

            services.AddDbContext<JornadaMilhasContext>(options =>
            {
                options.UseLazyLoadingProxies()
                    .UseSqlServer("Server=localhost,11433;Database=JornadaMilhasV3;User Id=sa;Password=Alura#2024;Encrypt=false;TrustServerCertificate=true;MultipleActiveResultSets=true;");
            });
        });

        base.ConfigureWebHost(builder);
    }

    public async Task<HttpClient> GetClientWithAccessTokenAsync()
    {
        var client = this.CreateClient();

        var user = new UserDTO
        {
            Email = "tester@email.com",
            Password = "Senha123@"
        };

        var response = await client.PostAsJsonAsync("/auth-login", user);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<UserTokenDTO>();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result!.Token);

        return client;
    }
}
