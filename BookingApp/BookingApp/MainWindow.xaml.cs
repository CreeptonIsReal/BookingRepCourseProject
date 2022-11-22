using BookingApp.MainPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public static Base.CLIENTS Client = null;
    public static string WhereOn;
    public static string WhereOf;
    public static DateTime DataOn;
    public static double CountPass = 1;
    //string WhereOf = WhereOf_Text.Text;


    public MainWindow()
    {
      InitializeComponent();
      if (Client != null)
      {
        ShowUserStackPanel();
      }
    }

    private void ShowUserStackPanel()
    {
      UserStackPanel.Visibility = Visibility.Visible;
      AutorizOrRegistrStackPanel.Visibility = Visibility.Collapsed;
      UserNameLabel.Content = "Добро пожаловать, " + Client.LOGIN;
      if (Client.ROLE_ID == true)
      {
        AdminPanelButton.Visibility = Visibility.Visible;
      }
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
      SelectedWindow.ChangeWindow("AuthorizationWindow", this);
    }

    private void Sign_Click(object sender, RoutedEventArgs e)
    {
      SelectedWindow.ChangeWindow("RegistrationWindow", this);
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
      Client = null;
      UserStackPanel.Visibility = Visibility.Collapsed;
      AutorizOrRegistrStackPanel.Visibility = Visibility.Visible;
    }

    private void AdminPanel_Click(object sender, RoutedEventArgs e)
    {
      SelectedWindow.ChangeWindow("AdminWindow", this);
    }

    private void SearchTicketButton_Click(object sender, RoutedEventArgs e)
    {
      StringBuilder errors = new StringBuilder();
      if (string.IsNullOrEmpty(WhereOn_Text.Text))
        errors.AppendLine("Укажите откуда хотите уехать");

      if (string.IsNullOrEmpty(WhereOf_Text.Text))
        errors.AppendLine("Укажите куда хотите приехать");

      if (string.IsNullOrEmpty(Passager_Text.Text))
        errors.AppendLine("Укажите количество пассажиров");

      if (errors.Length > 0)
      {
        MessageBox.Show(errors.ToString());
        return;
      }
      WhereOn = WhereOn_Text.Text;
      WhereOf = WhereOf_Text.Text;
      RootFrame.Navigate(new RoutePage());
    }


    private void Passager_Text_LostFocus(object sender, RoutedEventArgs e)
    {
      if (Passager_Text.Text == "")
      {
        Passager_Text.Text = "1";
      }
      CountPass = Convert.ToDouble(Passager_Text.Text);
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      string originalText = ((TextBox)sender).Text.Trim();
      if (originalText.Length > 0)
      {
        if (System.Text.RegularExpressions.Regex.IsMatch(originalText, "[^0-9]"))
        {
          originalText = originalText.Remove(originalText.Length - 1);
          ((TextBox)sender).Text = originalText;
        }
      }
      originalText = originalText.Replace(" ", string.Empty);
      ((TextBox)sender).Text = originalText;
    }

    private void WhereOn_Text_TextChanged(object sender, TextChangedEventArgs e)
    {
      WhereOn_Text.Text = WhereOn_Text.Text.Replace(" ", string.Empty);
      WhereOn_Text.SelectionStart = WhereOn_Text.Text.Length; // возврат каретки в конец текста
    }

    private void WhereOf_Text_TextChanged(object sender, TextChangedEventArgs e)
    {
      WhereOf_Text.Text = WhereOf_Text.Text.Replace(" ", string.Empty);
      WhereOf_Text.SelectionStart = WhereOf_Text.Text.Length; // возврат каретки в конец текста
    }
  }
}
