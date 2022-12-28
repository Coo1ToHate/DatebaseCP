using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.ViewModel.Base;
using System.Collections.ObjectModel;
using DatebaseCP.Command;
using DatebaseCP.View;
using System.Windows;

namespace DatebaseCP.ViewModel
{
    class ListLessonsWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();

        private string _title;
        private ObservableCollection<Lesson> _lessons;
        private Lesson _selelectedLesson;

        public ListLessonsWindowViewModel()
        {
            _title = "Предметы";
            _lessons = ado.GetAllLessons();
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

        public ObservableCollection<Lesson> Lessons
        {
            get => _lessons;
            set
            {
                _lessons = value;
                OnPropertyChanged();
            }
        }

        public Lesson SelelectedLesson
        {
            get => _selelectedLesson;
            set
            {
                _selelectedLesson = value;
                OnPropertyChanged();
            }
        }

        #region Command

        #region AddLessonCommand

        private RelayCommand _addLessonCommand;

        public RelayCommand AddLessonCommand
        {
            get
            {
                return _addLessonCommand ??= new RelayCommand(obj =>
                {
                    Lesson newLesson = new Lesson();

                    ListLessonEditWindow listLessonEditWindow = new ListLessonEditWindow
                    {
                        DataContext = new ListLessonEditWindowViewModel(newLesson),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    listLessonEditWindow.ShowDialog();

                    if (listLessonEditWindow.DialogResult.Value)
                    {
                        ado.InsertLesson(newLesson);
                        Lessons = ado.GetAllLessons();
                    }
                });
            }
        }

        #endregion

        #region EditLessonCommand

        private RelayCommand _editLessonCommand;

        public RelayCommand EditLessonCommand
        {
            get
            {
                return _editLessonCommand ??= new RelayCommand(obj =>
                {
                    Lesson updLesson = SelelectedLesson;

                    ListLessonEditWindow listLessonEditWindow = new ListLessonEditWindow
                    {
                        DataContext = new ListLessonEditWindowViewModel(updLesson),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    listLessonEditWindow.ShowDialog();

                    if (listLessonEditWindow.DialogResult.Value)
                    {
                        ado.UpdateLesson(updLesson);
                        Lessons = ado.GetAllLessons();
                    }
                }, obj => SelelectedLesson != null);
            }
        }

        #endregion

        #region DeleteLessonCommand

        private RelayCommand _deleteLessonCommand;

        public RelayCommand DeleteLessonCommand
        {
            get
            {
                return _deleteLessonCommand ??= new RelayCommand(obj =>
                {
                    ado.DeleteLesson(SelelectedLesson);
                    Lessons = ado.GetAllLessons();
                }, obj => SelelectedLesson != null && ado.CountLesson(SelelectedLesson.Id) == 0);
            }
        }

        #endregion

        #endregion
    }
}
