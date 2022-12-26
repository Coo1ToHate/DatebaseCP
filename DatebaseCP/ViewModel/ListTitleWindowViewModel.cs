using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.View;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    class ListTitleWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();

        private string _title;
        private ObservableCollection<TeacherTitle> _teacherTitles;
        private TeacherTitle _selectedTeacherTitle;

        public ListTitleWindowViewModel()
        {
            _title = "Звания";
            _teacherTitles = ado.GetAllTeacherTitle();
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

        public ObservableCollection<TeacherTitle> TeacherTitles
        {
            get => _teacherTitles;
            set
            {
                _teacherTitles = value;
                OnPropertyChanged();
            }
        }

        public TeacherTitle SelectedTeacherTitle
        {
            get => _selectedTeacherTitle;
            set
            {
                _selectedTeacherTitle = value;
                OnPropertyChanged();
            }
        }

        #region Command

        #region AddTitleCommand

        private RelayCommand _addTitleCommand;

        public RelayCommand AddTitleCommand
        {
            get
            {
                return _addTitleCommand ??= new RelayCommand(obj =>
                {
                    TeacherTitle newTitle = new TeacherTitle();

                    ListTitleEditWindow listTitleEditWindow = new ListTitleEditWindow
                    {
                        DataContext = new ListTitleEditWindowViewModel(newTitle),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };


                    listTitleEditWindow.ShowDialog();

                    if (listTitleEditWindow.DialogResult.Value)
                    {
                        ado.InsertTeacherTitle(newTitle);
                        TeacherTitles = ado.GetAllTeacherTitle();
                    }
                });
            }
        }

        #endregion

        #region EditTitleCommand

        private RelayCommand _editTitleCommand;

        public RelayCommand EditTitleCommand
        {
            get
            {
                return _editTitleCommand ??= new RelayCommand(obj =>
                {
                    TeacherTitle editTitle = SelectedTeacherTitle;

                    ListTitleEditWindow listTitleEditWindow = new ListTitleEditWindow
                    {
                        DataContext = new ListTitleEditWindowViewModel(editTitle),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };


                    listTitleEditWindow.ShowDialog();

                    if (listTitleEditWindow.DialogResult.Value)
                    {
                        ado.UpdateTeacherTitle(editTitle);
                        TeacherTitles = ado.GetAllTeacherTitle();
                    }
                }, obj => SelectedTeacherTitle != null);
            }
        }

        #endregion

        #region DeleteTitleCommand

        private RelayCommand _deleteTitleCommand;

        public RelayCommand DeleteTitleCommand
        {
            get
            {
                return _deleteTitleCommand ??= new RelayCommand(obj =>
                {
                    ado.DeleteTeacherTitle(SelectedTeacherTitle);
                    TeacherTitles = ado.GetAllTeacherTitle();
                }, obj => SelectedTeacherTitle != null && ado.CountTeacherWithTitle(SelectedTeacherTitle.Id) == 0);
            }
        }

        #endregion


        #endregion
    }
}
