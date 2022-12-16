using System.Collections.ObjectModel;
using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.View;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class ListSpecialityWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();

        private string _title;
        private ObservableCollection<Speciality> _specialities;
        private Speciality _selectSpeciality;

        public ListSpecialityWindowViewModel()
        {
            _title = "Специальности";
            _specialities = ado.GetAllSpecialities();
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

        public ObservableCollection<Speciality> Specialities
        {
            get => _specialities;
            set
            {
                _specialities = value;
                OnPropertyChanged();
            }
        }

        public Speciality SelectSpeciality
        {
            get => _selectSpeciality;
            set
            {
                _selectSpeciality = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        #region AddSpecialityCommand

        private RelayCommand _addSpecialityCommand;

        public RelayCommand AddSpecialityCommand
        {
            get
            {
                return _addSpecialityCommand ??= new RelayCommand(obj =>
                {
                    Speciality newSpeciality = new Speciality();

                    ListSpecialityEditWindow listSpecialityEditWindow = new ListSpecialityEditWindow
                    {
                        DataContext = new ListSpecialityEditWindowViewModel(newSpeciality),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    listSpecialityEditWindow.ShowDialog();

                    if (listSpecialityEditWindow.DialogResult.Value)
                    {
                        ado.InsertSpeciality(newSpeciality);
                        Specialities = ado.GetAllSpecialities();
                    }
                });
            }
        }

        #endregion

        #region EditSpecialityCommand

        private RelayCommand _editSpecialityCommand;

        public RelayCommand EditSpecialityCommand
        {
            get
            {
                return _editSpecialityCommand ??= new RelayCommand(obj =>
                    {
                        Speciality updSpeciality = SelectSpeciality;

                        ListSpecialityEditWindow listSpecialityEditWindow = new ListSpecialityEditWindow
                        {
                            DataContext = new ListSpecialityEditWindowViewModel(updSpeciality),
                            Owner = obj as Window,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        
                        listSpecialityEditWindow.ShowDialog();

                        if (listSpecialityEditWindow.DialogResult.Value)
                        {
                            ado.UpdateSpeciality(updSpeciality);
                            Specialities = ado.GetAllSpecialities();
                        }
                    },
                    obj => SelectSpeciality != null);
            }
        }

        #endregion

        #region DeleteSpecialityCommand

        private RelayCommand _deleteSpecialityCommand;

        public RelayCommand DeleteSpecialityCommand
        {
            get
            {
                return _deleteSpecialityCommand ??= new RelayCommand(obj =>
                {
                    ado.DeleteSpeciality(SelectSpeciality);
                    Specialities = ado.GetAllSpecialities();
                },
                    obj => SelectSpeciality != null);
            }
        }

        #endregion

        #endregion
    }
}
