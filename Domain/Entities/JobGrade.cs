namespace Domain.Entities;
using Domain.Entities;

// [Keyless]
public class JobGrade{
    // public int JobGradeId {get; set;}
    public string GradeLevel { get; set; }
    public int LowestSalary { get; set; }
    public int HighestSalary { get; set; }
}