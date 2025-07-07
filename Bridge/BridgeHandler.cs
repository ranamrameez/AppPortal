using Microsoft.Web.WebView2.Core;
using System.Text.Json;

namespace AppPortal.Bridge
{
    public static class BridgeHandler
    {
        public static void OnMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var json = JsonDocument.Parse(e.WebMessageAsJson);
            string action = json.RootElement.GetProperty("action").GetString() ?? "";

            switch (action)
            {
                case "saveConfig":
                    var payload = json.RootElement.GetProperty("payload");
                    string title = payload.GetProperty("title").GetString() ?? "";
                    string url = payload.GetProperty("url").GetString() ?? "";

                    var cfg = new AppConfig { Title = title, Url = url };
                    AppConfig.Save(cfg);
                    break;

                default:
                    break;
            }
        }
    }
}