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


namespace Tisinalite
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Settings _settings = Settings.GetSettings();
        private string notePath;
        public MainWindow()
        {
            InitializeComponent();
            if (_settings.OpenNote != null)
            {
                notePath = Global.GetNoteFile(_settings.OpenNote);
                OpenFile(_settings.OpenNote);
            }
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
            if (notePath != null)
                SaveFile();
            notePath = null;
            tbEditor.Text = "";
            SaveFile();
            UpdateTreeView();
        }
        private void SaveExecute(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFile();
            SaveSettings();
            UpdateTreeView();
        }
        private void DeleteExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Уверены, что хоте удалить заметку?", "Подтверждение действия",
                MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                File.Delete(notePath);
                notePath = null;
                UpdateTreeView();
                tbEditor.Clear();
                _settings.OpenNote = null;
            }
        }
        private void CloseExecute(object sender, ExecutedRoutedEventArgs e)
        {
            SaveExecute(sender, e);
            Application.Current.Shutdown();
        }
        private void UpdateTreeView()
        {
            tvNotes.Items.Clear();
            var rootDirectoryInfo = new DirectoryInfo(Global.NotesDir);
            tvNotes.Items.Add(CreateDirNode(rootDirectoryInfo));
        }
        private static TreeViewItem CreateDirNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeViewItem { Header = directoryInfo.Name };
            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Items.Add(new TreeViewItem { Header = file.Name });

            return directoryNode;
        }


        public void SaveFile()
        {
            if (notePath == null && !String.IsNullOrEmpty(tbEditor.Text))
            {
                Pages.Input input = new Pages.Input();
                input.Title = "Введите имя фала";
                input.ShowDialog();
                try
                {
                    string path = Path.Combine(Global.NotesDir, input.Entry);

                    if (!input.Result) return;
                    notePath = path;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            if (notePath != null)
            {
                using (StreamWriter writer = new StreamWriter(notePath))
                {
                    writer.WriteLine(tbEditor.Text);
                    writer.Close();
                }
            }
        }
        private void OpenFile(string note)
        {
            string _notePath = Global.GetNoteFile(note);
            if (notePath != null && _notePath != notePath)
                SaveFile();
            try
            {
                using (StreamReader reader = new StreamReader(_notePath))
                {
                    string noteText = reader.ReadToEnd();
                    tbEditor.Text = noteText;
                    reader.Close();
                    notePath = _notePath;
                    _settings.OpenNote = note;
                }
            }
            catch
            {
            }
        }

        private void tvNote_Changed(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = (TreeViewItem)tvNotes.SelectedItem;
            Debug.WriteLine(item);
            if (item != null)
            {
                string note = item.Header.ToString();
                OpenFile(note);
            }
        }

        private void CanDeleteExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = File.Exists(notePath);
        }

        private void ClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveFile();
            SaveSettings();
        }
    }
}
