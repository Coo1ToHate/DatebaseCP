using System;
using System.Collections.ObjectModel;
using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class ReportGroupWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();

        private string _title;

        private Group _group;
        private string _name;
        private string _speciality;
        private string _formOfEducations;
        private int _countStudents;
        private ObservableCollection<Student> _students;

        public ReportGroupWindowViewModel(Group group)
        {
            _title = $"Отчет о группе - {group.Name}";
            _group = group;
            _name = group.Name;
            _speciality = group.Speciality.Name;
            _formOfEducations = group.FormOfEducation.Name;
            _countStudents = ado.CountStudentsInGroup(group.Id);
            _students = ado.GetStudentsInGroup(group);
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

        public Group Group
        {
            get => _group;
            set
            {
                _group = value;
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

        public string Speciality
        {
            get => _speciality;
            set
            {
                _speciality = value;
                OnPropertyChanged();
            }
        }

        public string FormOfEducations
        {
            get => _formOfEducations;
            set
            {
                _formOfEducations = value;
                OnPropertyChanged();
            }
        }

        public int CountStudents
        {
            get => _countStudents;
            set
            {
                _countStudents = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Student> Students
        {
            get => _students;
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }
    }
}
