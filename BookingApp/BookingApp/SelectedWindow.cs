using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp
{
    internal class SelectedWindow
    {

        public static void ChangeWindow(string nameWindow, Window currentWindow)
        {
            switch (nameWindow)
            {
                case "RegistrationWindow":
                    RegistrationWindow registrationWindow = new RegistrationWindow();
                    currentWindow.Hide();
                    registrationWindow.ShowDialog();
                    currentWindow.Close();
                    break;
                case "AuthorizationWindow":
                    AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                    currentWindow.Hide();
                    authorizationWindow.ShowDialog();
                    currentWindow.Close();
                    break;
                case "AdminWindow":
                    AdminWindow adminWindow = new AdminWindow();
                    currentWindow.Hide();
                    adminWindow.ShowDialog();
                    currentWindow.Close();
                    break;
                case "MainWindow":
                    MainWindow mainWindow = new MainWindow();
                    currentWindow.Hide();
                    mainWindow.ShowDialog();
                    currentWindow.Close();
                    break;
                case "PayWindow":
                    MainPages.PayWindow payWindow = new MainPages.PayWindow();
                    currentWindow.Hide();
                    payWindow.ShowDialog();
                    break;
            }
        }


        public static void ChangePage(string nameWindow)
        {
            MainPages.PayWindow payWindow = new MainPages.PayWindow();
            payWindow.ShowDialog();
        }
    }
}
