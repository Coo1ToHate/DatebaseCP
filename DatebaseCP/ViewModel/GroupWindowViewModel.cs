using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class GroupWindowViewModel : BaseViewModel
    {
        private string _title;
        private Group group;
        private string _name;
        private IEnumerable<Speciality> _speciality;
        private Speciality _selectedSpeciality;
        private IEnumerable<FormOfEducation> _formOfEducations;
        private FormOfEducation _selectedFormOfEducation;

        public GroupWindowViewModel(Group group)
        {
            this.group = group;
            Title = "Добавление группы";
            Name = group.Name;
            ADO ado = new ADO();
            Specialities = ado.GetAllSpecialities();
            FormOfEducations = ado.GetAllFormOfEducation();
            if (Name != null)
            {
                Title = $"Редактирование группы - {Name}";
                SelectedSpeciality = Specialities.First(s => s.Id == group.SpecialityID);
                SelectedFormOfEducations = FormOfEducations.First(f => f.Id == group.FormOfEducationID);
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

        public IEnumerable<Speciality> Specialities
        {
            get => _speciality;
            set
            {
                _speciality = value;
                OnPropertyChanged();
            }
        }

        public Speciality SelectedSpeciality
        {
            get => _selectedSpeciality;
            set
            {
                _selectedSpeciality = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<FormOfEducation> FormOfEducations
        {
            get => _formOfEducations;
            set
            {
                _formOfEducations = value;
                OnPropertyChanged();
            }
        }

        public FormOfEducation SelectedFormOfEducations
        {
            get => _selectedFormOfEducation;
            set
            {
                _selectedFormOfEducation = value;
                OnPropertyChanged();
            }
        }

        #region Command
        
        #region saveCommand

        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(obj =>
                {
                    group.Name = Name;
                    group.SpecialityID = SelectedSpeciality.Id;
                    group.FormOfEducationID = SelectedFormOfEducations.Id;

                    Window window = obj as Window;
                    window.DialogResult = true;
                    window.Close();
                }, obj => !string.IsNullOrEmpty(Name) && SelectedSpeciality != null && SelectedFormOfEducations != null));
            }
        }

        #endregion
        
        #endregion
    }
}
