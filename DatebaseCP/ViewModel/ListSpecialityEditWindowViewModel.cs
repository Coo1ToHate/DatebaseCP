using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class ListSpecialityEditWindowViewModel : BaseViewModel
    {
        private string _title;
        private Speciality _speciality;
        private string _name;

        public ListSpecialityEditWindowViewModel(Speciality speciality)
        {
            _title = "Добавление специальности";
            _speciality = speciality;
            _name = speciality.Name;
            if (_name != null)
            {
                Title = "Редактирование специальности";
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        #region Command

        #region saveCommand

        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(obj =>
                {
                    _speciality.Name = Name;

                    Window window = obj as Window;
                    window.DialogResult = true;
                    window.Close();
                }, obj => !string.IsNullOrEmpty(Name)));
            }
        }

        #endregion

        #endregion
    }
}
