using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    class ListDegreeEditWindowViewModel : BaseViewModel
    {
        private string _title;
        private TeacherDegree _degree;
        private string _name;

        public ListDegreeEditWindowViewModel(TeacherDegree degree)
        {
            _title = "Добавление ученой степени";
            _degree = degree;
            _name = degree.Name;
            if (_name != null)
            {
                Title = "Редактирование ученой степени";
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
                    _degree.Name = Name;

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
