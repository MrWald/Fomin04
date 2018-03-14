using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Fomin04
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _firstName = "";
        private string _lastName = "";
        private string _email = "";
        private DateTime _date = DateTime.Today;
        private string _birthday;
        private string _adult;
        private string _wZodiac;
        private string _cZodiac;
        private bool _canExecute;

        private RelayCommand _calculateAgeCommand;

        private RelayCommand _returnToInputCommand;

        private readonly Action _closeAction;

        private readonly Action _returnAction;

        private readonly Action<bool> _showLoaderAction;
        
        internal MainWindowViewModel(Action closeAction, Action returnAction, Action<bool> showLoader)
        {
            _returnAction = returnAction;
            _closeAction = closeAction;
            _showLoaderAction = showLoader;
            CanExecute = false;
        }

        #region Properties
        public bool CanExecute
        {
            get { return _canExecute; }
            private set
            {
                _canExecute = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                CanExecute = CheckIfFilled();
                OnPropertyChanged();
            }
        }

        public string DateString
        {
            get { return _date.ToShortDateString(); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                CanExecute = CheckIfFilled();
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                CanExecute = CheckIfFilled();
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                CanExecute = CheckIfFilled();
                OnPropertyChanged();
            }
        }

        public string Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public string Adult
        {
            get { return _adult; }
            set
            {
                _adult = value;
                OnPropertyChanged();
            }
        }

        public string WesternZodiac
        {
            get { return _wZodiac; }
            private set
            {
                _wZodiac = value;
                OnPropertyChanged();
            }
        }

        public string ChineseZodiac
        {
            get { return _cZodiac; }
            private set
            {
                _cZodiac = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public RelayCommand CalculateAgeCommand
        {
            get { return _calculateAgeCommand ?? (_calculateAgeCommand = new RelayCommand(AgeCalcImpl)); }
        }

        public RelayCommand ReturnToInputCommand
        {
            get { return _returnToInputCommand ?? (_returnToInputCommand = new RelayCommand(RetToInpImpl)); }
        }

        private async void AgeCalcImpl(object o)
        {
            _showLoaderAction.Invoke(true);
            ClearValues();
            try
            {
                await Task.Run(() =>
                {
                        StationManager.CurrentPerson = AgeCalcAdapter.CreateUser(_firstName, _lastName, _email, _date);
                        Thread.Sleep(500);
                });
                if (DateTime.Today.DayOfYear == _date.DayOfYear)
                    MessageBox.Show($"Happy Birthday, {FirstName}!");
                Birthday = $"{StationManager.CurrentPerson.IsBirthday}";
                Adult = $"{StationManager.CurrentPerson.IsAdult}";
                WesternZodiac = StationManager.CurrentPerson.SunSign;
                ChineseZodiac = StationManager.CurrentPerson.ChineseSign;
                _closeAction.Invoke();
                CanExecute = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ClearInputValues();
            _showLoaderAction.Invoke(false);
        }

        private async void RetToInpImpl(object o)
        {
            await Task.Run(() =>
            {
                ClearInputValues();
                ClearValues();
            });
            _returnAction.Invoke();
        }

        private void ClearInputValues()
        {
            Date = DateTime.Today;
            FirstName = "";
            LastName = "";
            Email = "";
        }

        private void ClearValues()
        {
            CanExecute = false;
            Birthday = "";
            Adult = "";
            WesternZodiac = "";
            ChineseZodiac = "";
        }

        private bool CheckIfFilled()
        {
            return _firstName != "" && _lastName != "" && _email != "";
        }

        #region Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}