using CommomTestUtilities.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebAPI.Test.User.Register;

//Interface para teste de inetgração <PacoteNuGet<Classe criada no program.cs>>
public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    public RegisterUserTest(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var response = await _httpClient.PostAsJsonAsync("User", request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        //resposta do request é passado para stream e nao string por boas praticas
        await using var reponseBody = await response.Content.ReadAsStreamAsync();
        //ParseAsync converto um fluxo de dados(stream) em JsonDocument
        var responseData = await JsonDocument.ParseAsync(reponseBody);
        //Json sem pre as propriedades em minusculo
        responseData.RootElement.GetProperty("name").GetString().ShouldSatisfyAllConditions(
            e => e.ShouldNotBeNullOrWhiteSpace(),
            e => e.ShouldBe(request.Name));
    }
}