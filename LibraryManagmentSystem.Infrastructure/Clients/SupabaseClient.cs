using LibraryManagmentSystem.Application.IClients;
using Supabase;

namespace LibraryManagmentSystem.Infrastructure.Clients
{
    public class SupabaseClient : ISupabaseClient
    {
        private Client _supabaseClient;
        private bool _initialized = false;

        public Client Client => _supabaseClient;

        public async Task Initialize()
        {
            if (_initialized) return;
            string supabaseUrl = "https://vnqjztmsjqvgalzsmkyg.supabase.co";
            string supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZucWp6dG1zanF2Z2FsenNta3lnIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjE3NTA5NTIsImV4cCI6MjA3NzMyNjk1Mn0.5cpApUljiNTtPLCTY6kc_NJNdOw5UOFYGdzn4SjqTbQ";

            _supabaseClient = new Client(supabaseUrl, supabaseKey);

            await _supabaseClient.InitializeAsync();
            _initialized = true;

        }

        public Supabase.Client GetClient() => _supabaseClient;
        public async Task<string> UploadFileAsync(string bucketName, string fileName, Stream fileStream)
        {
            if (!_initialized)
                await Initialize();
            var bucket = _supabaseClient.Storage.From(bucketName);
            var bytes = await ReadFullyAsync(fileStream);

            await bucket.Upload(bytes, fileName);
            return bucket.GetPublicUrl(fileName); // هذا الرابط تقدر تستخدمه للعرض
        }

        private async Task<byte[]> ReadFullyAsync(Stream input)
        {
            using var ms = new MemoryStream();
            await input.CopyToAsync(ms);
            return ms.ToArray();
        }
    }

}
