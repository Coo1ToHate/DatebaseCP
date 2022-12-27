﻿using DatebaseCP.Models;
using DatebaseCP.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DatebaseCP.Command;
using DatebaseCP.Utils;
using System.Windows;

namespace DatebaseCP.ViewModel
{
    internal class TeacherWindowViewModel : BaseViewModel
    {
        private string _title;
        private Teacher _teacher;
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private DateTime _birthDate;
        private IEnumerable<TeacherTitle> _teacherTitles;
        private TeacherTitle _selectedTeacherTitle;
        private IEnumerable<TeacherDegree> _teacherDegrees;
        private TeacherDegree _selectedTeacherDegree;
        private ObservableCollection<Post> _allPosts;

        public TeacherWindowViewModel(Teacher teacher)
        {
            ADO ado = new ADO();
            _teacher = teacher;
            _teacherTitles = ado.GetAllTeacherTitle();
            _teacherDegrees = ado.GetAllTeacherDegree();
            _allPosts = ado.GetAllPosts();
            Title = "Добавление преподавателя";
            _firstName = teacher.FirstName;
            _lastName = teacher.LastName;
            _middleName = teacher.MiddleName;
            _birthDate = DateTime.Now.AddYears(-40);

            if (FirstName != null)
            {
                Title = $"Редактирование преподавателя - {FirstName} {LastName}";
                _birthDate = teacher.BirthDate;
                _selectedTeacherTitle = TeacherTitles.First(t => t.Id == teacher.TitleId);
                _selectedTeacherDegree = TeacherDegrees.First(d => d.Id == teacher.DegreeId);
                _allPosts = ado.GetAllPostsForTeacher(teacher.Id);
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

        public IEnumerable<TeacherTitle> TeacherTitles
        {
            get => _teacherTitles;
            set
            {
                _teacherTitles = value;
                OnPropertyChanged();
            }
        }

        public TeacherTitle SelectedTeacherTitle
        {
            get => _selectedTeacherTitle;
            set
            {
                _selectedTeacherTitle = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<TeacherDegree> TeacherDegrees
        {
            get => _teacherDegrees;
            set
            {
                _teacherDegrees = value;
                OnPropertyChanged();
            }
        }

        public TeacherDegree SelectedTeacherDegree
        {
            get => _selectedTeacherDegree;
            set
            {
                _selectedTeacherDegree = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Post> AllPosts
        {
            get => _allPosts;
            set
            {
                _allPosts = value;
                OnPropertyChanged();
            }
        }

        #region Command

        #region SaveCommand

        private RelayCommand _SaveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return _SaveCommand ??= new RelayCommand(obj =>
                {
                    _teacher.LastName = LastName;
                    _teacher.FirstName = FirstName;
                    _teacher.MiddleName = MiddleName;
                    _teacher.BirthDate = BirthDate;
                    _teacher.TitleId = SelectedTeacherTitle.Id;
                    _teacher.DegreeId = SelectedTeacherDegree.Id;
                    _teacher.Posts = AllPosts.Where(p => p.IsSelected).ToList();

                    Window window = obj as Window;
                    window.DialogResult = true;
                    window.Close();
                },
                    obj =>
                        !string.IsNullOrEmpty(FirstName) &&
                        !string.IsNullOrEmpty(LastName) &&
                        SelectedTeacherTitle != null &&
                        SelectedTeacherDegree != null && 
                        AllPosts.Any(p => p.IsSelected));
            }
        }

        #endregion

        #endregion

    }
}
