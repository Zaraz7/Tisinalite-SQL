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
using System.Windows.Shapes;
using System.Diagnostics;

namespace Tisinalite.Pages
{
    public partial class GroupEdit : Window
    {
        Groups group;
        public GroupEdit(int _masterID, Groups _group = null)
        {
            // Проверка на наличие группы в БД
            if (_group == null)
            {
                group = new Groups { MasterID = _masterID };
            }
            else
            {
                cbAccess.IsEnabled = false;
                group = _group;
            }

            InitializeComponent();
            DataContext = group;
        }

        private void SaveGroup_Click(object sender, RoutedEventArgs e)
        {
            // Запрещаем вписывать пустые поля
            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                MessageBox.Show("Неправильное имя");
                return;
            }
            group.Title = tbTitle.Text;
            group.Access = ((ComboBoxItem)cbAccess.SelectedItem).Tag.ToString();
            Debug.WriteLine(group.Title + " " + group.Access);
            if (group.ID == 0)
            {
                // Разграничение доступности в зависимости от выбранного параметра в combobox
                switch (cbAccess.SelectedIndex)
                {
                    case 0:
                        group.Access = "private";
                        break;
                    case 1:
                        group.Access = "public";
                        break;
                }
                // Попытка сохранить значения
                try
                {
                    TisinaliteDBEntities.GetContext().Groups.Add(group);
                    TisinaliteDBEntities.GetContext().SaveChanges();

                    TisinaliteDBEntities.GetContext().UsersOfGroups.Add(new UsersOfGroups { GroupID = group.ID, UserID = group.MasterID });
                    TisinaliteDBEntities.GetContext().SaveChanges();

                    if (group.Access == "public")
                        tbID.Text = $"Код группы: {group.ID}";
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString());
                }
            }
            else
            {
                try
                {
                    TisinaliteDBEntities.GetContext().SaveChanges();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString());
                }
            }
        }
    }
}
