   M S B A M L   `   `   `  ????E  ABookingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:BookingApp.MainPagesBookingApp.MainPages  5      $    BookingApp.MainPages.RoutePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:BookingApp.MainPages     5        N? Title$  	RoutePage??5	      .??5      ? .+?5      &   System.Windows.Controls.DataGrid  -   5          Name$ PageGrid??5   ,   $??2??6   $??0??6      AutoGenerateColumns .     CanUserAddRows .  $	??450??6q   ???#0c73fe???	#FF0C91FE$??Arial Rounded MT Bold)?6?      
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5   
   ,  &System.Windows.Controls.DataGridColumn   Header$' !Название компании??6      Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   ?? 	 ?? Path$	 CompanyName?6]     5   
   $+ %Название транспорта??6   $	 10* 6a    6:   ?? $	 TransportName?6a     5   
   $ Откуда??6   $	 10* 6R    6-   ?? $	 WhereOnName?6R     5   
   $ Куда??6   $	 10* 6P    6+   ?? $	 WhereOfName?6P     5   
   $ Дата туда??6   $	 10* 6o    60   ?? $	 DateOn?
 ?? StringFormat$
 {0:dd.MM.yyyy}??6o     5   
   $ Дата обратно??6   $	 10* 6r    63   ?? $	 DateOf?$
 {0:dd.MM.yyyy}??6r     5   
   $ Время в пути??6   $	 10* 6V    63   ?? $	 	TimeRoute?6V     5   
   $ Стоимость??6   $	 10* 6O    60   ?? $	 Price?6O     5   
   $ Свободные??6   $	 10* 6`    60   ?? $	 CountSeatsFreeOneRoute?6`   5      ?? 5      -   5         ??Name$ BookingButton??5      $+??%Забронировать билет??6$    ? Cursor$
 Hand??6C   $??35??6Q   $??0,0,0,0q?6]      PrimeButton#??[  6n   $??1??6?   $??3??6?   $y?	Collapsed=?6?   $??Bottom=?6?   6?   5      5      