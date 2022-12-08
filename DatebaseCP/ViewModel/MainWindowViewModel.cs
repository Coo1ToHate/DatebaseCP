using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private string _title;
        private string _statusBarMsg;
        private ObservableCollection<University> _universities;
        private University _selectedUniversity;
        private ObservableCollection<Group> _groups;
        private Group _selectedGroup;
        private ObservableCollection<Student> _students;
        private Student _selectedStudent;

        public MainWindowViewModel()
        {
            Title = "Универ";
            StatusBarMsg = "Готов!";

            #region test

            University university1 = new("ТУСУР");

            for (int i = 0; i < 3; i++)
            {
                university1.Groups.Add(new Group(i, $"Группа_{i+1}"));
                for (int j = 0; j < 5; j++)
                {
                    university1.Groups[i].Students.Add(new Student(i+j, $"Фамилия_{i+j+1}", $"Имя_{i+j+1}", $"Отчество_{i+j+1}", DateTime.Now.AddYears(-18-i).AddMonths(-j).AddDays(-new Random().Next(1,28))));
                }
            }

            #endregion

            Universities = new ObservableCollection<University> { university1 };
            SelectedUniversity = Universities.First();
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

        public string StatusBarMsg
        {
            get => _statusBarMsg;
            set
            {
                _statusBarMsg = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<University> Universities
        {
            get => _universities;
            set
            {
                _universities = value;
                OnPropertyChanged();
            }
        }

        public University SelectedUniversity
        {
            get => _selectedUniversity;
            set
            {
                _selectedUniversity = value;
                Groups = _selectedUniversity.Groups;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Group> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                SelectedGroup = _groups.First();
                OnPropertyChanged();
            }
        }

        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                Students = _selectedGroup.Students;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Student> Students
        {
            get => _students;
            set
            {
                _students = value;
                //SelectedStudent = students.First();
                OnPropertyChanged();
            }
        }

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        private RelayCommand _closeAppCommand;

        public RelayCommand CloseAppCommand
        {
            get
            {
                return _closeAppCommand ??= new RelayCommand(obj =>
                {
                    Application.Current.Shutdown();
                });
            }
        }

        #endregion

    }
}
