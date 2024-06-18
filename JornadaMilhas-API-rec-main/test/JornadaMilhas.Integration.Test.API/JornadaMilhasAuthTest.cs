using JornadaMilhas.API.DTO.Auth;
using System.Net;
using System.Net.Http.Json;

namespace JornadaMilhas.Integration.Test.API;

public class JornadaMilhasAuthTest
{
    [Fact]
    public async Task Post_Efetua_Login_Com_Sucesso()
    {
        //Arrange
        var app = new JornadaMilhasWebApplicationFactory();
        using var client = app.CreateClient();
        var user = new UserDTO
        {
            Email = "tester@email.com",
            Password = "Senha123@"
        };

        //Act
        var resultado = await client.PostAsJsonAsync("/auth-login", user);

        //Assert
        Assert.Equal(HttpStatusCode.OK, resultado.StatusCode);
    }
}