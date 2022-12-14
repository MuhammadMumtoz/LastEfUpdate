using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
public class EmployeeService
{
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;
    public EmployeeService(DataContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }
    public async Task<Response<List<GetEmployeeDto>>> GetEmployees()
    {
        var list = await _context.Employees.Select(s => new GetEmployeeDto()
        {
            EmployeeId = s.EmployeeId,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            PhoneNumber = s.PhoneNumber,
            HireDate = s.HireDate,
            JobTitle = s.Job.JobTitle,
            CommissionPct = s.CommissionPct,
            ManagerId = s.ManagerId,
            DepartmentName = s.Department.DepartmentName,
            FileName = s.File.FileName
        }).ToListAsync();
        return new Response<List<GetEmployeeDto>>(list.ToList());
    }

    public async Task<Response<AddEmployeeDto>> InsertEmployee(AddEmployeeDto employee)
    {
        var path = Path.Combine(_hostEnvironment.WebRootPath, "ProfileImages");
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        var filePath = Path.Combine(path, employee.File.FileName);
        using (var stream = File.Create(filePath))
        {
            await employee.File.CopyToAsync(stream);
        }
        var newEmployee = new Employee()
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            HireDate = employee.HireDate,
            JobId = employee.JobId,
            CommissionPct = employee.CommissionPct,
            ManagerId = employee.ManagerId,
            DepartmentId = employee.DepartmentId,
            FileName = employee.File.FileName
        };
        _context.Employees.Add(newEmployee);
        await _context.SaveChangesAsync();
        employee.EmployeeId = newEmployee.EmployeeId;
        return new Response<AddEmployeeDto>(employee);
    }

    public async Task<Response<AddEmployeeDto>> UpdateEmployee(AddEmployeeDto employee)
    {
        var find = await _context.Employees.FindAsync(employee.EmployeeId);
        find.FirstName = employee.FirstName;
        find.LastName = employee.LastName;
        find.Email = employee.Email;
        find.PhoneNumber = employee.PhoneNumber;
        find.HireDate = employee.HireDate;
        find.JobId = employee.JobId;
        find.CommissionPct = employee.CommissionPct;
        find.ManagerId = employee.ManagerId;
        find.DepartmentId = employee.DepartmentId;
        var updated = await _context.SaveChangesAsync();
        return new Response<AddEmployeeDto>(employee);
    }
    public async Task<Response<string>> DeleteEmployee(int id)
    {
        var find = await _context.Employees.FindAsync(id);
        _context.Remove(find);
        var response = await _context.SaveChangesAsync();
        if (response > 0)
            return new Response<string>("Object deleted successfully");
        return new Response<string>(HttpStatusCode.BadRequest, "Object not found");
    }

}