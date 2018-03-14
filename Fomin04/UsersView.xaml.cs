using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            UsersDataGrid.CellEditEnding += myDG_CellEditEnding;
        }

        internal void UpdateUsers()
        {
            ((UsersViewModel) DataContext).Users.Add(StationManager.CurrentPerson);
        }

        private void DeleteRow(object sender, EventArgs e)
        {
            Person item = (Person) UsersDataGrid.SelectedItems[0];
            if (item != null)
            {
                AgeCalcAdapter.Users.Remove(item);
                ((UsersViewModel) DataContext).Users.Remove(item);
            }
        }

        private void ShowInputView(object sender, EventArgs e)
        {
            _showInputViewAction.Invoke();
        }

        void myDG_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var bindingPath = (column.Binding as Binding).Path.Path;
                        int rowIndex = e.Row.GetIndex();
                        var el = e.EditingElement as TextBox;
                    try
                    {
                        Person person = (Person) UsersDataGrid.Items.GetItemAt(rowIndex);
                        string value = el.Text;
                        switch (bindingPath)
                        {
                            case "FirstName":
                                person.FirstName = value;
                                break;
                            case "LastName":
                                person.LastName = value;
                                break;
                            case "Email":
                                person.Email = value;
                                break;
                            case "BirthDate":
                                person.BirthDateString = value;
                                break;
                            default:

                                break;
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show($"Error!\n{exception.Message}");
                        e.Cancel = true;
                    }

                        // rowIndex has the row index
                        // bindingPath has the column's binding
                        // el.Text has the new, user-entered value
                }
            }
        }
        
    }
}
