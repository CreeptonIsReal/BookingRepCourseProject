﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D87A8BDAB3B69A6D3D5FD19BDB844EFC2CB6056E317B24F13D12B5465A716B4E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using BookingApp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace BookingApp {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel AutorizOrRegistrStackPanel;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel UserStackPanel;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label UserNameLabel;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AdminPanelButton;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel SelectLabelStackPanel;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WhereOn_Text;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WhereOf_Text;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Passager_Text;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchTicketButton;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame RootFrame;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BookingApp;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.AutorizOrRegistrStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            
            #line 34 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Login_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 35 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Sign_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.UserStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.UserNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.AdminPanelButton = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\MainWindow.xaml"
            this.AdminPanelButton.Click += new System.Windows.RoutedEventHandler(this.AdminPanel_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 42 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.SelectLabelStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            this.WhereOn_Text = ((System.Windows.Controls.TextBox)(target));
            
            #line 66 "..\..\MainWindow.xaml"
            this.WhereOn_Text.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.WhereOn_Text_TextChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.WhereOf_Text = ((System.Windows.Controls.TextBox)(target));
            
            #line 67 "..\..\MainWindow.xaml"
            this.WhereOf_Text.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.WhereOf_Text_TextChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Passager_Text = ((System.Windows.Controls.TextBox)(target));
            
            #line 69 "..\..\MainWindow.xaml"
            this.Passager_Text.LostFocus += new System.Windows.RoutedEventHandler(this.Passager_Text_LostFocus);
            
            #line default
            #line hidden
            
            #line 69 "..\..\MainWindow.xaml"
            this.Passager_Text.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            this.SearchTicketButton = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\MainWindow.xaml"
            this.SearchTicketButton.Click += new System.Windows.RoutedEventHandler(this.SearchTicketButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.RootFrame = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
