using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOptional(e => e.Manager)
            .WithMany()
            .HasForeignKey(m => m.ManagerId);
        modelBuilder.Entity<JobGrade>(
          eb =>
          {
              eb.HasNoKey();
              eb.ToView("View_JobGrade");
          });

        modelBuilder.Entity<JobHistory>()
   .HasKey(bc => new { bc.JobId, bc.EmployeeId });

        modelBuilder.Entity<JobHistory>()
            .HasOne(bc => bc.Job)
            .WithMany(b => b.JobHistories)
            .HasForeignKey(bc => bc.JobId);
        modelBuilder.Entity<JobHistory>()
            .HasOne(bc => bc.Employee)
            .WithMany(c => c.JobHistories)
            .HasForeignKey(bc => bc.EmployeeId);

        // modelBuilder.Entity<BookCategory>()
        //         .HasKey(bc => new { bc.BookId, bc.CategoryId });
        // modelBuilder.Entity<BookCategory>()
        //     .HasOne(bc => bc.Book)
        //     .WithMany(b => b.BookCategories)
        //     .HasForeignKey(bc => bc.BookId);
        // modelBuilder.Entity<BookCategory>()
        //     .HasOne(bc => bc.Category)
        //     .WithMany(c => c.BookCategories)
        // .HasForeignKey(bc => bc.CategoryId);

        // modelBuilder.Entity<JobHistory>()
        //     .HasPrincipalKey(e => e.EmployeeId);
    }
    #endregion

    public DbSet<Country> Countries { get; set; }
    public DbSet<Department> Departments { get; set; }

    public DbSet<JobGrade> JobGrades { get; set; }

    public DbSet<JobHistory> JobHistories { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Region> Regions { get; set; }
}