using System.Text;
using System.Text.Json;

namespace PizzaApp.Services
{
    public static class AuthService
    {
        private const string SupabaseUrl = "https://pukvchhvxwhjnomhcrth.supabase.co";
        private const string SupabaseAnonKey = "sb_publishable_GUboxTku7sewBK09wqbDag_wJNtWAYG";

        private static readonly HttpClient _client = new HttpClient();

        public static async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var url = $"{SupabaseUrl}/auth/v1/token?grant_type=password";

                var payload = new { email, password };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("apikey", SupabaseAnonKey);
                request.Content = content;

                var response = await _client.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
