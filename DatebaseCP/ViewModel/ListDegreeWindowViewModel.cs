using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.ViewModel.Base;
using System.Collections.ObjectModel;
using DatebaseCP.Command;
using DatebaseCP.View;
using System.Windows;
using System.Windows.Media;

namespace DatebaseCP.ViewModel
{
    class ListDegreeWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();

        private string _title;
        private ObservableCollection<TeacherDegree> _degrees;
        private TeacherDegree _selectedDegree;

        public ListDegreeWindowViewModel()
        {
            _title = "Ученые степени";
            _degrees = ado.GetAllTeacherDegree();
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

        public ObservableCollection<TeacherDegree> Degrees
        {
            get => _degrees;
            set
            {
                _degrees = value;
                OnPropertyChanged();
            }
        }

        public TeacherDegree SelectedDegree
        {
            get => _selectedDegree;
            set
            {
                _selectedDegree = value;
                OnPropertyChanged();
            }
        }

        #region Command

        #region AddDegreeCommand

        private RelayCommand _addDegreeCommand;

        public RelayCommand AddDegreeCommand
        {
            get
            {
                return _addDegreeCommand ??= new RelayCommand(obj =>
                {
                    TeacherDegree newDegree = new TeacherDegree();

                    ListDegreeEditWindow listDegreeEditWindow = new ListDegreeEditWindow
                    {
                        DataContext = new ListDegreeEditWindowViewModel(newDegree),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    listDegreeEditWindow.ShowDialog();

                    if (listDegreeEditWindow.DialogResult.Value)
                    {
                        ado.InsertTeacherDegree(newDegree);
                        Degrees = ado.GetAllTeacherDegree();
                    }
                });
            }
        }

        #endregion

        #region EditDegreeCommand

        private RelayCommand _editDegreeCommand;

        public RelayCommand EditDegreeCommand
        {
            get
            {
                return _editDegreeCommand ??= new RelayCommand(obj =>
                {
                    TeacherDegree updDegree = SelectedDegree;

                    ListDegreeEditWindow listDegreeEditWindow = new ListDegreeEditWindow
                    {
                        DataContext = new ListDegreeEditWindowViewModel(updDegree),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    listDegreeEditWindow.ShowDialog();

                    if (listDegreeEditWindow.DialogResult.Value)
                    {
                        ado.UpdateTeacherDegree(updDegree);
                        Degrees = ado.GetAllTeacherDegree();
                    }

                }, obj => SelectedDegree != null);
            }
        }

        #endregion

        #region DeleteDegreeCommand

        private RelayCommand _deleteDegreeCommand;

        public RelayCommand DeleteDegreeCommand
        {
            get
            {
                return _deleteDegreeCommand ??= new RelayCommand(obj =>
                {
                    ado.DeleteTeacherDegree(SelectedDegree);
                    Degrees = ado.GetAllTeacherDegree();
                }, obj => SelectedDegree != null && ado.CountTeacherWithDegree(SelectedDegree.Id) == 0);
            }
        }

        #endregion

        #endregion

    }
}
