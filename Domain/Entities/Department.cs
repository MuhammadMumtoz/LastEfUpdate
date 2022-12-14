namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;
public class Department
{
    [Key]
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    // public int ManagerId { get; set; }
    // public virtual Employee Manager { get; set; }
    public int LocationId { get; set; }
    public virtual Location Location { get; set; }
    public virtual List<JobHistory> JobHistories { get; set; }
    public virtual List<Employee> Employees { get; set; }

}