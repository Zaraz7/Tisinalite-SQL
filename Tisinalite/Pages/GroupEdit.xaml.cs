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
    /// <summary>
    /// Логика взаимодействия для GroupEdit.xaml
    /// </summary>
    public partial class GroupEdit : Window
    {
        Groups group;
        int masterID;
        public GroupEdit(int _masterID, Groups _group = null)
        {
            if (_group == null)
            {
                group = new Groups();
                masterID = _masterID;
            }
            else
                group = _group;

            InitializeComponent();
        }

        private void SaveGroup_Click(object sender, RoutedEventArgs e)
        {
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
                try { 
                group.MasterID = masterID;
                TisinaliteDBEntities.GetContext().Groups.Add(group);
                TisinaliteDBEntities.GetContext().SaveChanges();

                TisinaliteDBEntities.GetContext().UsersOfGroups.Add(new UsersOfGroups { GroupID = group.ID, UserID = masterID});
                TisinaliteDBEntities.GetContext().SaveChanges();

                if (group.Access == "public")
                    {
                        tbID.Text = $"Код группы: {group.ID}";
                        tbID.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString());
                }
            }
        }
    }
}
