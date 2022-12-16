using System.Collections.ObjectModel;
using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.View;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class ListFormOfEducationWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();

        private string _title;
        private ObservableCollection<FormOfEducation> _formOfEducations;
        private FormOfEducation _selectedFormOfEducation;

        public ListFormOfEducationWindowViewModel()
        {
            _title = "Формы обучения";
            _formOfEducations = ado.GetAllFormOfEducation();
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

        public ObservableCollection<FormOfEducation> FormOfEducations
        {
            get => _formOfEducations;
            set
            {
                _formOfEducations = value;
                OnPropertyChanged();
            }
        }

        public FormOfEducation SelectedFormOfEducation
        {
            get => _selectedFormOfEducation;
            set
            {
                _selectedFormOfEducation = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        #region AddFormOfEducationCommand

        private RelayCommand _addFormOfEducationCommand;

        public RelayCommand AddFormOfEducationCommand
        {
            get
            {
                return _addFormOfEducationCommand ??= new RelayCommand(obj =>
                {
                    FormOfEducation newFormOfEducations = new FormOfEducation();

                    ListFormOfEducationEditWindow listFormOfEducationEditWindow = new ListFormOfEducationEditWindow
                    {
                        DataContext = new ListFormOfEducationEditWindowViewModel(newFormOfEducations),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    listFormOfEducationEditWindow.ShowDialog();

                    if (listFormOfEducationEditWindow.DialogResult.Value)
                    {
                        ado.InsertFormOfEducation(newFormOfEducations);
                        FormOfEducations = ado.GetAllFormOfEducation();
                    }
                });
            }
        }

        #endregion

        #region EditFormOfEducationCommand

        private RelayCommand _editFormOfEducationCommand;

        public RelayCommand EditFormOfEducationCommand
        {
            get
            {
                return _editFormOfEducationCommand ??= new RelayCommand(obj =>
                    {
                        FormOfEducation updFormOfEducations = SelectedFormOfEducation;

                        ListFormOfEducationEditWindow listFormOfEducationEditWindow = new ListFormOfEducationEditWindow
                        {
                            DataContext = new ListFormOfEducationEditWindowViewModel(updFormOfEducations),
                            Owner = obj as Window,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        
                        listFormOfEducationEditWindow.ShowDialog();

                        if (listFormOfEducationEditWindow.DialogResult.Value)
                        {
                            ado.UpdateFormOfEducation(updFormOfEducations);
                            FormOfEducations = ado.GetAllFormOfEducation();
                        }
                    },
                    obj => SelectedFormOfEducation != null);
            }
        }

        #endregion

        //TODO проверку на кол-во с формой обучения
        #region DeleteFormOfEducationCommand

        private RelayCommand _deleteFormOfEducationCommand;

        public RelayCommand DeleteFormOfEducationCommand
        {
            get
            {
                return _deleteFormOfEducationCommand ??= new RelayCommand(obj =>
                {
                    ado.DeleteFormOfEducation(SelectedFormOfEducation);
                    FormOfEducations = ado.GetAllFormOfEducation();
                },
                    obj => SelectedFormOfEducation != null);
            }
        }

        #endregion

        #endregion

    }
}
