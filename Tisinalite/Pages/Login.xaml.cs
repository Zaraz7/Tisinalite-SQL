using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            // Проверка на содержание пустых полей
            if (string.IsNullOrEmpty(tbLogin.Text) || string.IsNullOrEmpty(pbPassword.Password))
            {
                MessageBox.Show("Сначало заполните все поля.");
                return;
            }
            // Попытка авторизироваться
            using (var db = new TisinaliteDBEntities())
            {
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == tbLogin.Text && u.Password == pbPassword.Password);
                if (user == null)
                {
                    MessageBox.Show("Неверный логин или пароль.");
                    return;
                }
                // Открытие главной страницы с авторизированными данными 
                Global.MainFrame.Navigate(new General(user));
            }
            
        }

        private void Regist_Click(object sender, RoutedEventArgs e)
        {
            Global.MainFrame.Navigate(new Regist());
        }
    }
}
