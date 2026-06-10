using System.Net.Http.Json;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5000")
        };
    }

    public async Task CreatePlayerAsync(string externalId, string partnerId)
    {
        var request = new RequestDTO
        {
            ExternalId = externalId,
            PartnerId = partnerId
        };

        var response = await _httpClient.PostAsJsonAsync(
            "/Funds/CreatePlayer",
            request);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Player Created:");
        Console.WriteLine(content);
    }

    public async Task GetBalanceAsync(string externalId)
    {
        var response = await _httpClient.GetAsync(
            $"/Funds/GetBalance?ExternalId={externalId}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            return;
        }

        var balance =
            await response.Content.ReadFromJsonAsync<BalanceResponse>();

        Console.WriteLine($"Balance: {balance?.Balance}");
    }
}