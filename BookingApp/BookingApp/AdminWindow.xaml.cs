using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace BookingApp
{
  /// <summary>
  /// Interaction logic for AdminWindow.xaml
  /// </summary>
  public partial class AdminWindow : Window
  {
    public AdminWindow()
    {
      InitializeComponent();
    }

    private void RouteButton_Click(object sender, RoutedEventArgs e)
    {
      RootFrame.Navigate(new AdminPages.RoutesPage1());
    }

    private void CompanyButton_Click(object sender, RoutedEventArgs e)
    {
      RootFrame.Navigate(new AdminPages.TravekCompanyPage());
    }

    private void BookingButton_Click(object sender, RoutedEventArgs e)
    {
      RootFrame.Navigate(new AdminPages.BookingsPage1());
    }

    private void TransportButton_Click(object sender, RoutedEventArgs e)
    {
      RootFrame.Navigate(new AdminPages.TransportPage());
    }

    private void ClientButton_Click(object sender, RoutedEventArgs e)
    {
      RootFrame.Navigate(new AdminPages.UsersPage());
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
      SelectedWindow.ChangeWindow("MainWindow", this);
    }
  }
}
