using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.ViewModel.Base;
using System.Data;
using System.Linq;
using DatebaseCP.Command;
using System.Diagnostics;
using System.IO;

namespace DatebaseCP.ViewModel
{
    internal class ReportTeacherWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();
        private string _title;
        private Teacher _teacher;
        private TeacherTitle _teacherTitle;
        private string _post;
        private string _degree;
        private string _lesson;
        private double _score;
        private DataTable _diarysTables;

        public ReportTeacherWindowViewModel(Teacher teacher)
        {
            Title = $"Отчет о преподавателе - {teacher.LastName} {teacher.FirstName}";
            Teacher = teacher;
            TeacherTitle = ado.GetTeacherTitle(teacher.TitleId);
            Post = string.Join(", ", teacher.Posts.Select(p => p.Name));
            Degree = string.Join(", ", teacher.Degrees.Select(d => d.Name));
            Lesson = string.Join(", ", teacher.Lessons.Select(l => l.Name));
            Score = ado.ScoreTeacher(teacher.Id);
            DiarysTables = ado.GetAllDiaresForTeacher(teacher.Id);
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

        public Teacher Teacher
        {
            get => _teacher;
            set
            {
                _teacher = value;
                OnPropertyChanged();
            }
        }

        public TeacherTitle TeacherTitle
        {
            get => _teacherTitle;
            set
            {
                _teacherTitle = value;
                OnPropertyChanged();
            }
        }

        public string Post
        {
            get => _post;
            set
            {
                _post = value;
                OnPropertyChanged();
            }
        }

        public string Degree
        {
            get => _degree;
            set
            {
                _degree = value;
                OnPropertyChanged();
            }
        }

        public string Lesson
        {
            get => _lesson;
            set
            {
                _lesson = value;
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
                    streamWriter.WriteLine($"<H1>Отчет по преподавателе - {Teacher.LastName} {Teacher.FirstName}</h1>");
                    streamWriter.WriteLine($"<p>Фамилия - {Teacher.LastName}");
                    streamWriter.WriteLine($"<p>Имя - {Teacher.FirstName}");
                    streamWriter.WriteLine($"<p>Отчество - {Teacher.MiddleName}");
                    streamWriter.WriteLine($"<p>Дата рождения - {Teacher.BirthDate:dd.MM.yyyy}");
                    streamWriter.WriteLine($"<p>Звание - {TeacherTitle}");
                    streamWriter.WriteLine($"<p>Должность - {Post}");
                    streamWriter.WriteLine($"<p>Ученая степень - {Degree}");
                    streamWriter.WriteLine($"<p>Предметы - {Lesson}");
                    streamWriter.WriteLine($"<p>Средний бал - {Score:F2}");
                    streamWriter.WriteLine("<table>");
                    streamWriter.WriteLine("<tr> <td>ID</td> <td>Дата</td> <td>Предмет</td> <td>Аттестация</td> <td>Оценка</td> <td>Студент</td>");
                    foreach (DataRow r in DiarysTables.Rows)
                    {
                        streamWriter.WriteLine("<tr> ");
                        for (int i = 0; i < r.ItemArray.Length; i++)
                        {
                            if (i != 6)
                            {
                                if (i == 1)
                                {
                                    streamWriter.WriteLine($"<td>{r[i]:dd.MM.yyyy}</td>");
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
