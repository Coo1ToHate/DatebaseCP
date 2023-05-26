using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.View;
using DatebaseCP.ViewModel.Base;
using Microsoft.Data.Sqlite;

namespace DatebaseCP.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private readonly string _dbFileName = "data.db";
        ADO ado = new ADO();
        University university1 = new("ТУСУР");

        private string _title;
        private string _statusBarMsg;
        private University _selectedUniversity;
        private ObservableCollection<Group> _groups;
        private string _groupInfo;
        private Group _selectedGroup;
        private ObservableCollection<Student> _students;
        private Student _selectedStudent;
        private Teacher _selectedTeacher;
        private DataTable _teachersTables;
        private DataRowView _selectedDataRow;

        public MainWindowViewModel()
        {
            Title = "Универ";
            StatusBarMsg = "Готов!";

            #region test

            if (!File.Exists(_dbFileName))
            {
                using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
                {
                    connection.Open();

                    SqliteCommand command = new();
                    command.Connection = connection;

                    #region students

                    command.CommandText = "CREATE TABLE Students(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, LastName TEXT NOT NULL, FirstName TEXT NOT NULL, MiddleName TEXT, BirthDate TEXT NOT NULL, Group_id INTEGER NOT NULL, RecordBook TEXT NOT NULL UNIQUE)";
                    command.ExecuteNonQuery();

                    #endregion

                    #region speciality

                    command.CommandText = "CREATE TABLE Speciality(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    #endregion

                    #region formOfEducation

                    command.CommandText = "CREATE TABLE FormOfEducation(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    #endregion

                    #region groups

                    command.CommandText = "CREATE TABLE Groups(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, Speciality_id INTEGER NOT NULL, FormOfEducation_id INTEGER NOT NULL, Curator_id INTEGER NOT NULL)";
                    command.ExecuteNonQuery();

                    #endregion

                    #region teachers

                    command.CommandText = "CREATE TABLE Teachers(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, LastName TEXT NOT NULL, FirstName TEXT NOT NULL, MiddleName TEXT, BirthDate TEXT NOT NULL, TeachingTitle_id INTEGER NOT NULL)";
                    command.ExecuteNonQuery();

                    #endregion

                    #region teacherTitle

                    command.CommandText = "CREATE TABLE TeacherTitle(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    #endregion

                    #region post

                    command.CommandText = "CREATE TABLE Post(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    #endregion

                    #region teacherPost

                    command.CommandText = "CREATE TABLE TeacherPost(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Teacher_id INTEGER NOT NULL, Post_id INTEGER NOT NULL)";
                    command.ExecuteNonQuery();

                    #endregion

                    #region degree

                    command.CommandText = "CREATE TABLE Degrees(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    #endregion

                    #region teacherDegree

                    command.CommandText = "CREATE TABLE TeacherDegree(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Teacher_id INTEGER NOT NULL, Degree_id INTEGER NOT NULL)";
                    command.ExecuteNonQuery();

                    #endregion

                    #region teacherLesson

                    command.CommandText = "CREATE TABLE TeacherLesson(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Teacher_id INTEGER NOT NULL, Lesson_id INTEGER NOT NULL)";
                    command.ExecuteNonQuery();

                    #endregion

                    #region diary

                    command.CommandText = "CREATE TABLE Lessons(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE TypesCertification(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE Diary(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Student_id INTEGER NOT NULL, Teacher_id INTEGER NOT NULL, Date TEXT NOT NULL, Score INTEGER NOT NULL, Lesson_id INTEGER NOT NULL, Type_id INTEGER NOT NULL)";
                    command.ExecuteNonQuery();

                    #endregion

                    InitDemoDb();
                }
            }

            #endregion

            #region LoadData

            university1.Groups = ado.GetAllGroup();
            university1.TeachersTable = ado.GetAllTeachersTable();

            #endregion

            SelectedUniversity = university1;
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

        public University SelectedUniversity
        {
            get => _selectedUniversity;
            set
            {
                _selectedUniversity = value;
                Groups = ado.GetAllGroup();
                TeachersTables = _selectedUniversity.TeachersTable;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Group> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                SelectedGroup = _groups.FirstOrDefault();
                OnPropertyChanged();
            }
        }

        public string GroupInfo
        {
            get => _groupInfo;
            set
            {
                _groupInfo = value;
                OnPropertyChanged();
            }
        }

        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                if (_selectedGroup != null)
                {
                    GroupInfo = $"{_selectedGroup.Name} - " +
                                $"{ado.GetSpeciality(_selectedGroup.SpecialityId).Name} - " +
                                $"{ado.GetFormOfEducation(_selectedGroup.FormOfEducationId).Name} - " +
                                $"{ado.CountStudentsInGroup(_selectedGroup.Id)}";
                    Students = ado.GetStudentsInGroup(_selectedGroup.Id);
                }
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

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
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

        public DataTable TeachersTables
        {
            get => _teachersTables;
            set
            {
                _teachersTables = value;
                OnPropertyChanged();
            }
        }

        public DataRowView SelectedDataRow
        {
            get => _selectedDataRow;
            set
            {
                _selectedDataRow = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        #region CloseAppCommand

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

        #region AddGroupCommand

        private RelayCommand _addGroupCommand;

        public RelayCommand AddGroupCommand
        {
            get
            {
                return _addGroupCommand ??= new RelayCommand(obj =>
                {
                    Group newGroup = new Group();

                    GroupWindow groupWindow = new GroupWindow
                    {
                        DataContext = new GroupWindowViewModel(newGroup),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    groupWindow.ShowDialog();

                    if (groupWindow.DialogResult.Value)
                    {
                        ado.InsertGroup(newGroup);
                        Groups = ado.GetAllGroup();
                        SelectedGroup = Groups.First(g => g.Id == newGroup.Id);
                    }
                });
            }
        }

        #endregion

        #region EditGroupCommand

        private RelayCommand _editGroupCommand;

        public RelayCommand EditGroupCommand
        {
            get
            {
                return _editGroupCommand ??= new RelayCommand(obj =>
                {
                    Group updGroup = SelectedGroup;

                    GroupWindow groupWindow = new GroupWindow()
                    {
                        DataContext = new GroupWindowViewModel(updGroup)
                    };

                    groupWindow.Owner = obj as Window;
                    groupWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    groupWindow.ShowDialog();

                    if (groupWindow.DialogResult.Value)
                    {
                        ado.UpdateGroup(updGroup);
                        Groups = ado.GetAllGroup();
                        SelectedGroup = Groups.First(g => g.Id == updGroup.Id);
                    }
                },
                    obj => SelectedGroup != null);
            }
        }

        #endregion

        #region DeleteGroupCommand

        private RelayCommand _deleteGroupCommand;

        public RelayCommand DeleteGroupCommand
        {
            get
            {
                return _deleteGroupCommand ??= new RelayCommand(obj =>
                {
                    ado.DeleteGroup(SelectedGroup);
                    Groups = ado.GetAllGroup();
                    SelectedGroup = Groups.FirstOrDefault();
                },
                    obj => SelectedGroup != null && ado.CountStudentsInGroup(SelectedGroup.Id) == 0);
            }
        }

        #endregion

        #region GroupReport

        private RelayCommand _groupReport;

        public RelayCommand GroupReport
        {
            get
            {
                return _groupReport ??= new RelayCommand(obj =>
                    {
                        Group group = SelectedGroup;

                        ReportGroupWindow reportGroupWindow = new ReportGroupWindow()
                        {
                            DataContext = new ReportGroupWindowViewModel(group)
                        };

                        reportGroupWindow.Owner = obj as Window;
                        reportGroupWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        reportGroupWindow.ShowDialog();
                    },
                    obj => SelectedGroup != null);
            }
        }

        #endregion

        #region ListSpecialityCommand

        private RelayCommand _listSpecialityCommand;

        public RelayCommand ListSpecialityCommand
        {
            get
            {
                return _listSpecialityCommand ??= new RelayCommand(obj =>
                {
                    ListSpecialityWindow listSpecialityWindow = new ListSpecialityWindow()
                    {
                        DataContext = new ListSpecialityWindowViewModel()
                    };

                    listSpecialityWindow.Owner = obj as Window;
                    listSpecialityWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    listSpecialityWindow.ShowDialog();
                });
            }
        }

        #endregion

        #region ListFormOfEducationCommand

        private RelayCommand _listFormOfEducationCommand;

        public RelayCommand ListFormOfEducationCommand
        {
            get
            {
                return _listFormOfEducationCommand ??= new RelayCommand(obj =>
                {
                    ListFormOfEducationWindow listFormOfEducationWindow = new ListFormOfEducationWindow()
                    {
                        DataContext = new ListFormOfEducationWindowViewModel()
                    };

                    listFormOfEducationWindow.Owner = obj as Window;
                    listFormOfEducationWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    listFormOfEducationWindow.ShowDialog();
                });
            }
        }

        #endregion

        #region AddStudentCommand

        private RelayCommand _addStudentCommand;

        public RelayCommand AddStudentCommand
        {
            get
            {
                return _addStudentCommand ??= new RelayCommand(obj =>
                {
                    Student newStudent = new Student();
                    newStudent.GroupId = SelectedGroup.Id;

                    StudentWindow studentWindow = new StudentWindow
                    {
                        DataContext = new StudentWindowViewModel(newStudent),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    studentWindow.ShowDialog();

                    if (studentWindow.DialogResult.Value)
                    {
                        ado.InsertStudent(newStudent);
                        SelectedGroup = Groups.First(g => g.Id == newStudent.GroupId);
                        SelectedStudent = Students.First(s => s.Id == newStudent.Id);
                    }
                }, obj => SelectedGroup != null);
            }
        }

        #endregion

        #region EditStudentCommand

        private RelayCommand _editStudentCommand;

        public RelayCommand EditStudentCommand
        {
            get
            {
                return _editStudentCommand ??= new RelayCommand(obj =>
                {
                    Student editStudent = SelectedStudent;

                    StudentWindow studentWindow = new StudentWindow
                    {
                        DataContext = new StudentWindowViewModel(editStudent),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    studentWindow.ShowDialog();

                    if (studentWindow.DialogResult.Value)
                    {
                        ado.UpdateStudent(editStudent);
                        SelectedGroup = Groups.First(g => g.Id == editStudent.GroupId);
                        SelectedStudent = Students.First(s => s.Id == editStudent.Id);
                    }
                },
                    obj => SelectedStudent != null);
            }
        }

        #endregion

        #region DeleteStudentCommand

        private RelayCommand _deleteStudentCommand;

        public RelayCommand DeleteStudentCommand
        {
            get
            {
                return _deleteStudentCommand ??= new RelayCommand(obj =>
                {
                    ado.DeleteStudent(SelectedStudent);
                    Students = ado.GetStudentsInGroup(SelectedGroup.Id);
                    SelectedStudent = Students.FirstOrDefault();
                },
                    obj => SelectedStudent != null);
            }
        }

        #endregion

        #region ListPostCommand

        private RelayCommand _listPostCommand;

        public RelayCommand ListPostCommand
        {
            get
            {
                return _listPostCommand ??= new RelayCommand(obj =>
                {
                    ListPostWindow listPostWindow = new ListPostWindow()
                    {
                        DataContext = new ListPostWindowViewModel()
                    };

                    listPostWindow.Owner = obj as Window;
                    listPostWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    listPostWindow.ShowDialog();
                });
            }
        }

        #endregion

        #region ListTeacherTitleCommand

        private RelayCommand _listTeacherTitleCommand;

        public RelayCommand ListTeacherTitleCommand
        {
            get
            {
                return _listTeacherTitleCommand ??= new RelayCommand(obj =>
                {
                    ListTitleWindow listTitleWindow = new ListTitleWindow()
                    {
                        DataContext = new ListTitleWindowViewModel()
                    };

                    listTitleWindow.Owner = obj as Window;
                    listTitleWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    listTitleWindow.ShowDialog();
                });
            }
        }

        #endregion

        #region ListTeacherDegreeCommand

        private RelayCommand _listTeacherDegreeCommand;

        public RelayCommand ListTeacherDegreeCommand
        {
            get
            {
                return _listTeacherDegreeCommand ??= new RelayCommand(obj =>
                {
                    ListDegreeWindow listDegreeWindow = new ListDegreeWindow()
                    {
                        DataContext = new ListDegreeWindowViewModel()
                    };

                    listDegreeWindow.Owner = obj as Window;
                    listDegreeWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    listDegreeWindow.ShowDialog();
                });
            }
        }

        #endregion

        #region AddTeacherCommand

        private RelayCommand _addTeacherCommand;

        public RelayCommand AddTeacherCommand
        {
            get
            {
                return _addTeacherCommand ??= new RelayCommand(obj =>
                {
                    Teacher newTeacher = new Teacher();

                    TeacherWindow teacherWindow = new TeacherWindow
                    {
                        DataContext = new TeacherWindowViewModel(newTeacher),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    teacherWindow.ShowDialog();

                    if (teacherWindow.DialogResult.Value)
                    {
                        ado.InsertTeacher(newTeacher);
                        ado.DeletePostsWithTeacher(newTeacher.Id);
                        ado.InsertTeacherPosts(newTeacher.Id, newTeacher.Posts);
                        ado.DeleteDegreesWithTeacher(newTeacher.Id);
                        ado.InsertTeacherDegrees(newTeacher.Id, newTeacher.Degrees);
                        ado.DeleteLessonsWithTeacher(newTeacher.Id);
                        ado.InsertTeacherLessons(newTeacher.Id, newTeacher.Lessons);

                        TeachersTables = ado.GetAllTeachersTable();
                    }
                });
            }
        }

        #endregion

        #region EditTeacherCommand

        private RelayCommand _editTeacherCommand;

        public RelayCommand EditTeacherCommand
        {
            get
            {
                return _editTeacherCommand ??= new RelayCommand(obj =>
                {
                    Teacher updTeacher = ado.GetTeacher(int.Parse(SelectedDataRow.Row.ItemArray[0].ToString()));

                    TeacherWindow teacherWindow = new TeacherWindow
                    {
                        DataContext = new TeacherWindowViewModel(updTeacher),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    teacherWindow.ShowDialog();

                    if (teacherWindow.DialogResult.Value)
                    {
                        ado.UpdateTeacher(updTeacher);
                        ado.DeletePostsWithTeacher(updTeacher.Id);
                        ado.InsertTeacherPosts(updTeacher.Id, updTeacher.Posts);
                        ado.DeleteDegreesWithTeacher(updTeacher.Id);
                        ado.InsertTeacherDegrees(updTeacher.Id, updTeacher.Degrees);
                        ado.DeleteLessonsWithTeacher(updTeacher.Id);
                        ado.InsertTeacherLessons(updTeacher.Id, updTeacher.Lessons);

                        TeachersTables = ado.GetAllTeachersTable();
                    }

                }, obj => SelectedDataRow != null);
            }
        }

        #endregion

        #region DeleteTeacherCommand

        private RelayCommand _deleteTeacherCommand;

        public RelayCommand DeleteTeacherCommand
        {
            get
            {
                return _deleteTeacherCommand ??= new RelayCommand(obj =>
                {
                    ado.DeleteTeacher(ado.GetTeacher(int.Parse(SelectedDataRow.Row.ItemArray[0].ToString())));
                    ado.DeletePostsWithTeacher(int.Parse(SelectedDataRow.Row.ItemArray[0].ToString()));
                    ado.DeleteDegreesWithTeacher(int.Parse(SelectedDataRow.Row.ItemArray[0].ToString()));
                    ado.DeleteLessonsWithTeacher(int.Parse(SelectedDataRow.Row.ItemArray[0].ToString()));

                    TeachersTables = ado.GetAllTeachersTable();
                }, obj => SelectedDataRow != null);
            }
        }

        #endregion

        #region ListLessonsCommand

        private RelayCommand _listLessonsCommand;

        public RelayCommand ListLessonsCommand
        {
            get
            {
                return _listLessonsCommand ??= new RelayCommand(obj =>
                {
                    ListLessonsWindow listLessonsWindow = new ListLessonsWindow()
                    {
                        DataContext = new ListLessonsWindowViewModel()
                    };

                    listLessonsWindow.Owner = obj as Window;
                    listLessonsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    listLessonsWindow.ShowDialog();
                });
            }
        }

        #endregion

        #region ListTypeCertificationCommand

        private RelayCommand _listTypeCertificationCommand;

        public RelayCommand ListTypeCertificationCommand
        {
            get
            {
                return _listTypeCertificationCommand ??= new RelayCommand(obj =>
                {
                    ListTypesCertificationWindow listTypesCertificationWindow = new ListTypesCertificationWindow()
                    {
                        DataContext = new ListTypesCertificationWindowViewModel()
                    };

                    listTypesCertificationWindow.Owner = obj as Window;
                    listTypesCertificationWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    listTypesCertificationWindow.ShowDialog();
                });
            }
        }

        #endregion

        #region DiaryCommand

        private RelayCommand _diaryCommand;

        public RelayCommand DiaryCommand
        {
            get
            {
                return _diaryCommand ??= new RelayCommand(obj =>
                {
                    DiaryWindow diaryWindow = new DiaryWindow()
                    {
                        DataContext = new DiaryWindowViewModel(SelectedStudent)
                    };

                    diaryWindow.Owner = obj as Window;
                    diaryWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    diaryWindow.ShowDialog();

                }, obj => SelectedStudent != null);
            }
        }

        #endregion

        #region StudentReportCommand

        private RelayCommand _studentReportCommand;

        public RelayCommand StudentReportCommand
        {
            get
            {
                return _studentReportCommand ??= new RelayCommand(obj =>
                {
                    ReportStudentWindow reportStudentWindow = new ReportStudentWindow()
                    {
                        DataContext = new ReportStudentWindowViewModel(SelectedStudent)
                    };

                    reportStudentWindow.Owner = obj as Window;
                    reportStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    reportStudentWindow.ShowDialog();

                }, obj => SelectedStudent != null);
            }
        }

        #endregion

        #region TeacherReportCommand

        private RelayCommand _teacherReportCommand;

        public RelayCommand TeacherReportCommand
        {
            get
            {
                return _teacherReportCommand ??= new RelayCommand(obj =>
                {
                    Teacher teacher = ado.GetTeacher(int.Parse(SelectedDataRow.Row.ItemArray[0].ToString()));

                    ReportTeacherWindow reportTeacherWindow = new ReportTeacherWindow()
                    {
                        DataContext = new ReportTeacherWindowViewModel(teacher)
                    };

                    reportTeacherWindow.Owner = obj as Window;
                    reportTeacherWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    reportTeacherWindow.ShowDialog();
                }, obj => SelectedDataRow != null);
            }
        }

        #endregion

        #endregion

        private void InitDemoDb()
        {
            for (int i = 0; i < 20; i++)
            {
                Student tmp = new Student
                {
                    LastName = $"Фамилия_{i + 1}",
                    FirstName = $"Имя_{i + 1}",
                    MiddleName = $"Отчество_{i + 1}",
                    BirthDate = DateTime.Now.AddYears(-17 - i).AddDays(-10 * i),
                    GroupId = i % 2 + 1,
                    RecordBook = $"Зачетка_{i + 1}"

                };
                ado.InsertStudent(tmp);

                Diary tmp2 = new Diary
                {
                    StudentId = i + 1,
                    TeacherId = i / 5 + 1,
                    Date = DateTime.Now.AddDays(-10 * (20 - i)),
                    Score = i / 5 + 1,
                    LessonId = i / 5 + 1,
                    TypeId = i % 2 + 1
                };
                ado.InsertDiary(tmp2);
            }

            for (int i = 0; i < 2; i++)
            {
                Speciality tmp = new Speciality
                {
                    Name = $"Специализация_{i + 1}"
                };
                ado.InsertSpeciality(tmp);

                FormOfEducation tmp2 = new FormOfEducation
                {
                    Name = $"Форма обучения {i + 1}"
                };
                ado.InsertFormOfEducation(tmp2);

                Group tmp3 = new Group
                {
                    Name = $"Группа_{i + 1}",
                    SpecialityId = i % 2 + 1,
                    FormOfEducationId = i % 2 + 1,
                    CuratorId = i % 2 + 1
                };
                ado.InsertGroup(tmp3);

                TeacherTitle tmp4 = new TeacherTitle
                {
                    Name = $"Звание_{i + 1}"
                };
                ado.InsertTeacherTitle(tmp4);

                TypeCertification tmp5 = new TypeCertification
                {
                    Name = $"Вид работы {i + 1}"
                };
                ado.InsertTypeCertification(tmp5);
            }

            for (int i = 0; i < 4; i++)
            {
                Teacher tmp = new Teacher
                {
                    LastName = $"Фамилия_{i + 1}",
                    FirstName = $"Имя_{i + 1}",
                    MiddleName = $"Отчество_{i + 1}",
                    BirthDate = DateTime.Now.AddYears(-42 - i).AddDays(-10 * i),
                    TitleId = i % 2 + 1
                };
                ado.InsertTeacher(tmp);

                Degree tmp2 = new Degree
                {
                    Name = $"Ученая_степень_{i + 1}"
                };
                ado.InsertDegree(tmp2);

                TeacherDegree tmp3 = new TeacherDegree
                {
                    TeacherId = i + 1,
                    DegreeId = i + 1
                };
                ado.InsertTeacherDegree(tmp3);

                Post tmp4 = new Post
                {
                    Name = $"Должность_{i + 1}"
                };
                ado.InsertPost(tmp4);

                TeacherPost tmp5 = new TeacherPost
                {
                    TeacherId = i + 1,
                    PostId = i + 1
                };
                ado.InsertTeacherPost(tmp5);

                Lesson tmp6 = new Lesson
                {
                    Name = $"Предмет_{i + 1}"
                };
                ado.InsertLesson(tmp6);

                TeacherLesson tmp7 = new TeacherLesson
                {
                    TeacherId = i + 1,
                    LessonId = i + 1
                };
                ado.InsertTeacherLesson(tmp7);
            }
        }
    }
}
