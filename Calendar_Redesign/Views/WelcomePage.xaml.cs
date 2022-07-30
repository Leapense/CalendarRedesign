using System;
using Calendar_Redesign.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Calendar_Redesign.Views;

public sealed partial class WelcomePage : Page
{
    public WelcomeViewModel ViewModel
    {
        get;
    }

    public WelcomePage()
    {
        ViewModel = App.GetService<WelcomeViewModel>();
        InitializeComponent();
        Loaded += WelcomePage_Loaded;
    }

    private void WelcomePage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var get_first_username = Environment.UserName;
        var get_initial_of_first_username = get_first_username.Substring(0, 1).ToUpper();
        PersonPicture.Initials = get_initial_of_first_username;
        UN.Text = get_first_username;
        
    }
}
