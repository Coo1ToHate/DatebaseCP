using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using DatebaseCP.Command;
using DatebaseCP.Models;
using DatebaseCP.Utils;
using DatebaseCP.ViewModel.Base;

namespace DatebaseCP.ViewModel
{
    internal class ReportGroupWindowViewModel : BaseViewModel
    {
        private ADO ado = new ADO();
        private string _title;
        private Group _group;
        private string _speciality;
        private string _formOfEducations;
        private int _countStudents;
        private ObservableCollection<Student> _students;
        private double _score;
        private Teacher _curator;
        private string _curatorName;

        public ReportGroupWindowViewModel(Group group)
        {
            Title = $"Отчет о группе - {group.Name}";
            Group = group;
            Speciality = ado.GetSpeciality(group.SpecialityId).Name;
            FormOfEducations = ado.GetFormOfEducation(group.FormOfEducationId).Name;
            CountStudents = ado.CountStudentsInGroup(group.Id);
            Students = ado.GetStudentsInGroup(group.Id);
            Score = ado.ScoreGroup(group.Id);
            _curator = ado.GetTeacher(group.CuratorId);
            CuratorName = $"{_curator.LastName} {_curator.FirstName} {_curator.MiddleName}";
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

        public Group Group
        {
            get => _group;
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        public string Speciality
        {
            get => _speciality;
            set
            {
                _speciality = value;
                OnPropertyChanged();
            }
        }

        public string FormOfEducations
        {
            get => _formOfEducations;
            set
            {
                _formOfEducations = value;
                OnPropertyChanged();
            }
        }

        public int CountStudents
        {
            get => _countStudents;
            set
            {
                _countStudents = value;
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

        public double Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        public string CuratorName
        {
            get => _curatorName;
            set
            {
                _curatorName = value;
                OnPropertyChanged();
            }
        }

        #region Commands

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
                    streamWriter.WriteLine($"<H1>Отчет по группе - {Group.Name}</h1>");
                    streamWriter.WriteLine($"<p>Специализация - {Speciality}");
                    streamWriter.WriteLine($"<p>Форма обучения - {FormOfEducations}");
                    streamWriter.WriteLine($"<p>Количество студентов - {CountStudents}");
                    streamWriter.WriteLine($"<p>Куратор - {CuratorName}");
                    streamWriter.WriteLine($"<p>Средний бал - {Score:F2}");
                    streamWriter.WriteLine("<table>");
                    streamWriter.WriteLine("<tr> <td>ID</td> <td>Фамилия</td> <td>Имя</td> <td>Отчество</td> <td>Дата рождения</td>");
                    foreach (var s in Students)
                    {
                        streamWriter.WriteLine($"<tr> <td>{s.Id}</td> <td>{s.LastName}</td> <td>{s.LastName}</td> <td>{s.MiddleName}</td> <td>{s.BirthDate:dd.MM.yyyy}</td>");
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
