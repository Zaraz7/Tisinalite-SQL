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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLogin.Text) || string.IsNullOrEmpty(pbPassword.Password))
            {
                MessageBox.Show("Сначало заполните все поля.");
                return;
            }
            using (var db = new TisinaliteDBEntities())
            {
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == tbLogin.Text && u.Password == pbPassword.Password);
                if (user == null)
                {
                    MessageBox.Show("Неверный логин или пароль.");
                    return;
                }
                Global.MainFrame.Navigate(new Pages.General(user));
            }
            
        }

        private void Regist_Click(object sender, RoutedEventArgs e)
        {
            Global.MainFrame.Navigate(new Regist());
        }
    }
}
