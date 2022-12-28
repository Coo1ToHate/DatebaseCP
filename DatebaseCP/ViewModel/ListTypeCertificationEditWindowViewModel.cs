using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    class ListTypeCertificationEditWindowViewModel : BaseViewModel
    {
        private string _title;
        private TypeCertification _typeCertification;
        private string _name;

        public ListTypeCertificationEditWindowViewModel(TypeCertification typeCertification)
        {
            _title = "Добавление вида аттестации";
            _typeCertification = typeCertification;
            _name = typeCertification.Name;
            if (_name != null)
            {
                Title = "Редактирование вида аттестации";
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

        #region SaveCommand

        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand(obj =>
                {
                    _typeCertification.Name = Name;

                    Window window = obj as Window;
                    window.DialogResult = true;
                    window.Close();
                }, obj => !string.IsNullOrEmpty(Name));
            }
        }

        #endregion

        #endregion

    }
}
