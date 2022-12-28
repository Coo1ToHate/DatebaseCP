using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class ReportStudentWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();
        private string _title;
        private Student _student;
        private string _lastName;
        private string _firstName;
        private string _middleName;
        private DateTime _bDate;
        private string _group;
        private double _score;
        private DataTable _diarysTables;

        public ReportStudentWindowViewModel(Student student)
        {
            _title = $"Отчет о студенте - {student.LastName} {student.FirstName}";
            _student = student;
            _lastName = student.LastName;
            _firstName = student.FirstName;
            _middleName = student.MiddleName;
            _bDate = student.BirthDate;
            _group = ado.GetGroup(student.GroupId).Name;
            _score = ado.ScoreStudent(student.Id);
            _diarysTables = ado.GetAllDiaresForStudent(student.Id);
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

        public Student Student
        {
            get => _student;
            set
            {
                _student = value;
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

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set {
                _middleName = value;
                OnPropertyChanged();
            }
        }

        public DateTime BDate
        {
            get => _bDate;
            set
            {
                _bDate = value;
                OnPropertyChanged();
            }
        }

        public string Group
        {
            get => _group;
            set {
                _group = value;
                OnPropertyChanged();
            }
        }

        public double Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        public DataTable DiarysTables
        {
            get => _diarysTables;
            set
            {
                _diarysTables = value;
                OnPropertyChanged();
            }
        }

        #region Command

        #region CreateReportCommand

        private RelayCommand _createReportCommand;

        public RelayCommand CreateReportCommand
        {
            get
            {
                return _createReportCommand ??= new RelayCommand(obj =>
                {
                    Create();
                });
            }
        }

        #endregion

        #endregion

        private void Create()
        {
            try
            {
                string path = "Report.html";

                using (StreamWriter streamWriter = File.CreateText(path))
                {
                    streamWriter.WriteLine("<html>");
                    streamWriter.WriteLine("<head>");
                    streamWriter.WriteLine("    <title>Отчет</title>");
                    streamWriter.WriteLine("    <meta http-equiv=\"Content-Type\" content\"text/html;charset=utf-8\" />");
                    streamWriter.WriteLine(@"     <style type=""text/css"">
                                                       table {
                                                            margin-top: 5px;
                                                            border-collapse: collapse;
                                                            border: 1px solid #000;
                                                            width: 80%;
                                                        }
                                                        thead{
                                                            text-align: center;
                                                        }
                                                        td,th{
                                                            padding: 5px;
                                                            border: 1px solid #000;
                                                        }
                                                  </style>");
                    streamWriter.WriteLine("</head>");
                    streamWriter.WriteLine("<body>");
                    streamWriter.WriteLine($"<H1>Отчет по студенте - {Student.LastName} {Student.FirstName}</h1>");
                    streamWriter.WriteLine($"<p>Фамилия - {Student.LastName}");
                    streamWriter.WriteLine($"<p>Имя - {Student.FirstName}");
                    streamWriter.WriteLine($"<p>Отчество - {Student.MiddleName}");
                    streamWriter.WriteLine($"<p>Дата рождения - {Student.BirthDate:dd.mm.yyyy}");
                    streamWriter.WriteLine($"<p>Группа - {Group}");
                    streamWriter.WriteLine($"<p>Средний бал - {Score:F2}");
                    streamWriter.WriteLine("<table>");
                    streamWriter.WriteLine("<tr> <td>ID</td> <td>Дата</td> <td>Предмет</td> <td>Аттестация</td> <td>Оценка</td> <td>Преподаватель</td>");
                    foreach (DataRow r in DiarysTables.Rows)
                    {
                        streamWriter.WriteLine("<tr> ");
                        for (int i = 0; i < r.ItemArray.Length; i++)
                        {
                            if (i != 5)
                            {
                                if (i == 1)
                                {
                                    streamWriter.WriteLine($"<td>{r[i]:dd.mm.yyyy}</td>");
                                }
                                else
                                {
                                    streamWriter.WriteLine($"<td>{r[i]}</td>");
                                }
                            }
                        }
                        streamWriter.WriteLine("</td>");
                    }
                    streamWriter.WriteLine("</table>");
                    streamWriter.WriteLine("</body>");
                    streamWriter.WriteLine("</html>");
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true
                });
            }
            catch
            {
            }
        }

    }
}
