using Domain.Entities;
namespace Domain.Dtos;
public class AddJobGradeDto
{
    // public int JobGradeId { get; set; }

    public string GradeLevel { get; set; }
    public int LowestSalary { get; set; }
    public int HighestSalary { get; set; }
}