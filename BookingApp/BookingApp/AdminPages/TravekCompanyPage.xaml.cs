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
  /// Логика взаимодействия для TravekCompanyPage.xaml
  /// </summary>
  public partial class TravekCompanyPage : Page
  {
    public TravekCompanyPage()
    {
      InitializeComponent();
      DataContext = this;
      UpdateGrid(null);
      DlgLoad(false, "");
      TypeComboBox.Items.Insert(0, "Авиа");
      TypeComboBox.Items.Insert(1, "ЖД");
    }

    private int DlgMode = 0;
    public Base.TRAVEK_COMPANY SelectedItem;
    public ObservableCollection<Base.TRAVEK_COMPANY> TRAVEK_COMPANYS;

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      List<string> Columns = new List<string>();
      for (int i = 0; i < 3; i++)
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
    public void UpdateGrid(Base.TRAVEK_COMPANY TRAVEK_COMPANY)
    {
      if ((TRAVEK_COMPANY == null) && (PageGrid.ItemsSource != null))
      {
        TRAVEK_COMPANY = (Base.TRAVEK_COMPANY)PageGrid.SelectedItem;
      }
      TRAVEK_COMPANYS = new ObservableCollection<Base.TRAVEK_COMPANY>(SourceCore.MyBase.TRAVEK_COMPANY);
      PageGrid.ItemsSource = TRAVEK_COMPANYS;
      PageGrid.SelectedItem = TRAVEK_COMPANY;
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
        SelectedItem = (Base.TRAVEK_COMPANY)PageGrid.SelectedItem;
        RecordTextCompanyName.Text = SelectedItem.NAME_COMPANY;
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
        SelectedItem = (Base.TRAVEK_COMPANY)PageGrid.SelectedItem;
        RecordTextCompanyName.Text = SelectedItem.NAME_COMPANY;
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
          Base.TRAVEK_COMPANY DeletingAccessory = (Base.TRAVEK_COMPANY)PageGrid.SelectedItem;
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
          Base.TRAVEK_COMPANY SelectingAccessory = (Base.TRAVEK_COMPANY)PageGrid.SelectedItem;
          SourceCore.MyBase.TRAVEK_COMPANY.Remove(DeletingAccessory);
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
          PageGrid.ItemsSource = SourceCore.MyBase.TRAVEK_COMPANY.Where(q => q.COMPANY_ID.ToString().Contains(textbox.Text)).ToList();
          break;
        case 1:
          PageGrid.ItemsSource = SourceCore.MyBase.TRAVEK_COMPANY.Where(q => q.NAME_COMPANY.Contains(textbox.Text)).ToList();
          break;
        case 2:
          PageGrid.ItemsSource = SourceCore.MyBase.TRAVEK_COMPANY.Where(q => q.TYPE_COMPANY.ToString().Contains(textbox.Text)).ToList();
          break;
      }
    }

    private void AddCommit_Click(object sender, RoutedEventArgs e)
    {
      StringBuilder errors = new StringBuilder();

      if (string.IsNullOrEmpty(RecordTextCompanyName.Text))
        errors.AppendLine("Укажите название компании");

      if (string.IsNullOrEmpty(TypeComboBox.Text))
        errors.AppendLine("Укажите тип компании");



      if (errors.Length > 0)
      {
        MessageBox.Show(errors.ToString());
        return;
      }

      if (DlgMode == 0)
      {
        var NewBase = new Base.TRAVEK_COMPANY();
        NewBase.NAME_COMPANY = RecordTextCompanyName.Text.Trim();
        if ((String)TypeComboBox.SelectedItem == "Авиа")
        {
          NewBase.TYPE_COMPANY = true;
        }
        else
        {
          NewBase.TYPE_COMPANY = false;
        }
        SourceCore.MyBase.TRAVEK_COMPANY.Add(NewBase);
        SelectedItem = NewBase;
      }
      else
      {
        var EditBase = new Base.TRAVEK_COMPANY();
        EditBase = SourceCore.MyBase.TRAVEK_COMPANY.First(p => p.COMPANY_ID == SelectedItem.COMPANY_ID);
        EditBase.NAME_COMPANY = RecordTextCompanyName.Text.Trim();
        if ((String)TypeComboBox.SelectedItem == "Авиа")
        {
          EditBase.TYPE_COMPANY = true;
        }
        else
        {
          EditBase.TYPE_COMPANY = false;
        }
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
