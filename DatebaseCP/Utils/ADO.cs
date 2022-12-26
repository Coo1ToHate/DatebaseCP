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

        public int CountGroupsWithSpecialities(int specialityId)
        {
            int result;

            string sql = @"SELECT COUNT(*) FROM Groups WHERE Speciality_id = @specialityId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter specialityIdParameter = new SqliteParameter("@specialityId", specialityId);
                command.Parameters.Add(specialityIdParameter);

                result = (int)(long)command.ExecuteScalar();
            }

            return result;
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

        public FormOfEducation GetFormOfEducation(int id)
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

        public int CountGroupsWithFormOfEducation(int formOfEducationId)
        {
            int result;

            string sql = @"SELECT COUNT(*) FROM Groups WHERE FormOfEducation_id = @formOfEducationId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter formOfEducationIdParameter = new SqliteParameter("@formOfEducationId", formOfEducationId);
                command.Parameters.Add(formOfEducationIdParameter);

                result = (int)(long)command.ExecuteScalar();
            }

            return result;
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

                            result.Add(new Group(id, name, specialitiesId, formOfEducationId));
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
                SqliteParameter groupIdParameter = new SqliteParameter("@groupId", id);
                command.Parameters.Add(groupIdParameter);
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
                            result.SpecialityID = specialitiesId;
                            result.FormOfEducationID = formOfEducationId;
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
                SqliteParameter specialityIdParameter = new SqliteParameter("@speciality_id", group.SpecialityID);
                command.Parameters.Add(specialityIdParameter);
                SqliteParameter formOfEducationIdParameter =
                    new SqliteParameter("@formOfEducation_id", group.FormOfEducationID);
                command.Parameters.Add(formOfEducationIdParameter);

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
                SqliteParameter specialityIdParameter = new SqliteParameter("@speciality_id", group.SpecialityID);
                command.Parameters.Add(specialityIdParameter);
                SqliteParameter formOfEducationIdParameter =
                    new SqliteParameter("@formOfEducation_id", group.FormOfEducationID);
                command.Parameters.Add(formOfEducationIdParameter);
                
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

        public int CountStudentsInGroup(int id)
        {
            int result;

            string sql = @"SELECT COUNT(*) FROM Students WHERE Group_id = @groupId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter formOfEducationIdParameter = new SqliteParameter("@groupId", id);
                command.Parameters.Add(formOfEducationIdParameter);

                result = (int)(long)command.ExecuteScalar();
            }

            return result;
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

                            result.Add(new Student(id, lastName, firstName, middleName, birthDate, groupId));
                        }
                    }
                }
            }

            return result;

        }

        public ObservableCollection<Student> GetStudentsInGroup(int groupId)
        {
            ObservableCollection<Student> result = new ObservableCollection<Student>();

            string sql = @"SELECT * FROM Students WHERE Group_id = @groupId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@groupId", groupId);
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
                            groupId = int.Parse(reader["Group_id"].ToString());

                            result.Add(new Student(id, lastName, firstName, middleName, birthDate, groupId));
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
                            var groupId = int.Parse(reader["Group_id"].ToString());

                            result.Id = id;
                            result.LastName = lastName;
                            result.FirstName = firstName;
                            result.MiddleName = middleName;
                            result.BirthDate = birthDate;
                            result.GroupId = groupId;
                        }
                    }
                }
            }
            return result;
        }

        public void InsertStudent(Student student)
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
                SqliteParameter groupIdParameter = new SqliteParameter("@groupId", student.GroupId);
                command.Parameters.Add(groupIdParameter);

                var id = (long)command.ExecuteScalar();
                student.Id = (int)id;
            }
        }

        public void UpdateStudent(Student student)
        {
            string sql =
                @"UPDATE Students SET LastName = @lastName, FirstName = @firstName, MiddleName = @middleName, BirthDate = @birthDate, Group_id = @groupId WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);       

                SqliteParameter idParameter = new SqliteParameter("@id", student.Id);
                command.Parameters.Add(idParameter);
                SqliteParameter lastNameParameter = new SqliteParameter("@lastName", student.LastName);
                command.Parameters.Add(lastNameParameter);
                SqliteParameter firstNameParameter = new SqliteParameter("@firstName", student.FirstName);
                command.Parameters.Add(firstNameParameter);
                SqliteParameter middleNameParameter = new SqliteParameter("@middleName", student.MiddleName);
                command.Parameters.Add(middleNameParameter);
                SqliteParameter birthDateParameter = new SqliteParameter("@birthDate", student.BirthDate);
                command.Parameters.Add(birthDateParameter);
                SqliteParameter groupIdParameter = new SqliteParameter("@groupId", student.GroupId);
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

        #region Post

        public ObservableCollection<Post> GetAllPosts()
        {
            ObservableCollection<Post> result = new ObservableCollection<Post>();

            string sql = @"SELECT * FROM Post";

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

                            result.Add(new Post(id, name));
                        }
                    }
                }
            }

            return result;
        }

        public Post GetPost(int id)
        {
            Post result = new Post();

            string sql = @"SELECT * FROM Post WHERE id = @postId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter postIdParameter = new SqliteParameter("@postId", id);
                command.Parameters.Add(postIdParameter);

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

        public void InsertPost(Post post)
        {
            string sql = @"INSERT INTO Post (Name) VALUES (@name); SELECT last_insert_rowid();";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);

                SqliteParameter nameParameter = new SqliteParameter("@name", post.Name);
                command.Parameters.Add(nameParameter);

                var id = (long)command.ExecuteScalar();
                post.Id = (int)id;
            }
        }

        public void UpdatePost(Post post)
        {
            string sql = @"UPDATE Post SET Name = @name WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", post.Id);
                command.Parameters.Add(idParameter);
                SqliteParameter nameParameter = new SqliteParameter("@name", post.Name);
                command.Parameters.Add(nameParameter);
                command.ExecuteNonQuery();
            }
        }

        public void DeletePost(Post post)
        {
            string sql = @"DELETE FROM Post WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", post.Id);
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        public int CountTeacherWithPost(int postId)
        {
            int result;

            string sql = @"SELECT COUNT(*) FROM TeacherPost WHERE Post_id = @postId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter postIdParameter = new SqliteParameter("@postId", postId);
                command.Parameters.Add(postIdParameter);

                result = (int)(long)command.ExecuteScalar();
            }

            return result;
        }

        #endregion

        #region Degree

        public ObservableCollection<TeacherDegree> GetAllTeacherDegree()
        {
            ObservableCollection<TeacherDegree> result = new ObservableCollection<TeacherDegree>();

            string sql = @"SELECT * FROM TeacherDegree";

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

                            result.Add(new TeacherDegree(id, name));
                        }
                    }
                }
            }

            return result;
        }

        public TeacherDegree GetTeacherDegree(int id)
        {
            TeacherDegree result = new TeacherDegree();

            string sql = @"SELECT * FROM TeacherDegree WHERE id = @degreeId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@degreeId", id);
                command.Parameters.Add(idParameter);

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

        public void InsertTeacherDegree(TeacherDegree teacherDegree)
        {
            string sql = @"INSERT INTO TeacherDegree (Name) VALUES (@name); SELECT last_insert_rowid();";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);

                SqliteParameter nameParameter = new SqliteParameter("@name", teacherDegree.Name);
                command.Parameters.Add(nameParameter);

                var id = (long)command.ExecuteScalar();
                teacherDegree.Id = (int)id;
            }
        }

        public void UpdateTeacherDegree(TeacherDegree teacherDegree)
        {
            string sql = @"UPDATE TeacherDegree SET Name = @name WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", teacherDegree.Id);
                command.Parameters.Add(idParameter);
                SqliteParameter nameParameter = new SqliteParameter("@name", teacherDegree.Name);
                command.Parameters.Add(nameParameter);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTeacherDegree(TeacherDegree teacherDegree)
        {
            string sql = @"DELETE FROM TeacherDegree WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", teacherDegree.Id);
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        public int CountTeacherWithDegree(int degreeId)
        {
            int result;

            string sql = @"SELECT COUNT(*) FROM Teachers WHERE TeachingDegree_id = @degreeId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@degreeId", degreeId);
                command.Parameters.Add(idParameter);

                result = (int)(long)command.ExecuteScalar();
            }

            return result;
        }

        #endregion

        #region Title

        public ObservableCollection<TeacherTitle> GetAllTeacherTitle()
        {
            ObservableCollection<TeacherTitle> result = new ObservableCollection<TeacherTitle>();

            string sql = @"SELECT * FROM TeacherTitle";

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

                            result.Add(new TeacherTitle(id, name));
                        }
                    }
                }
            }

            return result;
        }

        public TeacherTitle GetTeacherTitle(int id)
        {
            TeacherTitle result = new TeacherTitle();

            string sql = @"SELECT * FROM TeacherTitle WHERE id = @titleId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@titleId", id);
                command.Parameters.Add(idParameter);

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

        public void InsertTeacherTitle(TeacherTitle teacherTitle)
        {
            string sql = @"INSERT INTO TeacherTitle (Name) VALUES (@name); SELECT last_insert_rowid();";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);

                SqliteParameter nameParameter = new SqliteParameter("@name", teacherTitle.Name);
                command.Parameters.Add(nameParameter);

                var id = (long)command.ExecuteScalar();
                teacherTitle.Id = (int)id;
            }
        }

        public void UpdateTeacherTitle(TeacherTitle teacherTitle)
        {
            string sql = @"UPDATE TeacherTitle SET Name = @name WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", teacherTitle.Id);
                command.Parameters.Add(idParameter);
                SqliteParameter nameParameter = new SqliteParameter("@name", teacherTitle.Name);
                command.Parameters.Add(nameParameter);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTeacherTitle(TeacherTitle teacherTitle)
        {
            string sql = @"DELETE FROM TeacherTitle WHERE id = @id";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@id", teacherTitle.Id);
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        public int CountTeacherWithTitle(int titleId)
        {
            int result;

            string sql = @"SELECT COUNT(*) FROM Teachers WHERE TeachingTitle_id = @titleId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@titleId", titleId);
                command.Parameters.Add(idParameter);

                result = (int)(long)command.ExecuteScalar();
            }

            return result;
        }

        #endregion

        #region Teacher

        public ObservableCollection<Teacher> GetAllTeachers()
        {
            ObservableCollection<Teacher> result = new ObservableCollection<Teacher>();

            string sql = @"SELECT * FROM Teachers";

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
                            var titleId = int.Parse(reader["TeachingTitle_id"].ToString());
                            var degreeId = int.Parse(reader["TeachingDegree_id"].ToString());

                            result.Add(new Teacher(id, lastName, firstName, middleName, birthDate, titleId, degreeId));
                        }
                    }
                }
            }

            return result;
        }

        public Teacher GetTeacher(int id)
        {
            Teacher result = new Teacher();

            string sql = @"SELECT * FROM Teachers WHERE id = @teacherId";

            using (var connection = new SqliteConnection($"Data source={_dbFileName}"))
            {
                connection.Open();
                SqliteCommand command = new(sql, connection);
                SqliteParameter idParameter = new SqliteParameter("@teacherId", id);
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
                            var titleId = int.Parse(reader["TeachingTitle_id"].ToString());
                            var degreeId = int.Parse(reader["TeachingDegree_id"].ToString());

                            result.Id = id;
                            result.LastName = lastName;
                            result.FirstName = firstName;
                            result.MiddleName = middleName;
                            result.BirthDate = birthDate;
                            result.TitleId = titleId;
                            result.DegreeId = degreeId;
                        }
                    }
                }
            }
            return result;
        }

        #endregion


    }
}
