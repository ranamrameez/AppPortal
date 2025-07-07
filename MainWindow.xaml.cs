using Microsoft.Web.WebView2.Core;
using System.IO;
using System.Windows;

namespace AppPortal;
public partial class MainWindow : Window
{
    private string _homeUrl = "https://intranet/portal";
    private string _fallbackUrl;

    public MainWindow()
    {
        InitializeComponent();

        _fallbackUrl = $"file:///{Path.Combine(AppContext.BaseDirectory, "WebUI", "index.html").Replace("\\", "/")}";

        Loaded += Window_Loaded;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await WebView.EnsureCoreWebView2Async();

        WebView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;

        NavigateTo(_homeUrl);
    }

    private void CoreWebView2_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        if (!e.IsSuccess)
        {
            NavigateTo(_fallbackUrl);
        }
        else
        {
            UrlBar.Text = WebView.Source.ToString();
        }
    }

    private void NavigateTo(string url)
    {
        UrlBar.Text = url;
        WebView.CoreWebView2.Navigate(url);
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        if (WebView.CoreWebView2.CanGoBack)
            WebView.CoreWebView2.GoBack();
    }

    private void Forward_Click(object sender, RoutedEventArgs e)
    {
        if (WebView.CoreWebView2.CanGoForward)
            WebView.CoreWebView2.GoForward();
    }

    private void Reload_Click(object sender, RoutedEventArgs e)
    {
        WebView.CoreWebView2.Reload();
    }

    private void Home_Click(object sender, RoutedEventArgs e)
    {
        NavigateTo(_homeUrl);
    }

    private void ToggleUrl_Click(object sender, RoutedEventArgs e)
    {
        UrlBar.Visibility = UrlBar.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }
}

