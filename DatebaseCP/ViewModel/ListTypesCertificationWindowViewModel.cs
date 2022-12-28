using DatebaseCP.Models;
using DatebaseCP.Utils;
using System.Collections.ObjectModel;
using DatebaseCP.Command;
using DatebaseCP.View;
using DatebaseCP.ViewModel.Base;
using System.Windows;

namespace DatebaseCP.ViewModel
{
    class ListTypesCertificationWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();

        private string _title;
        private ObservableCollection<TypeCertification> _typeCertifications;
        private TypeCertification _selelectedTypeCertification;

        public ListTypesCertificationWindowViewModel()
        {
            _title = "Виды аттестации";
            _typeCertifications = ado.GetAllTypeCertifications();
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

        public ObservableCollection<TypeCertification> TypeCertifications
        {
            get => _typeCertifications;
            set
            {
                _typeCertifications = value;
                OnPropertyChanged();
            }
        }

        public TypeCertification SelelectedTypeCertification
        {
            get => _selelectedTypeCertification;
            set
            {
                _selelectedTypeCertification = value;
                OnPropertyChanged();
            }
        }

        #region Command

        #region AddTypeCertificationCommand

        private RelayCommand _addTypeCertificationCommand;

        public RelayCommand AddTypeCertificationCommand
        {
            get
            {
                return _addTypeCertificationCommand ??= new RelayCommand(obj =>
                {
                    TypeCertification newTypeCertification = new TypeCertification();

                    ListTypeCertificationEditWindow listTypeCertificationEditWindow =
                        new ListTypeCertificationEditWindow()
                        {
                            DataContext = new ListTypeCertificationEditWindowViewModel(newTypeCertification),
                            Owner = obj as Window,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };

                    listTypeCertificationEditWindow.ShowDialog();

                    if (listTypeCertificationEditWindow.DialogResult.Value)
                    {
                        ado.InsertTypeCertification(newTypeCertification);
                        TypeCertifications = ado.GetAllTypeCertifications();
                    }
                });
            }
        }

        #endregion

        #region EditTypeCertificationCommand

        private RelayCommand _editTypeCertificationCommand;

        public RelayCommand EditTypeCertificationCommand
        {
            get
            {
                return _editTypeCertificationCommand ??= new RelayCommand(obj =>
                {
                    TypeCertification updTypeCertification = SelelectedTypeCertification;

                    ListTypeCertificationEditWindow listTypeCertificationEditWindow =
                        new ListTypeCertificationEditWindow()
                        {
                            DataContext = new ListTypeCertificationEditWindowViewModel(updTypeCertification),
                            Owner = obj as Window,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };

                    listTypeCertificationEditWindow.ShowDialog();

                    if (listTypeCertificationEditWindow.DialogResult.Value)
                    {
                        ado.UpdateTypeCertification(updTypeCertification);
                        TypeCertifications = ado.GetAllTypeCertifications();
                    }

                }, obj => SelelectedTypeCertification != null);
            }
        }

        #endregion

        #region DeleteTypeCertificationCommand

        private RelayCommand _deleteTypeCertificationCommand;

        public RelayCommand DeleteTypeCertificationCommand
        {
            get
            {
                return _deleteTypeCertificationCommand ??= new RelayCommand(obj =>
                {
                    ado.DeleteTypeCertification(SelelectedTypeCertification);
                    TypeCertifications = ado.GetAllTypeCertifications();
                }, obj => SelelectedTypeCertification != null && ado.CountTypeCertification(SelelectedTypeCertification.Id) == 0);
            }
        }

        #endregion

        #endregion

    }
}
