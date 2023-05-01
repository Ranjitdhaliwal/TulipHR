using Microsoft.EntityFrameworkCore;
using TulipHR.API.DbContexts;
using TulipHR.API.Entities;

namespace TulipHR.API.Services
{
    public class OrgInfoRepository : IOrgInfoRepository
    {
        private readonly OrganizationContext _context;
        public OrgInfoRepository(OrganizationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
                
        public async Task<bool> PositionExistsAsync(int positionId)
        {
            return await _context.Positions.AnyAsync(c => c.Id == positionId);
        }
        
        public void DeletePositions(Position positions)
        {
            _context.Positions.Remove(positions);
        }

        public void AddPosition(Position position)
        {
            _context.Positions.Add(position);
        }
        public async Task<Position?> GetPositionAsync(int positionId)
        {
            return await _context.Positions
                .Where(p => p.Id == positionId)
               .FirstOrDefaultAsync();
        }
        public  IQueryable<Position> GetQueryablePositions()
        {
            return _context.Positions.AsQueryable();
        }

        public IQueryable<Employee> GetQueryableEmployees()
        {
            return _context.Employees.AsQueryable();
        }

        public async Task<Employee?> GetEmployeeAssignedAsync(int positionId)
        {
            return await _context.Employees
                .Where(p => p.PositionId == positionId)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeAsync()
        {   
            return await _context.Employees.ToListAsync();
        }
        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees
                .Where(p => p.Id == employeeId)
               .FirstOrDefaultAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
