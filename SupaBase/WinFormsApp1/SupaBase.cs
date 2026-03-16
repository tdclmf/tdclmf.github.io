using System.Threading.Tasks;

namespace SupabaseWinForms
{
    public static class SupabaseManager
    {
        public static Supabase.Client Client { get; private set; }

        public static async Task InitializeAsync()
        {
            if (Client == null)
            {
                var url = "https://ndopvebkbedvftrmlggm.supabase.co";
                var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im5kb3B2ZWJrYmVkdmZ0cm1sZ2dtIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzMzNzkxNzQsImV4cCI6MjA4ODk1NTE3NH0.ZhvpYir7FAOImslC9oxb9feFWFFFRX-ekhciz1w3ZOo";

                var options = new Supabase.SupabaseOptions { AutoConnectRealtime = true };
                Client = new Supabase.Client(url, key, options);
                await Client.InitializeAsync();
            }
        }
    }
}