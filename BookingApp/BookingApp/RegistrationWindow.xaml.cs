using System;
using System.Collections.Generic;
using System.Data.Entity;
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
  /// Interaction logic for RegistrationWindow.xaml
  /// </summary>
  /// 

  public partial class RegistrationWindow : Window
  {

    public RegistrationWindow()
    {
      InitializeComponent();
      GenerateCaptcha();
      FilterComboBox.Items.Insert(0, "Муж.");
      FilterComboBox.Items.Insert(1, "Жен.");
      CalendarBirtdayPicker.DisplayDateEnd = new DateTime(2004, 12, 31);
      CalendarBirtdayPicker.DisplayDateStart = new DateTime(1930, 12, 31);
    }


    private void PasswordButton_Click(object sender, RoutedEventArgs e)
    {
      // Переброска необходимой информации во временные буферы
      String Password = PasswordPasswordBox.Password;
      Visibility Visibility = PasswordPasswordBox.Visibility;
      double Width = PasswordPasswordBox.ActualWidth;
      // Изменение подписи на кнопке
      PasswordButton.Content = Visibility == Visibility.Visible ? ". . ." : "Aaa";
      // Переброска информации из TextBox'а в PasswordBox
      PasswordPasswordBox.Password = PasswordTextBox.Text;
      PasswordPasswordBox.Visibility = PasswordTextBox.Visibility;
      PasswordPasswordBox.Width = PasswordTextBox.Width;
      // Возврат информации из временных буферов в TextBox
      PasswordTextBox.Text = Password;
      PasswordTextBox.Visibility = Visibility;
      PasswordTextBox.Width = Width;
    }

    void GenerateCaptcha()
    {
      CaptchaCheckTextBox.Text = "";
      string allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
      allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
      allowchar += "1,2,3,4,5,6,7,8,9,0";
      //разделитель
      char[] a = { ',' };
      //расщепление массива по разделителю
      String[] ar = allowchar.Split(a);
      String pwd = "";
      Random r = new Random();
      for (int i = 0; i < 6; i++)
      {
        string temp = ar[(r.Next(0, ar.Length))];
        pwd += temp;
      }
      CaptchaTextBox.Text = pwd;
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {

      if (!ValidationClass.CheckPassword(PasswordPasswordBox.Password) || !ValidationClass.CheckNumberPhone(NumberTextBox.Text))
      {
        MessageBox.Show("Проблемы с паролем или логином", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
      }
      else if (! ValidationClass.CheckLoginExist(LoginTextBox.Text))
      {
        MessageBox.Show("Такой логин уже существует", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
      }
      else
      {
        if ((CaptchaCheckTextBox.Text == CaptchaTextBox.Text) && (CaptchaTextBox.Text != ""))
        {
          StringBuilder errors = new StringBuilder();
          if (string.IsNullOrEmpty(FIOTextBox.Text) || FIOTextBox.Text.Length < 8)
            errors.AppendLine("Укажите ФИО!");

          if (string.IsNullOrEmpty(PassSeriaTextBox.Text) || PassSeriaTextBox.Text.Length > 4)
            errors.AppendLine("Укажите серию пасспорта!");

          if (string.IsNullOrEmpty(PassNumberTextBox.Text) || PassNumberTextBox.Text.Length > 6)
            errors.AppendLine("Укажите номер пасспорта!");

          if (string.IsNullOrEmpty(CalendarBirtdayPicker.Text))
            errors.AppendLine("Укажите дату рождения!");

          if (string.IsNullOrEmpty(NumberTextBox.Text) || NumberTextBox.Text.Length < 6)
            errors.AppendLine("Укажите ваш номер!");

          if (errors.Length > 0)
          {
            MessageBox.Show(errors.ToString());
            return;
          }
          Base.CLIENTS User = new Base.CLIENTS();

          User.LOGIN = LoginTextBox.Text;

          User.PASSWORD = PasswordPasswordBox.Password != "" ? PasswordPasswordBox.Password : PasswordTextBox.Text;
          User.ROLE_ID = false;
          User.FIO = FIOTextBox.Text;
          User.PASSPORT_SERIA = int.Parse(PassSeriaTextBox.Text);
          User.PASSPORT_NUMBER = int.Parse(PassNumberTextBox.Text);
          User.NUMBER = NumberTextBox.Text;
          User.DATE_BIRTHDAY = CalendarBirtdayPicker.SelectedDate;
          if ((String)FilterComboBox.SelectedItem == "Муж.")
          {
            User.MALE = true;
          }
          else
          {
            User.MALE = false;
          }
          User.FriendlyName = "";


          // Добавление его в базу данных
          SourceCore.MyBase.CLIENTS.Add(User);
          // Сохранение изменений
          //MessageBox.Show("Ваш логин: " + LoginTextBox.Text + " Ваш пароль: " + PasswordPasswordBox.Password, "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
          SourceCore.MyBase.SaveChanges();
          SelectedWindow.ChangeWindow("MainWindow", this);
          Close();

        }
        else
        {
          MessageBox.Show("Неверно указана CAPTCHa!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
          GenerateCaptcha();
        }
      }

    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      SelectedWindow.ChangeWindow("MainWindow", this);
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

    private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
    }
  }
}
