using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        string transactionHash = "853793d552635f533aa982b92b35b00e63a1c1add062c099da2450a15119bcb2";
        string riskData = await GetTronTransactionRisk(transactionHash);

        if (riskData != null)
        {
            Console.WriteLine("Данные риска транзакции:");
            Console.WriteLine(riskData);
        }
        else
        {
            Console.WriteLine("Не удалось получить данные риска.");
        }
    }

    static async Task<string> GetTronTransactionRisk(string transactionHash)
    {
        HttpClient client = new HttpClient();

        string url = $"https://apilist.tronscan.org/api/transaction-info?hash={transactionHash}";
        HttpResponseMessage response = await client.GetAsync(url);
        try
        {
            string responseData = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseData);
            JToken riskData = json.SelectToken("riskTransaction");

            return riskData.ToString();
        }
        catch
        {
            Console.WriteLine($"Ошибка при запросе к API: {response.StatusCode}");
            return null;
        }
    }
}
