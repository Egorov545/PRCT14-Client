using System;
using System.Collections.Generic;

namespace PRCT14_Client.Models;

public partial class Discipline
{
    public int DisciplineCode { get; set; }

    public string Name { get; set; } = null!;

    public string? Direction { get; set; }

    public virtual ICollection<AcademicLoad> AcademicLoads { get; set; } = new List<AcademicLoad>();
}
