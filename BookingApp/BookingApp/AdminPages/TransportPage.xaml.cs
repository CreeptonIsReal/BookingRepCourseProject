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
  /// Логика взаимодействия для TransportPage.xaml
  /// </summary>
  public partial class TransportPage : Page
  {
    public TransportPage()
    {
      InitializeComponent();
      DataContext = this;
      UpdateGrid(null);
      DlgLoad(false, "");
      TypeComboBox.Items.Insert(0, "Самолет");
      TypeComboBox.Items.Insert(1, "Поезд");
    }

    private int DlgMode = 0;
    public Base.TRANSPORT SelectedItem;
    public ObservableCollection<Base.TRANSPORT> TRANSPORTS;

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      List<string> Columns = new List<string>();
      for (int i = 0; i < 5; i++)
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

    public void UpdateGrid(Base.TRANSPORT TRANSPORT)
    {
      if ((TRANSPORT == null) && (PageGrid.ItemsSource != null))
      {
        TRANSPORT = (Base.TRANSPORT)PageGrid.SelectedItem;
      }
      TRANSPORTS = new ObservableCollection<Base.TRANSPORT>(SourceCore.MyBase.TRANSPORT);
      PageGrid.ItemsSource = TRANSPORTS;
      PageGrid.SelectedItem = TRANSPORT;
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
        SelectedItem = (Base.TRANSPORT)PageGrid.SelectedItem;
        RecordTextTransportName.Text = SelectedItem.NAME_TRANSPORT;
        RecordSeatsText.Text = SelectedItem.NUM_SEATS.ToString();
        RecordIDCompanyText.Text = SelectedItem.COMPANY_ID.ToString();
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
        SelectedItem = (Base.TRANSPORT)PageGrid.SelectedItem;
        RecordTextTransportName.Text = SelectedItem.NAME_TRANSPORT;
        RecordSeatsText.Text = SelectedItem.NUM_SEATS.ToString();
        RecordIDCompanyText.Text = SelectedItem.COMPANY_ID.ToString();
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
          Base.TRANSPORT DeletingAccessory = (Base.TRANSPORT)PageGrid.SelectedItem;
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
          Base.TRANSPORT SelectingAccessory = (Base.TRANSPORT)PageGrid.SelectedItem;
          SourceCore.MyBase.TRANSPORT.Remove(DeletingAccessory);
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
          PageGrid.ItemsSource = SourceCore.MyBase.TRANSPORT.Where(q => q.TRANSPORT_ID.ToString().Contains(textbox.Text)).ToList();
          break;
        case 1:
          PageGrid.ItemsSource = SourceCore.MyBase.TRANSPORT.Where(q => q.TYPE_TRANSPORT.ToString().Contains(textbox.Text)).ToList();
          break;
        case 2:
          PageGrid.ItemsSource = SourceCore.MyBase.TRANSPORT.Where(q => q.NUM_SEATS.ToString().Contains(textbox.Text)).ToList();
          break;
        case 3:
          PageGrid.ItemsSource = SourceCore.MyBase.TRANSPORT.Where(q => q.COMPANY_ID.ToString().Contains(textbox.Text)).ToList();
          break;
        case 4:
          PageGrid.ItemsSource = SourceCore.MyBase.TRANSPORT.Where(q => q.NAME_TRANSPORT.Contains(textbox.Text)).ToList();
          break;
      }
    }

    private void AddCommit_Click(object sender, RoutedEventArgs e)
    {
      StringBuilder errors = new StringBuilder();

      if (string.IsNullOrEmpty(RecordTextTransportName.Text))
        errors.AppendLine("Укажите название транспорта");

      if (string.IsNullOrEmpty(TypeComboBox.Text))
        errors.AppendLine("Укажите тип транспорта");

      if (string.IsNullOrEmpty(RecordSeatsText.Text))
        errors.AppendLine("Укажите количество сидений");

      if (string.IsNullOrEmpty(RecordIDCompanyText.Text))
        errors.AppendLine("Укажите количество сидений");


      if (errors.Length > 0)
      {
        MessageBox.Show(errors.ToString());
        return;
      }

      if (DlgMode == 0)
      {
        var NewBase = new Base.TRANSPORT();
        NewBase.NAME_TRANSPORT = RecordTextTransportName.Text.Trim();
        if ((String)TypeComboBox.SelectedItem == "Самолет")
        {
          NewBase.TYPE_TRANSPORT = true;
        }
        else
        {
          NewBase.TYPE_TRANSPORT = false;
        }
        NewBase.NUM_SEATS = int.Parse(RecordSeatsText.Text);
        NewBase.COMPANY_ID = int.Parse(RecordIDCompanyText.Text);
        SourceCore.MyBase.TRANSPORT.Add(NewBase);
        SelectedItem = NewBase;
      }
      else
      {
        var EditBase = new Base.TRANSPORT();
        EditBase = SourceCore.MyBase.TRANSPORT.First(p => p.TRANSPORT_ID == SelectedItem.TRANSPORT_ID);
        EditBase.NAME_TRANSPORT = RecordTextTransportName.Text.Trim();
        if ((String)TypeComboBox.SelectedItem == "Самолет")
        {
          EditBase.TYPE_TRANSPORT = true;
        }
        else
        {
          EditBase.TYPE_TRANSPORT = false;
        }
        EditBase.NUM_SEATS = int.Parse(RecordSeatsText.Text);
        EditBase.COMPANY_ID = int.Parse(RecordIDCompanyText.Text);
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

  }
}
