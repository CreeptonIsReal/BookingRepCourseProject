using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.AdminPages
{
  /// <summary>
  /// Логика взаимодействия для UsersPage.xaml
  /// </summary>
  public partial class UsersPage : Page
  {
    public UsersPage()
    {
      InitializeComponent();
      DataContext = this;
      UpdateGrid(null);
      DlgLoad(false, "");
      TypeComboBoxMale.Items.Insert(0, "Женский");
      TypeComboBoxMale.Items.Insert(1, "Мужской");

      TypeComboBoxRole.Items.Insert(0, "Пользователь");
      TypeComboBoxRole.Items.Insert(1, "Администратор");

      CalendarBirtdayPicker.DisplayDateEnd = new DateTime(2004, 12, 31);
      CalendarBirtdayPicker.DisplayDateStart = new DateTime(1930, 12, 31);
    }
    private int DlgMode = 0;
    public Base.CLIENTS SelectedItem;
    public ObservableCollection<Base.CLIENTS> CLIENTSS;

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      List<string> Columns = new List<string>();
      for (int i = 0; i < 9; i++)
      {
        Columns.Add(PageGrid.Columns[i].Header.ToString());
      }
      FilterComboBox.ItemsSource = Columns;
      FilterComboBox.SelectedIndex = 0;

      foreach (DataGridColumn Column in PageGrid.Columns)
      {
        Column.CanUserSort = false;
      }
    }
    public void UpdateGrid(Base.CLIENTS CLIENTS)
    {
      if ((CLIENTS == null) && (PageGrid.ItemsSource != null))
      {
        CLIENTS = (Base.CLIENTS)PageGrid.SelectedItem;
      }
      CLIENTSS = new ObservableCollection<Base.CLIENTS>(SourceCore.MyBase.CLIENTS);
      PageGrid.ItemsSource = CLIENTSS;
      PageGrid.SelectedItem = CLIENTS;
    }
    public void DlgLoad(bool b, string DlgModeContent)
    {
      if (b == true)
      {
        ColumnChange.Width = new GridLength(300);
        PageGrid.IsHitTestVisible = false;
        RecordLabel.Content = DlgModeContent + " запись";
        AddCommit.Content = DlgModeContent;
        RecordAdd.IsEnabled = false;
        RecordCopy.IsEnabled = false;
        RecordEdit.IsEnabled = false;
        RecordDellete.IsEnabled = false;
      }
      else
      {
        ColumnChange.Width = new GridLength(0);
        PageGrid.IsHitTestVisible = true;
        RecordAdd.IsEnabled = true;
        RecordCopy.IsEnabled = true;
        RecordEdit.IsEnabled = true;
        RecordDellete.IsEnabled = true;
        DlgMode = -1;
      }
    }
    private void RecordAdd_Click(object sender, RoutedEventArgs e)
    {
      DlgLoad(true, "Добавить");
      DataContext = null;
      DlgMode = 0;
    }

    private void RecordkCopy_Click(object sender, RoutedEventArgs e)
    {
      if (PageGrid.SelectedItem != null)
      {
        DlgLoad(true, "Копировать");
        SelectedItem = (Base.CLIENTS)PageGrid.SelectedItem;
        RecordTextFIO.Text = SelectedItem.FIO;
        RecordTextPassSeria.Text = SelectedItem.PASSPORT_SERIA.ToString(); ;
        RecordTextPassNum.Text = SelectedItem.PASSPORT_NUMBER.ToString(); ;
        RecordTextPhoneNum.Text = SelectedItem.NUMBER.ToString(); ;
        CalendarBirtdayPicker.Text = SelectedItem.DATE_BIRTHDAY.ToString();
        RecordTextLogin.Text = SelectedItem.LOGIN;
        RecordTextPassword.Text = SelectedItem.PASSWORD;

        DlgMode = 0;
      }
      else
      {
        MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
      }
    }
    private void RecordEdit_Click(object sender, RoutedEventArgs e)
    {
      if (PageGrid.SelectedItem != null)
      {
        DlgLoad(true, "Изменить");
        SelectedItem = (Base.CLIENTS)PageGrid.SelectedItem;
        RecordTextFIO.Text = SelectedItem.FIO;
        RecordTextPassSeria.Text = SelectedItem.PASSPORT_SERIA.ToString(); ;
        RecordTextPassNum.Text = SelectedItem.PASSPORT_NUMBER.ToString(); ;
        RecordTextPhoneNum.Text = SelectedItem.NUMBER.ToString(); ;
        CalendarBirtdayPicker.Text = SelectedItem.DATE_BIRTHDAY.ToString();
        RecordTextLogin.Text = SelectedItem.LOGIN;
        RecordTextPassword.Text = SelectedItem.PASSWORD;
      }
      else
      {
        MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
      }
    }

    private void RecordDelete_Click(object sender, RoutedEventArgs e)
    {
      if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
      {

        try
        {
          // Ссылка на удаляемую книгу
          Base.CLIENTS DeletingAccessory = (Base.CLIENTS)PageGrid.SelectedItem;
          // Определение ссылки, на которую должен перейти указатель после удаления
          if (PageGrid.SelectedIndex < PageGrid.Items.Count - 1)
          {
            PageGrid.SelectedIndex++;
          }
          else
          {
            if (PageGrid.SelectedIndex > 0)
            {
              PageGrid.SelectedIndex--;
            }
          }
          Base.CLIENTS SelectingAccessory = (Base.CLIENTS)PageGrid.SelectedItem;
          SourceCore.MyBase.CLIENTS.Remove(DeletingAccessory);
          SourceCore.MyBase.SaveChanges();
          UpdateGrid(SelectingAccessory);
        }
        catch
        {

          MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
          "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
        }
      }
    }
    private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      var textbox = sender as TextBox;
      switch (FilterComboBox.SelectedIndex)
      {
        case 0:
          PageGrid.ItemsSource = SourceCore.MyBase.CLIENTS.Where(q => q.FIO.Contains(textbox.Text)).ToList();
          break;
        case 1:
          PageGrid.ItemsSource = SourceCore.MyBase.CLIENTS.Where(q => q.PASSPORT_SERIA.ToString().Contains(textbox.Text)).ToList();
          break;
        case 2:
          PageGrid.ItemsSource = SourceCore.MyBase.CLIENTS.Where(q => q.PASSPORT_NUMBER.ToString().Contains(textbox.Text)).ToList();
          break;
        case 3:
          PageGrid.ItemsSource = SourceCore.MyBase.CLIENTS.Where(q => q.NUMBER.ToString().Contains(textbox.Text)).ToList();
          break;
        case 4:
          PageGrid.ItemsSource = SourceCore.MyBase.CLIENTS.Where(q => q.DATE_BIRTHDAY.ToString().Contains(textbox.Text)).ToList();
          break;
        case 5:
          PageGrid.ItemsSource = SourceCore.MyBase.CLIENTS.Where(q => q.MALE.ToString().Contains(textbox.Text)).ToList();
          break;
        case 7:
          PageGrid.ItemsSource = SourceCore.MyBase.CLIENTS.Where(q => q.LOGIN.Contains(textbox.Text)).ToList();
          break;
        case 8:
          PageGrid.ItemsSource = SourceCore.MyBase.CLIENTS.Where(q => q.PASSWORD.Contains(textbox.Text)).ToList();
          break;
        case 6:
          PageGrid.ItemsSource = SourceCore.MyBase.CLIENTS.Where(q => q.ROLE_ID.ToString().Contains(textbox.Text)).ToList();
          break;
      }
    }

    private void AddCommit_Click(object sender, RoutedEventArgs e)
    {
      StringBuilder errors = new StringBuilder();

      if (string.IsNullOrEmpty(RecordTextFIO.Text) || (RecordTextFIO.Text.Length < 8))
        errors.AppendLine("Укажите ФИО");

      if (string.IsNullOrEmpty(TypeComboBoxMale.Text))
        errors.AppendLine("Укажите пол");

      if (string.IsNullOrEmpty(TypeComboBoxRole.Text))
        errors.AppendLine("Укажите тип пользователя");

      if (string.IsNullOrEmpty(RecordTextPhoneNum.Text) || (RecordTextPhoneNum.Text.Length < 11))
        errors.AppendLine("Укажите номер");

      if (string.IsNullOrEmpty(RecordTextPassNum.Text) || (RecordTextPassNum.Text.Length < 6))
        errors.AppendLine("Укажите номер пасспорта");

      if (string.IsNullOrEmpty(RecordTextPassSeria.Text) || (RecordTextPassSeria.Text.Length < 4))
        errors.AppendLine("Укажите серия пасспорта");

      if (string.IsNullOrEmpty(RecordTextLogin.Text) || (RecordTextLogin.Text.Length < 6))
        errors.AppendLine("Укажите логин");

      if (!ValidationClass.CheckLoginExist(RecordTextLogin.Text))
      {
        errors.AppendLine("Такой логин уже существует");
      }

      if (string.IsNullOrEmpty(RecordTextPassword.Text) || (RecordTextPassword.Text.Length < 6))
        errors.AppendLine("Укажите пароль");

      if (string.IsNullOrEmpty(CalendarBirtdayPicker.Text))
        errors.AppendLine("Укажите дату рождения");


      if (errors.Length > 0)
      {
        MessageBox.Show(errors.ToString());
        return;
      }

      if (DlgMode == 0)
      {
        var NewBase = new Base.CLIENTS();
        NewBase.FIO = RecordTextFIO.Text.Trim();

        if ((String)TypeComboBoxMale.SelectedItem == "Мужской")
        {
          NewBase.MALE = true;
        }
        else
        {
          NewBase.MALE = false;
        }

        if ((String)TypeComboBoxRole.SelectedItem == "Администратор")
        {
          NewBase.ROLE_ID = true;
        }
        else
        {
          NewBase.ROLE_ID = false;
        }

        NewBase.PASSPORT_SERIA = int.Parse(RecordTextPassSeria.Text);
        NewBase.PASSPORT_NUMBER = int.Parse(RecordTextPassNum.Text);
        NewBase.DATE_BIRTHDAY = CalendarBirtdayPicker.SelectedDate;
        NewBase.NUMBER = RecordTextPhoneNum.Text;
        NewBase.LOGIN = RecordTextLogin.Text;
        NewBase.PASSWORD = RecordTextPassword.Text;
        SourceCore.MyBase.CLIENTS.Add(NewBase);
        SelectedItem = NewBase;
      }
      else
      {
        var EditBase = new Base.CLIENTS();
        EditBase = SourceCore.MyBase.CLIENTS.First(p => p.CLIENT_ID == SelectedItem.CLIENT_ID);
        EditBase.FIO = RecordTextFIO.Text.Trim();

        if ((String)TypeComboBoxMale.SelectedItem == "Мужской")
        {
          EditBase.MALE = true;
        }
        else
        {
          EditBase.MALE = false;
        }

        if ((String)TypeComboBoxRole.SelectedItem == "Администратор")
        {
          EditBase.ROLE_ID = true;
        }
        else
        {
          EditBase.ROLE_ID = false;
        }

        EditBase.PASSPORT_SERIA = int.Parse(RecordTextPassSeria.Text);
        EditBase.PASSPORT_NUMBER = int.Parse(RecordTextPassNum.Text);
        EditBase.DATE_BIRTHDAY = CalendarBirtdayPicker.SelectedDate;
        EditBase.NUMBER = RecordTextPhoneNum.Text;
        EditBase.LOGIN = RecordTextLogin.Text;
        EditBase.PASSWORD = RecordTextPassword.Text;
      }

      try
      {
        SourceCore.MyBase.SaveChanges();
        UpdateGrid(SelectedItem);
        DlgLoad(false, "");
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString());
      }
    }

    private void AddRollback_Click(object sender, RoutedEventArgs e)
    {
      UpdateGrid(SelectedItem);
      DlgLoad(false, "");
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
  }
}
