using Microsoft.Web.WebView2.Core;
using System.IO;
using System.Text.Json;

namespace AppPortal.Bridge
{
    public class AppConfig
    {
        public string Title { get; set; } = "";
        public string Url { get; set; } = "";

        public static string ConfigPath => Path.Combine(AppContext.BaseDirectory, "config.json");

        public static void Save(AppConfig config)
        {
            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json);
        }

        public static AppConfig Load()
        {
            if (File.Exists(ConfigPath))
            {
                string json = File.ReadAllText(ConfigPath);
                return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
            }
            return new AppConfig();
        }
    }
}