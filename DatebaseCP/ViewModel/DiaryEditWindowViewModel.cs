using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    class DiaryEditWindowViewModel : BaseViewModel
    {
        private string _title;
        private Diary _diary;
        private DateTime _date;
        private ObservableCollection<Lesson> _lessons;
        private Lesson _selectedLesson;
        private ObservableCollection<TypeCertification> _typeCertifications;
        private TypeCertification _selectedTypeCertification;
        private ObservableCollection<Teacher> _teachers;
        private Teacher _selectedTeacher;
        private IList<int> _scoreList;
        private int _selectedScore;
        private int _idStudent;

        ADO ado = new ADO();

        public DiaryEditWindowViewModel(Diary diary, int idStudent)
        {
            Title = "Добавление оценки";
            _diary = diary;
            Date = DateTime.Now;
            Lessons = ado.GetAllLessons();
            TypeCertifications = ado.GetAllTypeCertifications();
            ScoreList = new List<int>() { 1, 2, 3, 4, 5 };
            _idStudent = idStudent;

            if (diary.Id != 0)
            {
                Title = "Редактирование оценки";
                SelectedLesson = _lessons.First(l => l.Id == diary.LessonId);
                SelectedTypeCertification = _typeCertifications.First(t => t.Id == diary.TypeId);
                SelectedScore = _scoreList.First(s => s == diary.Score);
                SelectedTeacher = _teachers.First(t => t.Id == diary.TeacherId);
            }
            else
            {
                SelectedLesson = _lessons.FirstOrDefault();
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

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Lesson> Lessons
        {
            get => _lessons;
            set
            {
                _lessons = value;
                OnPropertyChanged();
            }
        }

        public Lesson SelectedLesson
        {
            get => _selectedLesson;
            set
            {
                _selectedLesson = value;
                Teachers = ado.GetTeacherForLesson(_selectedLesson.Id);
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

        public TypeCertification SelectedTypeCertification
        {
            get => _selectedTypeCertification;
            set
            {
                _selectedTypeCertification = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Teacher> Teachers
        {
            get => _teachers;
            set
            {
                _teachers = value;
                OnPropertyChanged();
            }
        }

        public Teacher SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                _selectedTeacher = value;
                OnPropertyChanged();
            }
        }

        public IList<int> ScoreList
        {
            get => _scoreList;
            set
            {
                _scoreList = value;
                OnPropertyChanged();
            }
        }

        public int SelectedScore
        {
            get => _selectedScore;
            set
            {
                _selectedScore = value;
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
                        _diary.StudentId = _idStudent;
                        _diary.TeacherId = SelectedTeacher.Id;
                        _diary.Date = Date;
                        _diary.Score = SelectedScore;
                        _diary.LessonId = SelectedLesson.Id;
                        _diary.TypeId = SelectedTypeCertification.Id;

                        Window window = obj as Window;
                        window.DialogResult = true;
                        window.Close();
                    },
                    obj =>
                        SelectedTeacher != null &&
                        SelectedScore != null &&
                        SelectedLesson != null &&
                        SelectedTypeCertification != null);
            }
        }

        #endregion

        #endregion
    }
}
