namespace DatebaseCP.Models
{
    internal class TeacherDegree
    {
        public int TeacherId { get; set; }
        public int DegreeId { get; set; }
        
        public TeacherDegree()
        {
        }

        public TeacherDegree(int teacherId, int degreeId)
        {
            TeacherId = teacherId;
            DegreeId = degreeId;
        }
    }
}
