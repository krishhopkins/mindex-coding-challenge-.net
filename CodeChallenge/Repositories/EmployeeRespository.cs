using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _employeeContext.Employees.Load(); // this fixes the direct reports coming back as null
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            Employee e =  _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
            return e;
        }

        public int CountNumberOfReports(Employee employee)
        {
            int numberOfReports = 0;

            if (employee == null) {
                return numberOfReports;
            }
            if (employee.DirectReports == null) {
                return numberOfReports;
            }
            numberOfReports += employee.DirectReports.Count;
            foreach (Employee e in employee.DirectReports) {
                if (e != null)
                {
                    numberOfReports += CountNumberOfReports(e);
                }
            }

            return numberOfReports;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
