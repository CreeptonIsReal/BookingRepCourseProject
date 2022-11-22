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

namespace BookingApp.MainPages
{
  /// <summary>
  /// Interaction logic for PayWindow.xaml
  /// </summary>
  public partial class PayWindow : Window
  {
    public static bool PayValid;
    public PayWindow()
    {
      InitializeComponent();
      PayLabel.Content = "Оплата - " + RoutePage.MoneyOf + " руб.";
    }

    private void AuthorizationCommit_Click(object sender, RoutedEventArgs e)
    {
      StringBuilder errors = new StringBuilder();
      if (string.IsNullOrEmpty(NameCardText.Text))
        errors.AppendLine("Укажите имя");

      if (string.IsNullOrEmpty(NumCardText.Text))
        errors.AppendLine("Укажите номер карты");

      if (string.IsNullOrEmpty(CardDateText.Text))
        errors.AppendLine("Укажите дату");

      if (string.IsNullOrEmpty(CardCVCText.Text))
        errors.AppendLine("Укажите CVC код");

      if (errors.Length > 0)
      {
        MessageBox.Show(errors.ToString());
        return;
      }
      MessageBox.Show("Оплата была произведена, место забронированно!", "Предупреждение", MessageBoxButton.OK);
      PayValid = true;
      Close();
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
    }

    private void AboutButton_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Не забудьте оплатить! Место забранированно.", "Предупреждение", MessageBoxButton.OK);
      PayValid = false;
      Close();
    }
  }
}
