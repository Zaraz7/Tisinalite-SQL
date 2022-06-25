using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClassLibraryPassword;

namespace Tisinalite.Pages
{
    /// <summary>
    /// Логика взаимодействия для Regist.xaml
    /// </summary>
    public partial class Regist : Page
    {
        private static Users user = new Users(); 
        public Regist()
        {
            InitializeComponent();
            DataContext = user;
        }

        private void Regist_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMail.Text) || string.IsNullOrWhiteSpace(tbPassword.Password))
            {
                MessageBox.Show("Необходимые поля пусты");
                return;
            }
            if (tbPassword.Password != tbPasswordRepiat.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            if (!PasswordTest.PasswordHardCorrect(tbPassword.Password))
            {
                MessageBox.Show("Легкий пароль. Используйте латинские буквы разной высоты в перемешку с числами и специальными символами");
                return;
            }
            try { 
            using (var db = new TisinaliteDBEntities())
            {
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == tbMail.Text);
                if (user != null)
                {
                    MessageBox.Show("Такой пользователь уже зарегистрирован. Используйте другую почту");
                    return;
                }
            }
                user.Password = tbPassword.Password;
                TisinaliteDBEntities.GetContext().Users.Add(user);
                TisinaliteDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались");
                Global.MainFrame.GoBack();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Global.MainFrame.Navigate(new Login());
        }
    }
}
