using TulipHR.API.Entities;

namespace TulipHR.API.Services
{
    public interface IOrgInfoRepository
    {
        Task<bool> PositionExistsAsync(int positionId);
        void DeletePositions(Position positions);
        void AddPosition(Position position);
        Task<Position?> GetPositionAsync(int positionId);
        IQueryable<Position> GetQueryablePositions();
        IQueryable<Employee> GetQueryableEmployees();
        Task<Employee?> GetEmployeeAssignedAsync(int positionId);
        Task<IEnumerable<Employee>> GetEmployeeAsync();
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
        void AddEmployee(Employee position);
        Task<bool> SaveChangesAsync();

    }
}
