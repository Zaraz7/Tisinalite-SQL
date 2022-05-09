using System;
using System.Collections.Generic;
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
using System.Diagnostics;
using MdXaml;
using System.IO;
using Microsoft.Win32;


namespace Tisinalite
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pages.Login login;
        public MainWindow()
        {
            InitializeComponent();

            login = new Pages.Login();
            MainFrame.Navigate(login);
            Global.MainFrame = MainFrame;

        }

        private void ClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // При закрытии приложения
        }
    }
}
