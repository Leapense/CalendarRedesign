using Calendar_Redesign.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Calendar_Redesign.Views;

// To learn more about WebView2, see https://docs.microsoft.com/microsoft-edge/webview2/
public sealed partial class Email_ChatPage : Page
{
    public Email_ChatViewModel ViewModel
    {
        get;
    }

    public Email_ChatPage()
    {
        ViewModel = App.GetService<Email_ChatViewModel>();
        InitializeComponent();

        ViewModel.WebViewService.Initialize(WebView);
    }
}
