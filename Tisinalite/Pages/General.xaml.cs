using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using MdXaml;

namespace Tisinalite.Pages
{
    /// <summary>
    /// Логика взаимодействия для General.xaml
    /// </summary>
    public partial class General : Page
    {
        private Settings _settings = Settings.GetSettings();
        //private string notePath;
        Users user;
        Notes openNote= new Notes();
        Groups selectedGroup = new Groups();
        public General(Users _user)
        {
            user = _user;
            InitializeComponent();

            UpdateTreeView();
            Binding binding = new Binding();
            binding.ElementName = "tbEditor";
            binding.Path = new PropertyPath("Text");
            Markdownview.SetBinding(MarkdownScrollViewer.MarkdownProperty, binding);

        }
        public void SaveSettings()
        {
            _settings.Save();
        }

        private void NewExecute(object sender, ExecutedRoutedEventArgs e)
        {

            if (openNote.ID != 0)
                SaveNote();
            NewNote();
            UpdateTreeView();
        }
        private void SaveExecute(object sender, ExecutedRoutedEventArgs e)
        {
            SaveNote();
            UpdateTreeView();
        }
        private void DeleteExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите удалить \"{openNote.Title}\"?", "Подтверждение действия", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            try
            {
                TisinaliteDBEntities.GetContext().Notes.Remove(openNote);
                TisinaliteDBEntities.GetContext().SaveChanges();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
            UpdateTreeView();
        }
        private void CloseExecute(object sender, ExecutedRoutedEventArgs e)
        {
            SaveExecute(sender, e);
            Application.Current.Shutdown();
        }
        private void UpdateTreeView()
        {
            tvNotes.Items.Clear();
            var userOfGroups = TisinaliteDBEntities.GetContext().UsersOfGroups.ToList();
            var groups = TisinaliteDBEntities.GetContext().UsersOfGroups.ToList();

            userOfGroups = userOfGroups.Where(g => g.UserID == user.ID).ToList();


            foreach (var link in userOfGroups)
            {
                using (var db = new TisinaliteDBEntities())
                {
                    var group = db.Groups.AsNoTracking().FirstOrDefault(g => g.ID == link.GroupID);
                    if (group.Access == "personal") selectedGroup = group;
                    var groupNode = new TreeViewItem { Header = group.Title, Tag = group };

                    foreach (var note in TisinaliteDBEntities.GetContext().Notes
                        .Where(n => n.GroupID == group.ID).ToList())
                    {
                        var noteNode = new TreeViewItem { Header = note.Title, Foreground = Brushes.White, Tag = note };
                        groupNode.Items.Add(noteNode);
                    }
                    tvNotes.Items.Add(groupNode);
                }
            }
        }


        public void NewNote()
        {
            if (selectedGroup.ID == 0)
            {
                MessageBox.Show("Сначало выбирете группу, в кторой будете вести заметку");
                return;
            }
            Input input = new Input();
            input.ShowDialog();
            Debug.WriteLine(input.Result);
            if (input.Result)
            {
                openNote = new Notes { Title = input.Entry, GroupID = selectedGroup.ID };
                TisinaliteDBEntities.GetContext().Notes.Add(openNote);
                TisinaliteDBEntities.GetContext().SaveChanges();
                tbEditor.Clear();
                Debug.WriteLine(openNote.Title);
            }

        }
        public void SaveNote()
        {
            if (openNote.ID == 0)
                NewNote();
            try
            {
                openNote.Contents = tbEditor.Text;
                TisinaliteDBEntities.GetContext().SaveChanges();

            }catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
        private void OpenNote(Notes note)
        {
            if (!string.IsNullOrEmpty(tbEditor.Text))
                SaveNote();
            openNote = note;
            tbEditor.Text = note.Contents;
        }

        private void tvNote_Changed(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = (TreeViewItem)tvNotes.SelectedItem;
            if (item == null)
            {
                Debug.WriteLine("Не работает");
                return;
            }
            if (item.Foreground.Equals(Brushes.White))
            {
                Debug.WriteLine("Note");
                OpenNote((Notes)item.Tag);
            }
            else
            {
                Debug.WriteLine("Group");
                Groups group = item.Tag as Groups;
                Debug.WriteLine(group.Title + tvNotes.SelectedValuePath);
                selectedGroup = group;
            }

        }

        private void CanDeleteExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = openNote.ID != 0;
        }

        private void ClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void PasteExecute(object sender, ExecutedRoutedEventArgs e)
        {
            tbEditor.Paste();
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewGroup_Click(object sender, RoutedEventArgs e)
        {
            Input input = new Input();
            input.ShowDialog();
            Debug.WriteLine(input.Result);
            if (input.Result)
            {
                selectedGroup = new Groups { Title = input.Entry, MasterID = user.ID, Access="private"};
                TisinaliteDBEntities.GetContext().Groups.Add(selectedGroup);
                TisinaliteDBEntities.GetContext().SaveChanges();
                Debug.WriteLine(selectedGroup.ID);

                var link = new UsersOfGroups { UserID = user.ID, GroupID = selectedGroup.ID };
                TisinaliteDBEntities.GetContext().UsersOfGroups.Add(link);
                TisinaliteDBEntities.GetContext().SaveChanges();

                Debug.WriteLine(selectedGroup.Title);
            }
            UpdateTreeView();
        }
    }
}
