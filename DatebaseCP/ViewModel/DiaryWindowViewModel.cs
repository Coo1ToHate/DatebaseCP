using DatebaseCP.Models;
using DatebaseCP.ViewModel.Base;
using System.Data;
using DatebaseCP.Command;
using DatebaseCP.Utils;
using DatebaseCP.View;
using System.Windows;

namespace DatebaseCP.ViewModel
{
    class DiaryWindowViewModel : BaseViewModel
    {
        ADO ado = new ADO();
        private string _title;
        private Student _student;
        private DataTable _diarysTables;
        private DataRowView _selectedDataRow;


        public DiaryWindowViewModel(Student student)
        {
            _title = $"Аттестация студента {student.LastName} {student.FirstName}";
            _student = student;
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

        public DataTable DiarysTables
        {
            get => _diarysTables;
            set
            {
                _diarysTables = value;
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

        #region Command

        #region AddDiaryCommand

        private RelayCommand _addDiaryCommand;

        public RelayCommand AddDiaryCommand
        {
            get
            {
                return _addDiaryCommand ??= new RelayCommand(obj =>
                {
                    Diary newDiary = new Diary();

                    DiaryEditWindow diaryEditWindow = new DiaryEditWindow()
                    {
                        DataContext = new DiaryEditWindowViewModel(newDiary, _student.Id),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    diaryEditWindow.ShowDialog();

                    if (diaryEditWindow.DialogResult.Value)
                    {
                        ado.InsertDiary(newDiary);
                        DiarysTables = ado.GetAllDiaresForStudent(_student.Id);
                    }
                });
            }
        }

        #endregion

        #region EditDiaryCommand

        private RelayCommand _editDiaryCommand;

        public RelayCommand EditDiaryCommand
        {
            get
            {
                return _editDiaryCommand ??= new RelayCommand(obj =>
                {
                    Diary upDiary = ado.GetDiary(int.Parse(SelectedDataRow.Row.ItemArray[0].ToString()));

                    DiaryEditWindow diaryEditWindow = new DiaryEditWindow()
                    {
                        DataContext = new DiaryEditWindowViewModel(upDiary, _student.Id),
                        Owner = obj as Window,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    diaryEditWindow.ShowDialog();

                    if (diaryEditWindow.DialogResult.Value)
                    {
                        ado.UpdateDiary(upDiary);
                        DiarysTables = ado.GetAllDiaresForStudent(_student.Id);
                    }
                }, obj => SelectedDataRow != null);
            }
        }

        #endregion

        #region DeleteDiaryCommand

        private RelayCommand _deleteDiaryCommand;

        public RelayCommand DeleteDiaryCommand
        {
            get
            {
                return _deleteDiaryCommand ??= new RelayCommand(obj =>
                {
                    ado.DeleteDiary(ado.GetDiary(int.Parse(SelectedDataRow.Row.ItemArray[0].ToString())));
                    DiarysTables = ado.GetAllDiaresForStudent(_student.Id);
                }, obj => SelectedDataRow != null);
            }
        }

        #endregion

        #endregion

    }
}
