using Microsoft.EntityFrameworkCore;
using TulipHR.API.Entities;
using TulipHR.API.Models;

namespace TulipHR.API.DbContexts
{
    public class OrganizationContext : DbContext
    {
      public OrganizationContext(DbContextOptions<OrganizationContext> options) : base(options)
        {
        }

        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var Director = new Position {Id = 1, Title = "Director", Number = "1" };
            var seniorManager = new Position { Id = 2, Title = "Senior Manager", Number = "2", ManagerPositionId = Director.Id };
            var manager1 = new Position { Id = 3, Title = "Manager 1", Number = "3", ManagerPositionId = seniorManager.Id };
            var manager2 = new Position { Id = 4, Title = "Manager 2", Number = "3", ManagerPositionId = seniorManager.Id };
            var seniorDeveloper1 = new Position { Id = 5, Title = "Senior Developer 1", Number = "4", ManagerPositionId = manager1.Id };
            var seniorDeveloper2 = new Position { Id = 6, Title = "Senior Developer 2", Number = "4", ManagerPositionId = manager2.Id };
            var juniorDeveloper1 = new Position { Id = 7, Title = "Junior Developer", Number = "5", ManagerPositionId = seniorDeveloper1.Id };
            var juniorDeveloper2 = new Position { Id = 8, Title = "Junior Developer", Number = "5", ManagerPositionId = seniorDeveloper2.Id };

            var employee1 = new Employee { Id = 1,  FirstName = "John", LastName = "Doe", Number = "T0001", PositionId = Director.Id };
            var employee2 = new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Number = "T0002", PositionId = seniorManager.Id };
            var employee3 = new Employee { Id = 3, FirstName = "Bob", LastName = "Johnson", Number = "T0003", PositionId = manager1.Id };
            var employee4 = new Employee { Id = 4, FirstName = "Dave", LastName = "Raynal", Number = "T0004", PositionId = manager2.Id };
            var employee5 = new Employee { Id = 5, FirstName = "Michael", LastName = "Song", Number = "T0005", PositionId = seniorDeveloper1.Id };
            var employee6 = new Employee { Id = 6, FirstName = "Brett", LastName = "Lee", Number = "T0006", PositionId = seniorDeveloper2.Id };
            var employee7 = new Employee { Id = 7, FirstName = "Charles", LastName = "Smith", Number = "T0007", PositionId = juniorDeveloper1.Id };

            modelBuilder.Entity<Position>().HasData(
                Director, seniorManager, manager1, manager2, seniorDeveloper1, seniorDeveloper2, juniorDeveloper1, juniorDeveloper2
                );
            modelBuilder.Entity<Employee>().HasData(
                employee1, employee2, employee3, employee4, employee5, employee6, employee7
               );

            base.OnModelCreating(modelBuilder);

        }
    }
}
