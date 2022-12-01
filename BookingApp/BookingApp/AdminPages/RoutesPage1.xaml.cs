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
  /// Логика взаимодействия для RoutesPage1.xaml
  /// </summary>
  public partial class RoutesPage1 : Page
  {
    public RoutesPage1()
    {
      InitializeComponent();
      DataContext = this;
      UpdateGrid(null);
      DlgLoad(false, "");
      CalendarOnPicker.DisplayDateStart = DateTime.Today;
      CalendarOffPicker.DisplayDateStart = CalendarOnPicker.SelectedDate;
    }
    private int DlgMode = 0;
    public Base.ROUTE SelectedItem;
    public ObservableCollection<Base.ROUTE> ROUTES;

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      List<string> Columns = new List<string>();
      for (int i = 0; i < 8; i++)
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
    public void UpdateGrid(Base.ROUTE ROUTE)
    {
      if ((ROUTE == null) && (PageGrid.ItemsSource != null))
      {
        ROUTE = (Base.ROUTE)PageGrid.SelectedItem;
      }
      ROUTES = new ObservableCollection<Base.ROUTE>(SourceCore.MyBase.ROUTE);
      PageGrid.ItemsSource = ROUTES;
      PageGrid.SelectedItem = ROUTE;
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
        SelectedItem = (Base.ROUTE)PageGrid.SelectedItem;
        RecordTextCompanyID.Text = SelectedItem.COMPANY_ID.ToString();
        RecordTextTransportID.Text = SelectedItem.TRANSPORT_ID.ToString();
        RecordTextWhereOn.Text = SelectedItem.WHERE_ON;
        RecordTextWhereOf.Text = SelectedItem.WHERE_OF;
        CalendarOnPicker.Text = SelectedItem.DATE_ON.ToString();
        CalendarOffPicker.Text = SelectedItem.DATE_OFF.ToString();
        RecordTextTimeInRoute.Text = SelectedItem.TIME_ROUTE.ToString();
        RecordTextPrice.Text = SelectedItem.PRICE_ROUTE.ToString();
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
        SelectedItem = (Base.ROUTE)PageGrid.SelectedItem;
        RecordTextCompanyID.Text = SelectedItem.COMPANY_ID.ToString();
        RecordTextTransportID.Text = SelectedItem.TRANSPORT_ID.ToString();
        RecordTextWhereOn.Text = SelectedItem.WHERE_ON;
        RecordTextWhereOf.Text = SelectedItem.WHERE_OF;
        CalendarOnPicker.Text = SelectedItem.DATE_ON.ToString();
        CalendarOffPicker.Text = SelectedItem.DATE_OFF.ToString();
        RecordTextTimeInRoute.Text = SelectedItem.TIME_ROUTE.ToString();
        RecordTextPrice.Text = SelectedItem.PRICE_ROUTE.ToString();
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
          Base.ROUTE DeletingAccessory = (Base.ROUTE)PageGrid.SelectedItem;
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
          Base.ROUTE SelectingAccessory = (Base.ROUTE)PageGrid.SelectedItem;
          SourceCore.MyBase.ROUTE.Remove(DeletingAccessory);
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
          PageGrid.ItemsSource = SourceCore.MyBase.ROUTE.Where(q => q.COMPANY_ID.ToString().Contains(textbox.Text)).ToList();
          break;
        case 1:
          PageGrid.ItemsSource = SourceCore.MyBase.ROUTE.Where(q => q.TRANSPORT_ID.ToString().Contains(textbox.Text)).ToList();
          break;
        case 2:
          PageGrid.ItemsSource = SourceCore.MyBase.ROUTE.Where(q => q.WHERE_ON.ToString().Contains(textbox.Text)).ToList();
          break;
        case 3:
          PageGrid.ItemsSource = SourceCore.MyBase.ROUTE.Where(q => q.WHERE_OF.ToString().Contains(textbox.Text)).ToList();
          break;
        case 4:
          PageGrid.ItemsSource = SourceCore.MyBase.ROUTE.Where(q => q.DATE_ON.ToString().Contains(textbox.Text)).ToList();
          break;
        case 5:
          PageGrid.ItemsSource = SourceCore.MyBase.ROUTE.Where(q => q.DATE_OFF.ToString().Contains(textbox.Text)).ToList();
          break;
        case 6:
          PageGrid.ItemsSource = SourceCore.MyBase.ROUTE.Where(q => q.TIME_ROUTE.ToString().Contains(textbox.Text)).ToList();
          break;
        case 7:
          PageGrid.ItemsSource = SourceCore.MyBase.ROUTE.Where(q => q.PRICE_ROUTE.ToString().Contains(textbox.Text)).ToList();
          break;
      }
    }

    private void AddCommit_Click(object sender, RoutedEventArgs e)
    {
      StringBuilder errors = new StringBuilder();



      if (string.IsNullOrEmpty(RecordTextCompanyID.Text))
        errors.AppendLine("Укажите ID Компании");

      if (string.IsNullOrEmpty(RecordTextTransportID.Text))
        errors.AppendLine("Укажите ID Транспорта");

      if (string.IsNullOrEmpty(RecordTextWhereOn.Text))
        errors.AppendLine("Укажите откуда");

      if (string.IsNullOrEmpty(RecordTextWhereOf.Text))
        errors.AppendLine("Укажите куда");

      if (string.IsNullOrEmpty(CalendarOnPicker.Text))
        errors.AppendLine("Укажите дату туда");

      if (string.IsNullOrEmpty(CalendarOffPicker.Text))
        errors.AppendLine("Укажите дату оттуда");

      if (string.IsNullOrEmpty(RecordTextTimeInRoute.Text))
        errors.AppendLine("Укажите время в пути");

      if (string.IsNullOrEmpty(RecordTextPrice.Text))
        errors.AppendLine("Укажите цену");




      if (errors.Length > 0)
      {
        MessageBox.Show(errors.ToString());
        return;
      }

      if (DlgMode == 0)
      {
        var NewBase = new Base.ROUTE();
        NewBase.COMPANY_ID = int.Parse(RecordTextCompanyID.Text);
        NewBase.DATE_ON = CalendarOnPicker.SelectedDate;
        NewBase.DATE_OFF = CalendarOffPicker.SelectedDate;
        NewBase.TRANSPORT_ID = int.Parse(RecordTextTransportID.Text);
        NewBase.WHERE_ON = RecordTextWhereOn.Text;
        NewBase.WHERE_OF = RecordTextWhereOf.Text;
        NewBase.TIME_ROUTE = TimeSpan.Parse(RecordTextTimeInRoute.Text);
        NewBase.PRICE_ROUTE = int.Parse(RecordTextPrice.Text);
        SourceCore.MyBase.ROUTE.Add(NewBase);
        SelectedItem = NewBase;
      }
      else
      {
        var EditBase = new Base.ROUTE();
        EditBase = SourceCore.MyBase.ROUTE.First(p => p.ROUTE_ID == SelectedItem.ROUTE_ID);
        EditBase.COMPANY_ID = int.Parse(RecordTextCompanyID.Text);
        EditBase.DATE_ON = CalendarOnPicker.SelectedDate;
        EditBase.DATE_OFF = CalendarOffPicker.SelectedDate;
        EditBase.TRANSPORT_ID = int.Parse(RecordTextTransportID.Text);
        EditBase.WHERE_ON = RecordTextWhereOn.Text;
        EditBase.WHERE_OF = RecordTextWhereOf.Text;
        EditBase.TIME_ROUTE = TimeSpan.Parse(RecordTextTimeInRoute.Text);
        EditBase.PRICE_ROUTE = int.Parse(RecordTextPrice.Text);
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
