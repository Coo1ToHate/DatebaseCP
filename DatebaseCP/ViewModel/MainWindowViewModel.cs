using System;
using System.Collections.ObjectModel;
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

        private string _title;
        private string _statusBarMsg;
        private University _selectedUniversity;
        private ObservableCollection<Group> _groups;
        private string _groupInfo;
        private Group _selectedGroup;
        private ObservableCollection<Student> _students;
        private Student _selectedStudent;
        private ObservableCollection<Teacher> _teachers;
        private Teacher _selectedTeacher;

        public MainWindowViewModel()
        {
            Title = "Универ";
            StatusBarMsg = "Готов!";

            University university1 = new("ТУСУР");

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
            foreach (var g in university1.Groups)
            {
                g.Students = ado.GetStudentsInGroup(g);
            }

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();

                #region speciality

                string sqlExpression = "SELECT * FROM Speciality";
                SqliteCommand command = new(sqlExpression, connection);
                //using (SqliteDataReader reader = command.ExecuteReader())
                //{
                //    if (reader.HasRows)
                //    {
                //        while (reader.Read())
                //        {
                //            var id = int.Parse(reader["id"].ToString());
                //            var name = reader["Name"].ToString();

                //            university1.Specialitys.Add(new Speciality(id, name));
                //        }
                //    }
                //}

                #endregion

                #region formOfEducation

                //sqlExpression = "SELECT * FROM FormOfEducation";
                //command = new SqliteCommand(sqlExpression, connection);
                //using (SqliteDataReader reader = command.ExecuteReader())
                //{
                //    if (reader.HasRows)
                //    {
                //        while (reader.Read())
                //        {
                //            var id = int.Parse(reader["id"].ToString());
                //            var name = reader["Name"].ToString();

                //            university1.FormOfEducations.Add(new FormOfEducation(id, name));
                //        }
                //    }
                //}

                #endregion

                #region groups

                //sqlExpression = "SELECT * FROM Groups";
                //command = new SqliteCommand(sqlExpression, connection);
                //using (SqliteDataReader reader = command.ExecuteReader())
                //{
                //    if (reader.HasRows)
                //    {
                //        while (reader.Read())
                //        {
                //            var id = int.Parse(reader["id"].ToString());
                //            var name = reader["Name"].ToString();
                //            var specialitiesId = int.Parse(reader["Speciality_id"].ToString());
                //            var formOfEducationId = int.Parse(reader["FormOfEducation_id"].ToString());

                //            university1.Groups.Add(new Group(id, name, university1.Specialitys[specialitiesId - 1], university1.FormOfEducations[formOfEducationId - 1]));
                //        }
                //    }
                //}

                #endregion

                #region students

                //sqlExpression = "SELECT * FROM Students";
                //command = new SqliteCommand(sqlExpression, connection);
                //using (SqliteDataReader reader = command.ExecuteReader())
                //{
                //    if (reader.HasRows)
                //    {
                //        while (reader.Read())
                //        {
                //            var id = int.Parse(reader["id"].ToString());
                //            var lastName = reader["LastName"].ToString();
                //            var firstName = reader["FirstName"].ToString();
                //            var middleName = reader["MiddleName"].ToString();
                //            var birthDate = DateTime.Parse(reader["BirthDate"].ToString());
                //            var groupId = int.Parse(reader["Group_id"].ToString());
                //            var tmp = new Student(id, lastName, firstName, middleName, birthDate);
                //            university1.Groups[groupId - 1].Students.Add(tmp);
                //        }
                //    }
                //}

                #endregion

                #region teacherTitle

                sqlExpression = "SELECT * FROM TeacherTitle";
                command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["id"].ToString());
                            var title = reader["Name"].ToString();

                            university1.TeachersTitle.Add(new TeacherTitle(id, title));
                        }
                    }
                }

                #endregion

                #region post

                sqlExpression = "SELECT * FROM Post";
                command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["id"].ToString());
                            var name = reader["Name"].ToString();

                            university1.TeacherPosts.Add(new TeacherPost(id, name));
                        }
                    }
                }

                #endregion

                #region degree

                sqlExpression = "SELECT * FROM TeacherDegree";
                command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["id"].ToString());
                            var name = reader["Name"].ToString();

                            university1.TeachersDegree.Add(new TeacherDegree(id, name));
                        }
                    }
                }

                #endregion

                #region teacher

                sqlExpression = "SELECT * FROM Teachers";
                command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["id"].ToString());
                            var lastName = reader["LastName"].ToString();
                            var firstName = reader["FirstName"].ToString();
                            var middleName = reader["MiddleName"].ToString();
                            var birthDate = DateTime.Parse(reader["BirthDate"].ToString());
                            var titleId = int.Parse(reader["TeachingTitle_id"].ToString());
                            var degreeId = int.Parse(reader["TeachingDegree_id"].ToString());
                            var tmp = new Teacher(
                                id,
                                lastName,
                                firstName,
                                middleName,
                                birthDate,
                                university1.TeachersTitle.First(t => t.Id == titleId),
                                university1.TeachersDegree.First(d => d.Id == degreeId)
                                );
                            university1.Teachers.Add(tmp);
                        }
                    }
                }

                #endregion

                #region teacherPost

                sqlExpression = "SELECT * FROM TeacherPost";
                command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["id"].ToString());
                            var teacherID = int.Parse(reader["Teacher_id"].ToString());
                            var postID = int.Parse(reader["Post_id"].ToString());
                            university1.Teachers.First(t => t.Id == teacherID).Posts.Add(university1.TeacherPosts.First(p => p.Id == postID));
                        }
                    }
                }

                #endregion

            }

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
                Teachers = _selectedUniversity.Teachers;
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
                GroupInfo = $"{_selectedGroup.Id} - {_selectedGroup.Name} - {_selectedGroup.Speciality.Name} - {_selectedGroup.FormOfEducation.Name} - {ado.CountStudentsInGroup(_selectedGroup.Id)}";
                Students = ado.GetStudentsInGroup(_selectedGroup);
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


        
        #endregion

    }
}
