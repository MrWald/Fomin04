using System;
using System.ComponentModel;
using System.Windows;

namespace Fomin04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadCreationUserWindow(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().Show();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            AgeCalcAdapter.SaveData();
            base.OnClosing(e);
        }
    }
}
