namespace PRCT14_Client.Models
{
    public partial class AcademicLoadEditDTO
    {
        public int AcademLoadCode { get; set; }
        public int TeacherCode { get; set; }
        public int DisciplineCode { get; set; }
        public int Group { get; set; }
        public int Semester { get; set; }
        public int NumberOfHours { get; set; }
    }
}