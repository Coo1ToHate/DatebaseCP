namespace DatebaseCP.Models
{
    internal class TeacherLesson
    {
        public int TeacherId { get; set; }
        public int LessonId { get; set; }

        public TeacherLesson()
        {
        }

        public TeacherLesson(int teacherId, int lessonId)
        {
            TeacherId = teacherId;
            LessonId = lessonId;
        }
    }
}
