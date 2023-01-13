namespace DatebaseCP.Models
{
    internal class TeacherPost
    {
        public int TeacherId { get; set; }
        public int PostId { get; set; }

        public TeacherPost()
        {
        }

        public TeacherPost(int teacherId, int postId)
        {
            TeacherId = teacherId;
            PostId = postId;
        }
    }
}
