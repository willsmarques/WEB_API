using FluentAssertions;
using MeuLivroDeReceitas.API;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Xunit;

namespace WebApi.Test;

public class ControllerBase : IClassFixture<MeuLivroReceitaWeApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ControllerBase(MeuLivroReceitaWeApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        ResourceMensagensDeErro.Culture = CultureInfo.CurrentCulture;
    }

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body)
    {
        var jsonString = JsonConvert.SerializeObject(body);

        return await _client.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }

    protected async Task<HttpResponseMessage> PutRequest(string metodo, object body,string token = "")
    {
        AutorizarRequisicao(token);
        var jsonString = JsonConvert.SerializeObject(body);

        return await _client.PutAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }

    protected async Task<string> Login(string email, string senha)
    {
        var requisicao = new MeuLivroDeReceitas.Comunicacao.Requisicoes.RequisicaoLoginJson
        {
            Email = email,
            Senha = senha
        };

        var resposta = await PostRequest("login", requisicao);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaData = await JsonDocument.ParseAsync(responstaBody);

        return respostaData.RootElement.GetProperty("token").GetString();
    }

    private void AutorizarRequisicao(string token)
    {
        if (!string.IsNullOrWhiteSpace(token))
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }


    }



}
