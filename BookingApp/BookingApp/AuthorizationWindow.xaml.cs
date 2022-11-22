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
  /// Interaction logic for AuthorizationWindow.xaml
  /// </summary>
  ///
  public partial class AuthorizationWindow : Window
  {
    //private Base.BOOKING_AVIA_ZHDEntities1 DataBase;
    public AuthorizationWindow()
    {
      InitializeComponent();
    }
    private void AuthorizationCommit_Click(object sender, RoutedEventArgs e)
    {
      Base.CLIENTS User = SourceCore.MyBase.CLIENTS.SingleOrDefault(U => U.LOGIN == LoginText.Text && U.PASSWORD == PasswordText.Text);
      if (User != null)
      {
        MainWindow.Client = User;
        SelectedWindow.ChangeWindow("MainWindow", this);

      }
      else
      {
        MessageBox.Show("Неверно указан логин и/или пароль!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
      };
    }

    private void AuthorizationRollBack_Click(object sender, RoutedEventArgs e)
    {
      SelectedWindow.ChangeWindow("MainWindow", this);
      Close();
    }

    private void RegistrationButton_Click(object sender, RoutedEventArgs e)
    {
      SelectedWindow.ChangeWindow("RegistrationWindow", this);
    }
  }
}
