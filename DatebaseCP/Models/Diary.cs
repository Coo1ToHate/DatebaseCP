using System;

namespace DatebaseCP.Models
{
    class Diary
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
        public int LessonId { get; set; }
        public int TypeId { get; set; }

        public Diary()
        {
        }

        public Diary(int studentId, int teacherId, DateTime date, int score, int lessonId, int typeId)
        {
            StudentId = studentId;
            TeacherId = teacherId;
            Date = date;
            Score = score;
            LessonId = lessonId;
            TypeId = typeId;
        }

        public Diary(int id, int studentId, int teacherId, DateTime date, int score, int lessonId, int typeId)
        {
            Id = id;
            StudentId = studentId;
            TeacherId = teacherId;
            Date = date;
            Score = score;
            LessonId = lessonId;
            TypeId = typeId;
        }
    }
}
