using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Calendar_Redesign.ViewModels;

using Microsoft.UI.Xaml.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Sockets;
using System.Net;
using MySqlX.XDevAPI.Common;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Runtime.InteropServices;
using System.IO;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace Calendar_Redesign.Views;

public sealed partial class Make_AppointmentPage : Page
{
    
    public Make_AppointmentViewModel ViewModel
    {
        get;
    }
	static List<string> appointment_list = new List<string>();
    public Make_AppointmentPage()
    {
        ViewModel = App.GetService<Make_AppointmentViewModel>();
        InitializeComponent();
    }
    
    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        appointment_list.Add(Dates.SelectedDate.ToString().Substring(0,10) + "\t" + Times.SelectedTime.ToString() + "\t" + Places.Text + "\t" + Description.Text);
    }

    private void Button_Click_1(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
		AppointmentList.Items.Clear();
        foreach(var appointments in appointment_list)
        {
			AppointmentList.Items.Add(appointments);
		}
			
    }

    private void SwipeItem_Invoked(SwipeItem sender, SwipeItemInvokedEventArgs args)
    {

    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ToolTip tool = new ToolTip();
        
        if (Lang.SelectedIndex == 0)
        {
            L1.Text = "언어";
            Name.Text = "이름";
            T1.Text = "약속 일정";
            T2.Text = "약속 장소";
            // "기억이 안 나신다면 물어보거나 수첩을 확인해주세요."
            T3.Text = "약속 시간";
            T4.Text = "약속 내용";
            B1.Content = "약속 추가하기";
        }
        else if (Lang.SelectedIndex == 1)
        {
            L1.Text = "Language";
            Name.Text = "Name";
            T1.Text = "Appointment Date";
            // If you don't remember, ask or check your memo.
            T2.Text = "Appointment Place";
            T3.Text = "Appointment Time";
            T4.Text = "Appointment Info";
            B1.Content = "Add Appointment";
        }
        else if (Lang.SelectedIndex == 2)
        {
            L1.Text = "Sprache";
            Name.Text = "Name";
            T1.Text = "Datum der Ernennung";
            // Wenn Sie sich nicht erinnern können, fragen Sie nach oder sehen Sie in Ihrem Memo nach.
            T2.Text = "Ort der Verabredung";
            T3.Text = "Zeit der Verabredung";
            T4.Text = "Informationen zur Ernennung";
            B1.Content = "Termin hinzufügen";
        }
    }

    private async void Button_Click_2(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Windows.Storage.StorageFolder storageFolder =
            Windows.Storage.ApplicationData.Current.LocalFolder;
        Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync(Tagus.Text + ".txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
        List<string> appointments = new List<string>();

        //sampleFile = await storageFolder.GetFileAsync(Tagus.Text + ".txt");
        
        for (int i = 0; i < AppointmentList.Items.Count; i++)
        {
            appointments.Add(AppointmentList.Items[i].ToString());
        }
        await FileIO.WriteLinesAsync(sampleFile, appointments);


    }

    private void Button_Click_3(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AppointmentList.Items.Remove(AppointmentList.SelectedItem);
    }

    private async void Button_Click_4(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AppointmentList.Items.Clear();
        Windows.Storage.StorageFolder storageFolder =
            Windows.Storage.ApplicationData.Current.LocalFolder;
        Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync(Tagus.Text + ".txt");
        string appointments = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
        string[] strings = appointments.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < strings.Length; i++)
        {
            AppointmentList.Items.Add(strings[i]);
        }
    }
    static List<string> ml_list = new List<string>();
    private async void AppBarButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var filePicker = new FileOpenPicker();
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
        WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

        filePicker.FileTypeFilter.Add("*");
        var file = await filePicker.PickSingleFileAsync();

        // play the music file
        if (file != null)
        {
            MusicList.Items.Add(file.Name);
            ml_list.Add(file.Path);
        }

    }
    [ComImport]
    [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInitializeWithWindow
    {
        void Initialize(IntPtr hwnd);
    }
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB")]
    internal interface IWindowNative
    {
        IntPtr WindowHandle
        {
            get;
        }
    }
    public MediaPlayer mediaPlayer = new MediaPlayer();
    private void AppBarButton_Click_1(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        string selected_name = MusicList.SelectedValue.ToString();
        // Get full path of selected_name
        foreach (string te in ml_list)
        {
            if (te.EndsWith(selected_name))
            {
                mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(te));
                break;
            }
        }
        SongName.Text = selected_name;
        
        mediaPlayer.Play();
    }

    private void AppBarButton_Click_2(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (mediaPlayer.CurrentState == MediaPlayerState.Playing)
        {
            mediaPlayer.Pause();
        }
        else
        {
            mediaPlayer.Play();
        }
    }

    private void AppBarButton_Click_3(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        mediaPlayer.Pause();
        mediaPlayer.Position = TimeSpan.Zero;
    }
    
    private void AppBarButton_Click_4(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        
    }

    private void AppBarButton_Click_5(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

    }

    private void AppBarButton_Click_6(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        mediaPlayer.PlaybackRate = mediaPlayer.PlaybackRate - 0.1;
        if (mediaPlayer.PlaybackRate == 0.5) mediaPlayer.PlaybackRate = 0.5;
    }

    private void AppBarButton_Click_7(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        mediaPlayer.PlaybackRate = mediaPlayer.PlaybackRate + 0.1;
        if (mediaPlayer.PlaybackRate == 2.0) mediaPlayer.PlaybackRate = 2.0;
    }
}
