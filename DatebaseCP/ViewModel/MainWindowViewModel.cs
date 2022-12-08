using System;
using System.Collections.ObjectModel;
using System.Linq;
using DatebaseCP.Models;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private string title;
        private string statusBarMsg;
        private ObservableCollection<University> universities;
        private University selectedUniversity;
        private ObservableCollection<Group> groups;
        private Group selectedGroup;
        private ObservableCollection<Student> students;
        private Student selectedStudent;

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
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public string StatusBarMsg
        {
            get => statusBarMsg;
            set
            {
                statusBarMsg = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<University> Universities
        {
            get => universities;
            set
            {
                universities = value;
                OnPropertyChanged();
            }
        }

        public University SelectedUniversity
        {
            get => selectedUniversity;
            set
            {
                selectedUniversity = value;
                Groups = selectedUniversity.Groups;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Group> Groups
        {
            get => groups;
            set
            {
                groups = value;
                SelectedGroup = groups.First();
                OnPropertyChanged();
            }
        }

        public Group SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                Students = selectedGroup.Students;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Student> Students
        {
            get => students;
            set
            {
                students = value;
                //SelectedStudent = students.First();
                OnPropertyChanged();
            }
        }

        public Student SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                OnPropertyChanged();
            }
        }

        #region commands

        

        #endregion
        
    }
}
