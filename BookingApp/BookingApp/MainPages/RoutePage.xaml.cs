using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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

namespace BookingApp.MainPages
{
  /// <summary>
  /// Логика взаимодействия для RoutePage.xaml
  /// </summary>
  public partial class RoutePage : Page
  {
    //public static Base.CLIENTS Client = null;

    public class BookingD
    {
      public int? IdRoute { get; set; }
      public int? IdCompany { get; set; }
      public int? CountSeats { get; set; }
      public int? CountSeatsFree { get; set; }
      public int? CountSeatsFill { get; set; }
      public int? CountSeatsFreeOneRoute { get; set; }
      public string CompanyName { get; set; }
      public string TransportName { get; set; }
      public string WhereOfName { get; set; }
      public string WhereOnName { get; set; }
      public DateTime? DateOn { get; set; }
      public DateTime? DateOf { get; set; }
      public TimeSpan? TimeRoute { get; set; }
      public double Price { get; set; }
    }
    public RoutePage()
    {
      InitializeComponent();
      DataContext = this;
      UpdateGridS(null);
      BookingButton.Visibility = Visibility.Visible;

    }

    public static double MoneyOf;



    public Base.BOOKING SelectedItemBooking;
    public Base.ROUTE SelectedItemRoute;
    public BookingD SelectedItemRouteClass;
    public ObservableCollection<Base.ROUTE> ROUTES;
    public ObservableCollection<BookingD> Bookings;
    public BookingD SelectedItem;

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      List<string> Columns = new List<string>();
      for (int i = 0; i < 7; i++)
      {
        Columns.Add(PageGrid.Columns[i].Header.ToString());
      }
      foreach (DataGridColumn Column in PageGrid.Columns)
      {
        Column.CanUserSort = false;
      }
    }

    private void UpdateGridS(BookingD BookingD)
    {
      if ((BookingD == null) && (PageGrid.ItemsSource != null))
      {
        BookingD = (BookingD)PageGrid.SelectedItem;
      }
      var s = (from p in SourceCore.MyBase.ROUTE
                 //join f in SourceCore.MyBase.BOOKING on p.ROUTE_ID equals f.ROUTE_ID
               join c in SourceCore.MyBase.TRAVEK_COMPANY on p.COMPANY_ID equals c.COMPANY_ID
               join d in SourceCore.MyBase.TRANSPORT on p.TRANSPORT_ID equals d.TRANSPORT_ID
               select new
               {
                 IdCompany = p.COMPANY_ID,
                 IdRoute = p.ROUTE_ID,
                 CompanyName = c.NAME_COMPANY,
                 TransportName = d.NAME_TRANSPORT,
                 WhereOfName = p.WHERE_OF,
                 WhereOnName = p.WHERE_ON,
                 DateOn = p.DATE_ON,
                 DateOf = p.DATE_OFF,
                 TimeRoute = p.TIME_ROUTE,
                 Price = p.PRICE_ROUTE,
                 CountSeatsFree = d.NUM_SEATS,
                 CountSeats = MainWindow.CountPass,

                 CountSeatsFill = (from a in SourceCore.MyBase.BOOKING where (p.ROUTE_ID == a.ROUTE_ID) select a.COUNT_SEATS).Sum() == null
                 ? (0) : ((from a in SourceCore.MyBase.BOOKING where (p.ROUTE_ID == a.ROUTE_ID) select a.COUNT_SEATS).Sum()),

                 CountSeatsFreeOneRoute = d.NUM_SEATS
               }); ;
      Bookings = new ObservableCollection<BookingD>();
      foreach (var item in s)
      {
        Bookings.Add(new BookingD
        {
          IdCompany = item.IdCompany,
          IdRoute = item.IdRoute,
          CompanyName = item.CompanyName,
          TransportName = item.TransportName,
          WhereOfName = item.WhereOfName,
          WhereOnName = item.WhereOnName,
          DateOn = item.DateOn,
          DateOf = item.DateOf,
          TimeRoute = item.TimeRoute,
          Price = (double)item.Price,
          CountSeats = (int)item.CountSeats,
          CountSeatsFree = item.CountSeatsFree,
          CountSeatsFill = item.CountSeatsFill,
          CountSeatsFreeOneRoute = item.CountSeatsFree - item.CountSeatsFill
        }); ;
      }
      PageGrid.ItemsSource = Bookings.Where(q => q.WhereOnName.Contains(MainWindow.WhereOn) && q.WhereOfName.Contains(MainWindow.WhereOf)).ToList();
      PageGrid.SelectedItem = BookingD;
    }


    private void BookingButton_Click(object sender, RoutedEventArgs e)
    {
      if (MainWindow.Client != null)
      {
        if (PageGrid.SelectedItem != null )
        {
          SelectedItemRouteClass = (BookingD)PageGrid.SelectedItem;
          if (SelectedItemRouteClass.CountSeatsFreeOneRoute > MainWindow.CountPass)
          {
            var NewBase = new Base.BOOKING();
            NewBase.CLIENT_ID = MainWindow.Client.CLIENT_ID;
            NewBase.ROUTE_ID = SelectedItemRouteClass.IdRoute;
            NewBase.COMPANY_ID = SelectedItemRouteClass.IdCompany;
            NewBase.DATE_BOOKING = DateTime.Today;
            NewBase.COUNT_SEATS = SelectedItemRouteClass.CountSeats;
            MoneyOf = (double)SelectedItemRouteClass.Price * MainWindow.CountPass;
            SelectedWindow.ChangePage("PayWindow");

            if (PayWindow.PayValid)
            {
              NewBase.STATUS = true;
            }
            else
            {
              NewBase.STATUS = false;
            }
            SourceCore.MyBase.BOOKING.Add(NewBase);
            SelectedItemBooking = NewBase;
          }
          else
          {
            MessageBox.Show("Не осталось свободных мест для " + MainWindow.CountPass + " человек", "Сообщение", MessageBoxButton.OK);
          }

          try
          {
            SourceCore.MyBase.SaveChanges();
            UpdateGridS(SelectedItem);
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.Message.ToString());
          }
        }
        else
        {
          MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
        }
      }
      else
      {
        MessageBox.Show("Авторизуйтесь в приложении!", "Вы не авторизованны !", MessageBoxButton.OK);
      }
    }

  }
}
