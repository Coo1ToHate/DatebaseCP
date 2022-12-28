using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    class ListLessonEditWindowViewModel : BaseViewModel
    {
        private string _title;
        private Lesson _lesson;
        private string _name;

        public ListLessonEditWindowViewModel(Lesson lesson)
        {
            _title = "Добавление предмета";
            _lesson = lesson;
            _name = lesson.Name;
            if (_name != null)
            {
                Title = "Редактирование предмета";
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
                    _lesson.Name = Name;

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
