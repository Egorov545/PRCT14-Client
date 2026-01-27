using System;
using System.Collections.Generic;

namespace PRCT14_Client.Models;

public partial class Teacher
{
    public int ServiceNumber { get; set; }

    public string Surname { get; set; } = null!;

    public string? Post { get; set; }

    public string? Department { get; set; }

    public int? Experience { get; set; }

    public virtual ICollection<AcademicLoad> AcademicLoads { get; set; } = new List<AcademicLoad>();
}
