using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Fomin04
{
    class UsersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _users;
        private readonly Action _showInputViewAction;
        private readonly DataGrid _dataGrid;

        private RelayCommand _showInputViewCommand;
        private RelayCommand _deleteRowCommand;

        public ObservableCollection<Person> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        internal UsersViewModel(DataGrid dataGrid, Action showInputViewAction)
        {
            _users = new ObservableCollection<Person>(AgeCalcAdapter.Users);
            _dataGrid = dataGrid;
            _dataGrid.CellEditEnding += myDG_CellEditEnding;
            _showInputViewAction = showInputViewAction;
        }

        internal void UpdateUsers()
        {
            if (Users.Count != AgeCalcAdapter.Users.Count)
                Users.Add(StationManager.CurrentPerson);
        }

        
        public RelayCommand ShowInputViewCommand
        {
            get { return _showInputViewCommand ?? (_showInputViewCommand = new RelayCommand(ShowInputViewImpl)); }
        }

        public RelayCommand DeleteRowCommand
        {
            get { return _deleteRowCommand ?? (_deleteRowCommand = new RelayCommand(DeleteRowImpl)); }
        }

        private void ShowInputViewImpl(object o)
        {
            _showInputViewAction.Invoke();
        }
    
        private void DeleteRowImpl(object o)
        {
            Person item = (Person)_dataGrid.SelectedItems[0];
            if (item != null)
            {
                AgeCalcAdapter.Users.Remove(item);
                Users.Remove(item);
            }
        }

        void myDG_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                if (e.Column is DataGridBoundColumn column)
                {
                    var bindingPath = (column.Binding as Binding)?.Path.Path;
                    int rowIndex = e.Row.GetIndex();
                    var el = e.EditingElement as TextBox;
                    try
                    {
                        if (el != null)
                        {
                            string value = el.Text;
                            Person person = (Person)_dataGrid.Items.GetItemAt(rowIndex);
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
                                    throw new Exception("No such property exists");
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show($"Error!\n{exception.Message}");
                        e.Cancel = true;
                    }
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
