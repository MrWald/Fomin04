using System;
using System.Windows.Controls;

namespace Fomin04
{
    /// <summary>
    /// Interaction logic for UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        public UsersView(Action showInputViewAction)
        {
            InitializeComponent();
            DataContext = new UsersViewModel(UsersDataGrid, showInputViewAction);
        }
    }
}
