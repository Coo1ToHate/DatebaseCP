using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.ViewModel.Base;
using Microsoft.Data.Sqlite;

namespace DatebaseCP.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private readonly string _dbFileName = "data.db";

        private string _title;
        private string _statusBarMsg;
        private ObservableCollection<University> _universities;
        private University _selectedUniversity;
        private ObservableCollection<Group> _groups;
        private string _groupInfo;
        private Group _selectedGroup;
        private ObservableCollection<Student> _students;
        private Student _selectedStudent;

        public MainWindowViewModel()
        {
            Title = "Универ";
            StatusBarMsg = "Готов!";

            University university1 = new("ТУСУР");

            #region test

            //for (int i = 0; i < 3; i++)
            //{
            //    university1.Groups.Add(new Group(i, $"Группа_{i + 1}", new Speciality(i, $"Специализация_{i+1}"), new FormOfEducation(i, $"Форма_обучения_{i + 1}")));
            //    for (int j = 0; j < 5; j++)
            //    {
            //        university1.Groups[i].Students.Add(new Student(i + j, $"Фамилия_{i + j + 1}", $"Имя_{i + j + 1}", $"Отчество_{i + j + 1}", DateTime.Now.AddYears(-18 - i).AddMonths(-j).AddDays(-new Random().Next(1, 28))));
            //    }
            //}

            if (!File.Exists(_dbFileName))
            {
                using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
                {
                    connection.Open();

                    SqliteCommand command = new();
                    command.Connection = connection;

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
                }
            }

            #endregion

            #region LoadData

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();

                string sqlExpression = "SELECT * FROM Speciality";
                SqliteCommand command = new(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["id"].ToString());
                            var name = reader["name"].ToString();

                            university1.Specialitys.Add(new Speciality(id, name));
                        }
                    }
                }

                sqlExpression = "SELECT * FROM FormOfEducation";
                command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["id"].ToString());
                            var name = reader["name"].ToString();

                            university1.FormOfEducations.Add(new FormOfEducation(id, name));
                        }
                    }
                }

                sqlExpression = "SELECT * FROM Groups";
                command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["id"].ToString());
                            var name = reader["name"].ToString();
                            var specialitiesId = int.Parse(reader["Speciality_id"].ToString());
                            var formOfEducationId = int.Parse(reader["FormOfEducation_id"].ToString());

                            university1.Groups.Add(new Group(id, name, university1.Specialitys[specialitiesId - 1], university1.FormOfEducations[formOfEducationId - 1]));
                        }
                    }
                }

                sqlExpression = "SELECT * FROM Students";
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
                            var groupId = int.Parse(reader["Group_id"].ToString());
                            var tmp = new Student(id, lastName, firstName, middleName, birthDate);
                            university1.Groups[groupId - 1].Students.Add(tmp);
                        }
                    }
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
                GroupInfo = $"{_selectedGroup.Name} - {_selectedGroup.Name} - {_selectedGroup.Speciality.Name} - {_selectedGroup.FormOfEducation.Name}";
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
