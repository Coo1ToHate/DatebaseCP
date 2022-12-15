using System;
using System.Collections.ObjectModel;
using DatebaseCP.Models;
using Microsoft.Data.Sqlite;

namespace DatebaseCP.Utils
{
    internal class ADO
    {
        private readonly string _dbFileName = "data.db";

        #region Specialities

        public ObservableCollection<Speciality> GetAllSpecialities()
        {
            string sql = "SELECT * FROM Speciality";

            ObservableCollection<Speciality> result = new ObservableCollection<Speciality>();

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["id"].ToString());
                            var name = reader["Name"].ToString();

                            result.Add(new Speciality(id, name));
                        }
                    }
                }
            }
            return result;
        }

        public Speciality GetSpeciality(int id)
        {
            Speciality result = new Speciality();

            string sql = @"SELECT * FROM Speciality WHERE id = @specialityId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter specialityParameter = new SqliteParameter("@specialityId", id);
                command.Parameters.Add(specialityParameter);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var name = reader["Name"].ToString();
                            result.Id = id;
                            result.Name = name;
                        }
                    }
                    else
                    {
                        result = null;
                    }
                }
            }
            return result;
        }

        public void InsertSpeciality(Speciality speciality)
        {
            string sql = @"INSERT INTO Speciality (Name) VALUES (@name); SELECT last_insert_rowid();";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);

                SqliteParameter nameParameter = new SqliteParameter("@name", speciality.Name);
                command.Parameters.Add(nameParameter);

                var id = (long)command.ExecuteScalar();
                speciality.Id = (int)id;
            }
        }

        public void UpdateSpeciality(Speciality speciality)
        {
            string sql = @"UPDATE Speciality SET Name = @name WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", speciality.Id);
                command.Parameters.Add(idParameter);
                SqliteParameter nameParameter = new SqliteParameter("@name", speciality.Name);
                command.Parameters.Add(nameParameter);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteSpeciality(Speciality speciality)
        {
            string sql = @"DELETE FROM Speciality WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", speciality.Id);
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region FormOfEducation

        public ObservableCollection<FormOfEducation> GetAllFormOfEducation()
        {
            string sql = "SELECT * FROM FormOfEducation";

            ObservableCollection<FormOfEducation> result = new ObservableCollection<FormOfEducation>();

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["id"].ToString());
                            var name = reader["Name"].ToString();

                            result.Add(new FormOfEducation(id, name));
                        }
                    }
                }
            }
            return result;
        }

        public FormOfEducation GetsFormOfEducation(int id)
        {
            FormOfEducation result = new FormOfEducation();

            string sql = @"SELECT * FROM FormOfEducation WHERE id = @formOfEducationId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter formOfEducationIdParameter = new SqliteParameter("@formOfEducationId", id);
                command.Parameters.Add(formOfEducationIdParameter);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var name = reader["Name"].ToString();
                            result.Id = id;
                            result.Name = name;
                        }
                    }
                    else
                    {
                        result = null;
                    }
                }
            }
            return result;
        }

        public void InsertFormOfEducation(FormOfEducation formOfEducation)
        {
            string sql = @"INSERT INTO FormOfEducation (Name) VALUES (@name); SELECT last_insert_rowid();";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);

                SqliteParameter nameParameter = new SqliteParameter("@name", formOfEducation.Name);
                command.Parameters.Add(nameParameter);
                
                var id = (long)command.ExecuteScalar();
                formOfEducation.Id = (int)id;
            }
        }

        public void UpdateFormOfEducation(FormOfEducation formOfEducation)
        {
            string sql = @"UPDATE FormOfEducation SET Name = @name WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", formOfEducation.Id);
                command.Parameters.Add(idParameter);
                SqliteParameter nameParameter = new SqliteParameter("@name", formOfEducation.Name);
                command.Parameters.Add(nameParameter);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteFormOfEducation(FormOfEducation formOfEducation)
        {
            string sql = @"DELETE FROM FormOfEducation WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", formOfEducation.Id);
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }
        
        #endregion

        #region Groups

        public ObservableCollection<Group> GetAllGroup()
        {
            ObservableCollection<Group> result = new ObservableCollection<Group>();

            string sql = @"SELECT * FROM Groups";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var id = int.Parse(reader["id"].ToString());
                            var name = reader["Name"].ToString();
                            var specialitiesId = int.Parse(reader["Speciality_id"].ToString());
                            var formOfEducationId = int.Parse(reader["FormOfEducation_id"].ToString());

                            result.Add(new Group(id, name, GetSpeciality(specialitiesId), GetsFormOfEducation(formOfEducationId)));
                        }
                    }
                }
            }

            return result;
        }

        public Group GetGroup(int id)
        {
            Group result = new Group();

            string sql = @"SELECT * FROM Groups WHERE id = @groupId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter formOfEducationIdParameter = new SqliteParameter("@groupId", id);
                command.Parameters.Add(formOfEducationIdParameter);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var name = reader["Name"].ToString();
                            var specialitiesId = int.Parse(reader["Speciality_id"].ToString());
                            var formOfEducationId = int.Parse(reader["FormOfEducation_id"].ToString());

                            result.Id = id;
                            result.Name = name;
                            result.Speciality = GetSpeciality(specialitiesId);
                            result.FormOfEducation = GetsFormOfEducation(formOfEducationId);
                        }
                    }
                }
            }

            return result;
        }

        public void InsertGroup(Group group)
        {
            string sql = @"INSERT INTO Groups (Name, Speciality_id, FormOfEducation_id) VALUES (@name, @speciality_id, @formOfEducation_id); SELECT last_insert_rowid();";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);

                SqliteParameter nameParameter = new SqliteParameter("@name", group.Name);
                command.Parameters.Add(nameParameter);
                SqliteParameter specialityParameter = new SqliteParameter("@speciality_id", group.Speciality.Id);
                command.Parameters.Add(specialityParameter);
                SqliteParameter formOfEducationParameter =
                    new SqliteParameter("@formOfEducation_id", group.FormOfEducation.Id);
                command.Parameters.Add(formOfEducationParameter);

                var id = (long)command.ExecuteScalar();
                group.Id = (int)id;
            }
        }

        public void UpdateGroup(Group group)
        {
            string sql = @"UPDATE Groups SET Name = @name, Speciality_id = @speciality_id, FormOfEducation_id = @formOfEducation_id WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", group.Id);
                command.Parameters.Add(idParameter);
                SqliteParameter nameParameter = new SqliteParameter("@name", group.Name);
                command.Parameters.Add(nameParameter);
                SqliteParameter specialityParameter = new SqliteParameter("@speciality_id", group.Speciality.Id);
                command.Parameters.Add(specialityParameter);
                SqliteParameter formOfEducationParameter =
                    new SqliteParameter("@formOfEducation_id", group.FormOfEducation.Id);
                command.Parameters.Add(formOfEducationParameter);
                
                command.ExecuteNonQuery();
            }
        }

        public void DeleteGroup(Group group)
        {
            string sql = @"DELETE FROM Groups WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", group.Id);
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Students

        //TODO
        public ObservableCollection<Student> GetAllStudents()
        {

            ObservableCollection<Student> result = new ObservableCollection<Student>();

            string sql = @"SELECT * FROM Students";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);

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

                            result.Add(new Student(id, lastName, firstName, middleName, birthDate));
                        }
                    }
                }
            }

            return result;

        }

        public ObservableCollection<Student> GetStudentsInGroup(Group group)
        {
            ObservableCollection<Student> result = new ObservableCollection<Student>();

            string sql = @"SELECT * FROM Students WHERE Group_id = @groupId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@groupId", group.Id);
                command.Parameters.Add(idParameter);

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

                            result.Add(new Student(id, lastName, firstName, middleName, birthDate));
                        }
                    }
                }
            }

            return result;
        }

        public Student GetStudent(int id)
        {
            Student result = new Student();

            string sql = @"SELECT * FROM Students WHERE id = @studentId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@studentId", id);
                command.Parameters.Add(idParameter);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var lastName = reader["LastName"].ToString();
                            var firstName = reader["FirstName"].ToString();
                            var middleName = reader["MiddleName"].ToString();
                            var birthDate = DateTime.Parse(reader["BirthDate"].ToString());

                            result.Id = id;
                            result.LastName = lastName;
                            result.FirstName = firstName;
                            result.MiddleName = middleName;
                            result.BirthDate = birthDate;
                        }
                    }
                }
            }
            return result;
        }

        public void InsertStudent(Student student, Group group)
        {
            string sql =
                @"INSERT INTO Students (LastName, FirstName, MiddleName, BirthDate, Group_id) VALUES (@lastName, @firstName, @middleName, @birthDate, @groupId); SELECT last_insert_rowid();";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);

                SqliteParameter lastNameParameter = new SqliteParameter("@lastName", student.LastName);
                command.Parameters.Add(lastNameParameter);
                SqliteParameter firstNameParameter = new SqliteParameter("@firstName", student.FirstName);
                command.Parameters.Add(firstNameParameter);
                SqliteParameter middleNameParameter = new SqliteParameter("@middleName", student.MiddleName);
                command.Parameters.Add(middleNameParameter);
                SqliteParameter birthDateParameter = new SqliteParameter("@birthDate", student.BirthDate);
                command.Parameters.Add(birthDateParameter);
                SqliteParameter groupIdParameter = new SqliteParameter("@groupId", group.Id);
                command.Parameters.Add(groupIdParameter);

                var id = (long)command.ExecuteScalar();
                student.Id = (int)id;
            }
        }

        public void UpdateStudent(Student student, Group group)
        {
            string sql =
                @"UPDATE Students SET LastName = @lastName, FirstName = @firstName, MiddleName = @middleName, BirthDate = @birthDate, Group_id = @groupId WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);

                SqliteParameter lastNameParameter = new SqliteParameter("@lastName", student.LastName);
                command.Parameters.Add(lastNameParameter);
                SqliteParameter firstNameParameter = new SqliteParameter("@firstName", student.FirstName);
                command.Parameters.Add(firstNameParameter);
                SqliteParameter middleNameParameter = new SqliteParameter("@middleName", student.MiddleName);
                command.Parameters.Add(middleNameParameter);
                SqliteParameter birthDateParameter = new SqliteParameter("@birthDate", student.BirthDate);
                command.Parameters.Add(birthDateParameter);
                SqliteParameter groupIdParameter = new SqliteParameter("@groupId", group.Id);
                command.Parameters.Add(groupIdParameter);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(Student student)
        {
            string sql = @"DELETE FROM Students WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", student.Id);
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
