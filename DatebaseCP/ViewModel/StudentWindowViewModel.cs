using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class StudentWindowViewModel : BaseViewModel
    {
        private string _title;
        private Student _student;
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private DateTime _birthDate;
        private IEnumerable<Group> _groups;
        private Group _selectedGroup;
        private string _recordBook;

        public StudentWindowViewModel(Student student)
        {
            ADO ado = new ADO();
            _groups = ado.GetAllGroup();
            _student = student;
            _selectedGroup = Groups.FirstOrDefault(g => g.Id == student.GroupId);
            Title = "Добавление студента";
            _firstName = student.FirstName;
            _lastName = student.LastName;
            _middleName = student.MiddleName;
            _birthDate = DateTime.Now.AddYears(-20);
            _recordBook = student.RecordBook;

            if (FirstName != null)
            {
                Title = $"Редактирование студента - {FirstName} {LastName}";
                _birthDate = student.BirthDate;
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

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged();
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Group> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }

        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged();
            }
        }

        public string RecordBook
        {
            get => _recordBook;
            set
            {
                _recordBook = value;
                OnPropertyChanged();
            }
        }

        #region SaveCommand

        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand(obj =>
                {
                    _student.LastName = LastName;
                    _student.FirstName = FirstName;
                    _student.MiddleName = MiddleName;
                    _student.BirthDate = BirthDate;
                    _student.GroupId = SelectedGroup.Id;
                    _student.RecordBook = RecordBook;
                    
                    Window window = obj as Window;
                    window.DialogResult = true;
                    window.Close();
                },
                    obj=> !string.IsNullOrEmpty(LastName) || !string.IsNullOrEmpty(FirstName) || !string.IsNullOrEmpty(RecordBook));
            }
        }

        #endregion
    }
}
