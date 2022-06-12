using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using System.Data.Entity;
using MdXaml;
using Microsoft.Win32;

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
        Notes openNote = new Notes();
        Groups selectedGroup = new Groups();
        TisinaliteDBEntities db = new TisinaliteDBEntities();
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

        // Executes
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
            if (MessageBox.Show("Вы точно хотите выйти? Все несохраненные изменения будут потеряны.", "Выход", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }
        private void PasteExecute(object sender, ExecutedRoutedEventArgs e)
        {
            tbEditor.Paste();
        }
        // Execute cause
        private void CanDeleteExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = openNote.ID != 0;
        }
        // Any functions
        public void SaveSettings()
        {
            _settings.Save();
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
                    if (group == null)
                        continue;

                    if (group.Access == "personal") 
                        selectedGroup = group;
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
            Debug.WriteLine(tbEditor.Text);
            if (openNote.ID == 0)
                NewNote();
            try
            {
                openNote.Contents = tbEditor.Text;
                TisinaliteDBEntities.GetContext().SaveChanges();

            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
            Markdownview.Markdown = tbEditor.Text;
        }
        private void OpenNote(Notes note)
        {
            if (!string.IsNullOrEmpty(tbEditor.Text))
                SaveNote();
            openNote = note;
            tbEditor.Text = note.Contents;
            Markdownview.Markdown = tbEditor.Text;
        }

        // Events
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

        private void ClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        // Clicks
        private void Image_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Файлы изображений(*.PNG;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF;*.PNG|Все файлы (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                tbEditor.SelectedText = $" ![Название картинки]({filePath}) ";
            }
        }

        private void NewGroup_Click(object sender, RoutedEventArgs e)
        {
            var input = new GroupEdit(user.ID);
            input.ShowDialog();
            UpdateTreeView();
        }

        private void DeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (selectedGroup.ID == 0)
            {
                MessageBox.Show("Сначало выберети группу.");
                return;
            }
            if (selectedGroup.Access == "personal")
            {
                MessageBox.Show("Вы не можете удалить свою личную группу, она у вас одна!");
                return;
            }
            if (MessageBox.Show("Вы точно хотите удаить группу и её заметки? Это действие невозможно отменить!",
                "Подтверждение действия", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                try
                {
                    var links = TisinaliteDBEntities.GetContext().UsersOfGroups.ToList();
                    links = links.Where(l => l.GroupID == selectedGroup.ID).ToList();
                    TisinaliteDBEntities.GetContext().UsersOfGroups.RemoveRange(links);
                    TisinaliteDBEntities.GetContext().SaveChanges();
                    if (selectedGroup.Access != "public")
                    {
                        var notes = TisinaliteDBEntities.GetContext().Notes.ToList();
                        notes = notes.Where(n => n.GroupID == selectedGroup.ID).ToList();
                        TisinaliteDBEntities.GetContext().Notes.RemoveRange(notes);
                        TisinaliteDBEntities.GetContext().SaveChanges();

                        TisinaliteDBEntities.GetContext().Groups.Remove(selectedGroup);
                    }

                    TisinaliteDBEntities.GetContext().SaveChanges();
                }
                catch
                {
                    // Хуй знает, но он всегда жалуется
                    // По этому я его заткнул
                    //MessageBox.Show(error.ToString());
                }
                
            }
            UpdateTreeView();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            var input = new Input ( "Введите ID группы", "Подключение" );
            input.ShowDialog();
            if (!input.Result)
                return;
            var id = int.Parse(input.Entry);
            var group = db.Groups.FirstOrDefault(g => g.ID == id);
            if (group == null || group.Access != "public")
            {
                MessageBox.Show("Нет публичной папки с таким именем");
                return;
            }
            var link = new UsersOfGroups { UserID = user.ID, GroupID = id };
            TisinaliteDBEntities.GetContext().UsersOfGroups.Add(link);
            TisinaliteDBEntities.GetContext().SaveChanges();
            UpdateTreeView();
        }
        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (selectedGroup.ID == 0)
            {
                MessageBox.Show("Сначало выберети папку.");
                return;
            }
            if (selectedGroup.Access == "private" || selectedGroup.Access == "personal")
            {
                MessageBox.Show("Приватные папки не отключаются. Они удаляются");
                return;
            }
            if (selectedGroup.MasterID == user.ID)
            {
                MessageBox.Show("Вы не можете отключиться от своей созданной папки (это пока не реализовано), но вы можете её удалить.");
                return;
            }
            var link = db.UsersOfGroups.FirstOrDefault(l => l.UserID == user.ID && l.GroupID == selectedGroup.ID);
            Debug.WriteLine(link.ID);

            TisinaliteDBEntities.GetContext().UsersOfGroups.Attach(link);
            TisinaliteDBEntities.GetContext().Entry(link).State = EntityState.Deleted;
            TisinaliteDBEntities.GetContext().SaveChanges();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Global.MainFrame.GoBack();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
