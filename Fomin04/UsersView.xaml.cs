using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Fomin04
{
    /// <summary>
    /// Interaction logic for UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        private readonly Action _showInputViewAction;

        public UsersView(Action showInputViewAction)
        {
            InitializeComponent();
            DataContext = new UsersViewModel();
            _showInputViewAction = showInputViewAction;
        }

        internal void UpdateUsers()
        {
            ((UsersViewModel)DataContext).Users.Add(StationManager.CurrentPerson);
        }

        private void DeleteRow(object sender, EventArgs e)
        {
            Person item = (Person)UsersDataGrid.SelectedItems[0];
            if (item != null)
            {
                AgeCalcAdapter.Users.Remove(item);
                ((UsersViewModel)DataContext).Users.Remove(item);
            }
        }

        private void ShowInputView(object sender, EventArgs e)
        {
            _showInputViewAction.Invoke();
        }
    }
}
