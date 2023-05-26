using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.ViewModel.Base;
using System.Collections.ObjectModel;
using DatebaseCP.Command;
using DatebaseCP.View;
using System.Windows;

namespace DatebaseCP.ViewModel
{
    class ListDegreeWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();

        private string _title;
        private ObservableCollection<Degree> _degrees;
        private Degree _selectedDegree;

        public ListDegreeWindowViewModel()
        {
            _title = "Ученые степени";
            _degrees = ado.GetAllDegrees();
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

        public ObservableCollection<Degree> Degrees
        {
            get => _degrees;
            set
            {
                _degrees = value;
                OnPropertyChanged();
            }
        }

        public Degree SelectedDegree
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
                    Degree newDegree = new Degree();

                    ListDegreeEditWindow listDegreeEditWindow = new ListDegreeEditWindow
                    {
                        DataContext = new ListDegreeEditWindowViewModel(newDegree),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    listDegreeEditWindow.ShowDialog();

                    if (listDegreeEditWindow.DialogResult.Value)
                    {
                        ado.InsertDegree(newDegree);
                        Degrees = ado.GetAllDegrees();
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
                    Degree updDegree = SelectedDegree;

                    ListDegreeEditWindow listDegreeEditWindow = new ListDegreeEditWindow
                    {
                        DataContext = new ListDegreeEditWindowViewModel(updDegree),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    listDegreeEditWindow.ShowDialog();

                    if (listDegreeEditWindow.DialogResult.Value)
                    {
                        ado.UpdateDegree(updDegree);
                        Degrees = ado.GetAllDegrees();
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
                    ado.DeleteDegree(SelectedDegree);
                    Degrees = ado.GetAllDegrees();
                }, obj => SelectedDegree != null && ado.CountTeacherWithDegree(SelectedDegree.Id) == 0);
            }
        }

        #endregion

        #endregion

    }
}
