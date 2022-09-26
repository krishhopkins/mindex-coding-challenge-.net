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
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _context;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext context)
        {
            _context = context;
            _context.Compensations.Load();
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            _context.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetById(string id)
        {
            Compensation c =  _context.Compensations.SingleOrDefault(c => c.Id == id);
            return c;
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
