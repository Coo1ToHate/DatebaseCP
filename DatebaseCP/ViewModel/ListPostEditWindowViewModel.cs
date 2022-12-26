using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.ViewModel.Base;
using System.Windows;

namespace DatebaseCP.ViewModel
{
    internal class ListPostEditWindowViewModel : BaseViewModel
    {
        private string _title;
        private Post _post;
        private string _name;

        public ListPostEditWindowViewModel(Post post)
        {
            _title = "Добавление должности";
            _post = post;
            _name = post.Name;
            if (_name != null)
            {
                Title = "Редактирование должности";
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
                    _post.Name = Name;

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
