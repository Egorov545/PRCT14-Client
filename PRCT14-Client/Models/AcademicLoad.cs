using System;
using System.Collections.Generic;

namespace PRCT14_Client.Models;

public partial class AcademicLoad
{
    public int AcademLoadCode { get; set; }

    public int TeacherCode { get; set; }

    public int DisciplineCode { get; set; }

    public int Group { get; set; }

    public int Semester { get; set; }

    public int NumberOfHours { get; set; }

    public virtual Discipline DisciplineCodeNavigation { get; set; } = null!;

    public virtual Teacher TeacherCodeNavigation { get; set; } = null!;
}
