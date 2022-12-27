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

                    command.CommandText = "CREATE TABLE Students(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, LastName TEXT NOT NULL, FirstName TEXT NOT NULL, MiddleName TEXT NOT NULL, BirthDate TEXT, Group_id INTEGER NOT NULL)";
                    command.ExecuteNonQuery();

                    string commandStr = "INSERT INTO Students (LastName, FirstName, MiddleName, BirthDate, Group_id) VALUES";
                    int max = 9;
                    for (int i = 0; i < max; i++)
                    {
                        if (i == max - 1)
                        {
                            commandStr += $" ('Фамилия_{i + 1}', 'Имя_{i + 1}', 'Отчество_{i + 1}', '{DateTime.Now.AddYears(-18 - i).AddMonths(-i).AddDays(-new Random().Next(1, 28))}', '{i % 2 + 1}')";
                        }
                        else
                        {
                            commandStr += $" ('Фамилия_{i + 1}', 'Имя_{i + 1}', 'Отчество_{i + 1}', '{DateTime.Now.AddYears(-18 - i).AddMonths(-i).AddDays(-new Random().Next(1, 28))}', '{i % 2 + 1}'),";
                        }
                    }
                    command.CommandText = commandStr;
                    command.ExecuteNonQuery();

                    #endregion

                    #region speciality

                    command.CommandText = "CREATE TABLE Speciality(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    commandStr = "INSERT INTO Speciality (Name) VALUES";
                    max = 2;
                    for (int i = 0; i < max; i++)
                    {
                        if (i == max - 1)
                        {
                            commandStr += $" ('Специальность_{i + 1}')";
                        }
                        else
                        {
                            commandStr += $" ('Специальность_{i + 1}'),";
                        }
                    }
                    command.CommandText = commandStr;
                    command.ExecuteNonQuery();

                    #endregion

                    #region formOfEducation

                    command.CommandText = "CREATE TABLE FormOfEducation(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    commandStr = "INSERT INTO FormOfEducation (Name) VALUES";
                    max = 2;
                    for (int i = 0; i < max; i++)
                    {
                        if (i == max - 1)
                        {
                            commandStr += $" ('Форма_обучения_{i + 1}')";
                        }
                        else
                        {
                            commandStr += $" ('Форма_обучения_{i + 1}'),";
                        }
                    }
                    command.CommandText = commandStr;
                    command.ExecuteNonQuery();

                    #endregion

                    #region groups

                    command.CommandText = "CREATE TABLE Groups(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, Speciality_id INTEGER NOT NULL, FormOfEducation_id INTEGER NOT NULL)";
                    command.ExecuteNonQuery();

                    commandStr = "INSERT INTO Groups (Name, Speciality_id, FormOfEducation_id) VALUES";
                    max = 2;
                    for (int i = 0; i < max; i++)
                    {
                        if (i == max - 1)
                        {
                            commandStr += $" ('Группа_{i + 1}', '{i % 2 + 1}', '{i % 2 + 1}')";
                        }
                        else
                        {
                            commandStr += $" ('Группа_{i + 1}', '{i % 2 + 1}', '{i % 2 + 1}'),";
                        }
                    }
                    command.CommandText = commandStr;
                    command.ExecuteNonQuery();

                    #endregion

                    #region teachers

                    command.CommandText = "CREATE TABLE Teachers(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, LastName TEXT NOT NULL, FirstName TEXT NOT NULL, MiddleName TEXT NOT NULL, BirthDate TEXT, TeachingTitle_id INTEGER NOT NULL, TeachingDegree_id INTEGER NOT NULL)";
                    command.ExecuteNonQuery();

                    commandStr = "INSERT INTO Teachers (LastName, FirstName, MiddleName, BirthDate, TeachingTitle_id, TeachingDegree_id) VALUES";
                    max = 4;
                    for (int i = 0; i < max; i++)
                    {
                        if (i == max - 1)
                        {
                            commandStr += $" ('Фамилия_{i + 1}', 'Имя_{i + 1}', 'Отчество_{i + 1}', '{DateTime.Now.AddYears(-25 - i).AddMonths(-i).AddDays(-new Random().Next(1, 28))}', '{i % 2 + 1}', '{i % 2 + 1}')";
                        }
                        else
                        {
                            commandStr += $" ('Фамилия_{i + 1}', 'Имя_{i + 1}', 'Отчество_{i + 1}', '{DateTime.Now.AddYears(-25 - i).AddMonths(-i).AddDays(-new Random().Next(1, 28))}', '{i % 2 + 1}', '{i % 2 + 1}'),";
                        }
                    }
                    command.CommandText = commandStr;
                    command.ExecuteNonQuery();

                    #endregion

                    #region teacherTitle

                    command.CommandText = "CREATE TABLE TeacherTitle(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    commandStr = "INSERT INTO TeacherTitle (Name) VALUES";
                    max = 2;
                    for (int i = 0; i < max; i++)
                    {
                        if (i == max - 1)
                        {
                            commandStr += $" ('Звание_{i + 1}')";
                        }
                        else
                        {
                            commandStr += $" ('Звание_{i + 1}'),";
                        }
                    }
                    command.CommandText = commandStr;
                    command.ExecuteNonQuery();

                    #endregion

                    #region post

                    command.CommandText = "CREATE TABLE Post(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    commandStr = "INSERT INTO Post (Name) VALUES";
                    max = 6;
                    for (int i = 0; i < max; i++)
                    {
                        if (i == max - 1)
                        {
                            commandStr += $" ('Должность_{i + 1}')";
                        }
                        else
                        {
                            commandStr += $" ('Должность_{i + 1}'),";
                        }
                    }
                    command.CommandText = commandStr;
                    command.ExecuteNonQuery();

                    #endregion

                    #region teacherPost

                    command.CommandText = "CREATE TABLE TeacherPost(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Teacher_id INTEGER NOT NULL, Post_id INTEGER NOT NULL)";
                    command.ExecuteNonQuery();

                    commandStr = "INSERT INTO TeacherPost (Teacher_id, Post_id) VALUES";
                    max = 4;
                    for (int i = 0; i < max; i++)
                    {
                        if (i == max - 1)
                        {
                            commandStr += $" ('{i + 1}', '{i + 1}'), ('{i + 1}', '{i + 2}')";
                        }
                        else
                        {
                            commandStr += $" ('{i + 1}', '{i + 1}'),";
                        }
                    }
                    command.CommandText = commandStr;
                    command.ExecuteNonQuery();

                    #endregion

                    #region degree

                    command.CommandText = "CREATE TABLE TeacherDegree(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();

                    commandStr = "INSERT INTO TeacherDegree (Name) VALUES";
                    max = 2;
                    for (int i = 0; i < max; i++)
                    {
                        if (i == max - 1)
                        {
                            commandStr += $" ('Ученая_степень_{i + 1}')";
                        }
                        else
                        {
                            commandStr += $" ('Ученая_степень_{i + 1}'),";
                        }
                    }
                    command.CommandText = commandStr;
                    command.ExecuteNonQuery();

                    #endregion

                }
            }

            #endregion

            #region LoadData

            university1.Specialitys = ado.GetAllSpecialities();
            university1.FormOfEducations = ado.GetAllFormOfEducation();
            university1.Groups = ado.GetAllGroup();
            university1.TeachersTitle = ado.GetAllTeacherTitle();
            university1.TeachersDegree = ado.GetAllTeacherDegree();
            university1.Posts = ado.GetAllPosts();
            university1.TeachersTable = ado.GetAllTeachers();

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
                SelectedGroup = _groups.First();
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
                GroupInfo = $"{_selectedGroup.Id} - " +
                            $"{_selectedGroup.Name} - " +
                            $"{university1.Specialitys.First(s => s.Id == _selectedGroup.SpecialityID).Name} - " +
                            $"{university1.FormOfEducations.First(f => f.Id == _selectedGroup.FormOfEducationID).Name} - " +
                            $"{ado.CountStudentsInGroup(_selectedGroup.Id)}";
                Students = ado.GetStudentsInGroup(_selectedGroup.Id);
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

                    university1.Specialitys = ado.GetAllSpecialities();
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

                    university1.FormOfEducations = ado.GetAllFormOfEducation();
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
                });
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

                    university1.Posts = ado.GetAllPosts();
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

                    university1.TeachersTitle = ado.GetAllTeacherTitle();
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

                    university1.TeachersDegree = ado.GetAllTeacherDegree();
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

                        TeachersTables = ado.GetAllTeachers();
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

                        TeachersTables = ado.GetAllTeachers();
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

                    TeachersTables = ado.GetAllTeachers();
                }, obj => SelectedDataRow != null);
            }
        }

        #endregion

        #endregion

    }
}
