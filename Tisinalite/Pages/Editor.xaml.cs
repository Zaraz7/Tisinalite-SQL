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
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Diagnostics;

namespace Tisinalite.Pages
{
    /// <summary>
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Page
    {
        private Note note = new Note { 
            Title = "Welcome.txt"
        };
        private Microsoft.Win32.OpenFileDialog _openDialog = new Microsoft.Win32.OpenFileDialog();
        private Microsoft.Win32.SaveFileDialog _saveDialog = new Microsoft.Win32.SaveFileDialog();
        bool isDirty = false;
        public Editor()
        {
            InitializeComponent();
            tbEditor.Text = @"Добро пожаловать в Tisinalite!
Это open-source приложение для ведения заметок
Вы можете создать свою, или открыть существующую
А можете открыть любой текствый файл
";
        }

        private void NewExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (isDirty)
            {
                SaveExecute(sender, e);
            }
            tbEditor.Text = "";
        }
        private void OpenExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (isDirty)
            {
                SaveExecute(sender, e);
            }
            if (_openDialog.ShowDialog() == true)
                OpenFile();
        }
        private void SaveExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (_saveDialog.FileName == "")
            {
                SaveAsExecute(sender, e);
                return;
            }
            SaveFile();
        }
        private void SaveAsExecute(object sender, ExecutedRoutedEventArgs e)
        {
            _saveDialog.FileName = note.Title;
            if (_saveDialog.ShowDialog() == true)
            {
                SaveFile();
            }
        }
        private void CloseExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (isDirty)
            {
                SaveExecute(sender, e);
            }
            System.Windows.Application.Current.Shutdown();
        }

        private void SaveFile()
        {
            StreamWriter writer = new StreamWriter(_saveDialog.FileName);
            Debug.WriteLine($"file name: {_saveDialog.FileName}");
            writer.WriteLine(tbEditor.Text);
            Debug.WriteLine($"Content: {tbEditor.Text}");
            writer.Close();
            isDirty = false;
        }
        private void OpenFile()
        {
            StreamReader reader = new StreamReader(_openDialog.FileName);
            tbEditor.Text = reader.ReadToEnd();
            reader.Close();
            _saveDialog.FileName = _openDialog.FileName;
            isDirty = false;
        }

        private void ContentChanged(object sender, TextChangedEventArgs e)
        {
            isDirty = true;
        }
    }
    // Предпологается, что этот класс должен выполнять роль контекста данных
    public class Note
    {
        private string _title;
        public string Title
        {
            get { return _title; } set { _title = value; }
        }
    }
}
