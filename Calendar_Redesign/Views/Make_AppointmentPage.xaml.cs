using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Calendar_Redesign.ViewModels;

using Microsoft.UI.Xaml.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Sockets;
using System.Net;
using MySqlX.XDevAPI.Common;

namespace Calendar_Redesign.Views;

public sealed partial class Make_AppointmentPage : Page
{
    
    public Make_AppointmentViewModel ViewModel
    {
        get;
    }

    public Make_AppointmentPage()
    {
        ViewModel = App.GetService<Make_AppointmentViewModel>();
        InitializeComponent();
    }
    
    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        try
        {
            var cs = @"Server=192.168.1.6,1433;uid=sa;pwd=1234;database=dbo;";
            using var conn = new SqlConnection(cs);
            conn.Open();
            var sql = "insert into alarm (yourname, dates, place, times, info) values (@yourname, @dates, @place, @times, @info);";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@yourname", YourName.Text);
            cmd.Parameters.AddWithValue("@dates", Dates.SelectedDate.ToString().Substring(0, 10));
            cmd.Parameters.AddWithValue("@place", Places.Text);
            cmd.Parameters.AddWithValue("@times", Times.SelectedTime.ToString());
            cmd.Parameters.AddWithValue("@info", Description.Text);
            cmd.ExecuteNonQuery();
            conn.Close();

        } catch(Exception ex) { Description.Text = ex.Message; }
    }

    private void Button_Click_1(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AppointmentList.Items.Clear();
        
        try
        {
            var cs = @"Server=192.168.1.6,1433;uid=sa;pwd=1234;database=dbo;";
            using var conn = new SqlConnection(cs);
            conn.Open();
            var sql = "select * from alarm where yourname = N'" + YourName.Text + "';";
            using var cmd = new SqlCommand(sql, conn);
            using SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                AppointmentList.Items.Add(rdr.GetString(1) + "         "
                                        + rdr.GetString(2) + "         "
                                        + rdr.GetString(3) + "         "
                                        + rdr.GetString(4));
            }
            conn.Close();

        }
        catch (Exception ex) { Description.Text = ex.Message; }
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
}
