using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class ListFormOfEducationEditWindowViewModel : BaseViewModel
    {
        private string _title;
        private FormOfEducation _formOfEducation;
        private string _name;

        public ListFormOfEducationEditWindowViewModel(FormOfEducation formOfEducation)
        {
            _title = "Добавление формы обучения";
            _formOfEducation = formOfEducation;
            _name = formOfEducation.Name;
            if (_name != null)
            {
                Title = "Редактирование формы обучения";
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
                    _formOfEducation.Name = Name;

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
