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
  /// Логика взаимодействия для BookingsPage1.xaml
  /// </summary>
  public partial class BookingsPage1 : Page
  {
    public BookingsPage1()
    {
      InitializeComponent();
      DataContext = this;
      UpdateGrid(null);
      DlgLoad(false, "");
      TypeComboBoxStatus.Items.Insert(0, "Оплачена");
      TypeComboBoxStatus.Items.Insert(1, "Не оплачена");
    }
    private int DlgMode = 0;
    public Base.BOOKING SelectedItem;
    public ObservableCollection<Base.BOOKING> BOOKINGS;
    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      List<string> Columns = new List<string>();
      for (int i = 0; i < 6; i++)
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

    public void UpdateGrid(Base.BOOKING BOOKING)
    {
      if ((BOOKING == null) && (PageGrid.ItemsSource != null))
      {
        BOOKING = (Base.BOOKING)PageGrid.SelectedItem;
      }
      BOOKINGS = new ObservableCollection<Base.BOOKING>(SourceCore.MyBase.BOOKING);
      PageGrid.ItemsSource = BOOKINGS;
      PageGrid.SelectedItem = BOOKING;
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
        SelectedItem = (Base.BOOKING)PageGrid.SelectedItem;
        RecordTextClientID.Text = SelectedItem.CLIENT_ID.ToString();
        RecordTextRouteID.Text = SelectedItem.ROUTE_ID.ToString();
        RecordTextCompanyID.Text = SelectedItem.COMPANY_ID.ToString();
        CalendarBookingPicker.Text = SelectedItem.DATE_BOOKING.ToString();
        RecordTextCountSeats.Text = SelectedItem.COUNT_SEATS.ToString();
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
        SelectedItem = (Base.BOOKING)PageGrid.SelectedItem;
        RecordTextClientID.Text = SelectedItem.CLIENT_ID.ToString();
        RecordTextRouteID.Text = SelectedItem.ROUTE_ID.ToString();
        RecordTextCompanyID.Text = SelectedItem.COMPANY_ID.ToString();
        CalendarBookingPicker.Text = SelectedItem.DATE_BOOKING.ToString();
        RecordTextCountSeats.Text = SelectedItem.COUNT_SEATS.ToString();
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
          Base.BOOKING DeletingAccessory = (Base.BOOKING)PageGrid.SelectedItem;
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
          Base.BOOKING SelectingAccessory = (Base.BOOKING)PageGrid.SelectedItem;
          SourceCore.MyBase.BOOKING.Remove(DeletingAccessory);
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
          PageGrid.ItemsSource = SourceCore.MyBase.BOOKING.Where(q => q.CLIENT_ID.ToString().Contains(textbox.Text)).ToList();
          break;
        case 1:
          PageGrid.ItemsSource = SourceCore.MyBase.BOOKING.Where(q => q.ROUTE_ID.ToString().Contains(textbox.Text)).ToList();
          break;
        case 2:
          PageGrid.ItemsSource = SourceCore.MyBase.BOOKING.Where(q => q.COMPANY_ID.ToString().Contains(textbox.Text)).ToList();
          break;
        case 3:
          PageGrid.ItemsSource = SourceCore.MyBase.BOOKING.Where(q => q.DATE_BOOKING.ToString().Contains(textbox.Text)).ToList();
          break;
        case 4:
          PageGrid.ItemsSource = SourceCore.MyBase.BOOKING.Where(q => q.STATUS.ToString().Contains(textbox.Text)).ToList();
          break;
        case 5:
          PageGrid.ItemsSource = SourceCore.MyBase.BOOKING.Where(q => q.COUNT_SEATS.ToString().Contains(textbox.Text)).ToList();
          break;
      }
    }

    private void AddCommit_Click(object sender, RoutedEventArgs e)
    {
      StringBuilder errors = new StringBuilder();

      if (string.IsNullOrEmpty(RecordTextClientID.Text))
        errors.AppendLine("Укажите ID Клиента");

      if (string.IsNullOrEmpty(TypeComboBoxStatus.Text))
        errors.AppendLine("Укажите статус брони");

      if (string.IsNullOrEmpty(RecordTextRouteID.Text))
        errors.AppendLine("Укажите ID Маршрута");

      if (string.IsNullOrEmpty(RecordTextCompanyID.Text))
        errors.AppendLine("Укажите ID Компании");

      if (string.IsNullOrEmpty(RecordTextCountSeats.Text))
        errors.AppendLine("Укажите количество занятых мест");

      if (string.IsNullOrEmpty(CalendarBookingPicker.Text))
        errors.AppendLine("Укажите дату брони");


      if (errors.Length > 0)
      {
        MessageBox.Show(errors.ToString());
        return;
      }

      if (DlgMode == 0)
      {
        var NewBase = new Base.BOOKING();
        NewBase.CLIENT_ID = int.Parse(RecordTextClientID.Text);

        if ((String)TypeComboBoxStatus.SelectedItem == "Оплачена")
        {
          NewBase.STATUS = true;
        }
        else
        {
          NewBase.STATUS = false;
        }

        NewBase.ROUTE_ID = int.Parse(RecordTextRouteID.Text);
        NewBase.COMPANY_ID = int.Parse(RecordTextCompanyID.Text);
        NewBase.DATE_BOOKING = CalendarBookingPicker.SelectedDate;
        NewBase.COUNT_SEATS = int.Parse(RecordTextCountSeats.Text);
        SourceCore.MyBase.BOOKING.Add(NewBase);
        SelectedItem = NewBase;
      }
      else
      {
        var EditBase = new Base.BOOKING();
        EditBase = SourceCore.MyBase.BOOKING.First(p => p.BOOKING_ID == SelectedItem.BOOKING_ID);
        EditBase.CLIENT_ID = int.Parse(RecordTextClientID.Text);

        if ((String)TypeComboBoxStatus.SelectedItem == "Оплачена")
        {
          EditBase.STATUS = true;
        }
        else
        {
          EditBase.STATUS = false;
        }

        EditBase.ROUTE_ID = int.Parse(RecordTextRouteID.Text);
        EditBase.COMPANY_ID = int.Parse(RecordTextCompanyID.Text);
        EditBase.DATE_BOOKING = CalendarBookingPicker.SelectedDate;
        EditBase.COUNT_SEATS = int.Parse(RecordTextCountSeats.Text);
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
