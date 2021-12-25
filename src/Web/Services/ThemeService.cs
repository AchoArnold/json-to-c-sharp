using System.Web;
using Microsoft.AspNetCore.Components;

namespace Web.Services;

public class ThemeService
{
    private const string DefaultTheme = "default";
    private const string QueryParameter = "theme";

    public static readonly Theme[] Themes = {
        new() { Text = "Light Theme", Value = "default"},
        new() { Text = "Dark Theme", Value="dark" },
    };

    public string CurrentTheme { get; set; } = DefaultTheme;

    public bool IsDarkTheme()
    {
        return CurrentTheme == "dark";
    }
    
    public void Initialize(NavigationManager navigationManager)
    {
        var uri = new Uri(navigationManager.ToAbsoluteUri(navigationManager.Uri).ToString());
        var query = HttpUtility.ParseQueryString(uri.Query);
        var value = query.Get(QueryParameter);

        if (Themes.Any(theme => theme.Value == value) && value != null)
        {
            CurrentTheme = value;
        }
    }

    public void Change(NavigationManager navigationManager, string theme)
    {
        var url = navigationManager.GetUriWithQueryParameter(QueryParameter, theme);

        navigationManager.NavigateTo(url, true);
    }
    
    public class Theme
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Text { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}