using TulipHR.API.Entities;

namespace TulipHR.API.Models
{
    /// <summary>
    /// A DTO for the Position providing detail of the position and the employee
    /// </summary>
    public class PositionDto
    {
        /// <summary>
        /// Unique Id of the Position
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Title of the Position
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// The number assigned to the Position
        /// </summary>
        public string Number { get; set; } = string.Empty;
        /// <summary>
        ///The Id of the manager position
        /// </summary>
        public int? ManagerPositionId { get; set; }
        public EmployeeDTO? Employee { get; set; }

        /// <summary>
        /// Property indicating if the Position is vacant
        /// </summary>
        public bool IsVacant
        {
            get
            {
                return Employee == null;
            }
        }
    }
}
