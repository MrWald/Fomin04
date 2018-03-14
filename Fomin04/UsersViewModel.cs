using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Fomin04
{
    class UsersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _users;

        public ObservableCollection<Person> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        internal UsersViewModel()
        {
            _users = new ObservableCollection<Person>(AgeCalcAdapter.Users);
            
        }


        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
