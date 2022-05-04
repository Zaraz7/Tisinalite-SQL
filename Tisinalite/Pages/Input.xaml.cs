﻿using System;
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

namespace Tisinalite.Pages
{
    /// <summary>
    /// Логика взаимодействия для Input.xaml
    /// </summary>
    public partial class Input : Window
    {
        public string Entry;
        public Input()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Entry = inputEntry.Text;
            this.Close();
        }
    }
}