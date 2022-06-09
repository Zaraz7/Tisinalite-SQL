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
using System.Windows.Shapes;

namespace Tisinalite.Pages
{
    /// <summary>
    /// Логика взаимодействия для Regist.xaml
    /// </summary>
    public partial class Regist : Page
    {
        public Regist()
        {
            InitializeComponent();
        }

        private void Regist_Click(object sender, RoutedEventArgs e)
        {
            Login_Click(sender, e);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Global.MainFrame.Navigate(new Login());
        }
    }
}
