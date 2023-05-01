using System.ComponentModel.DataAnnotations;

namespace TulipHR.API.Models
{
    public class EmployeeDTO
    {
        /// <summary>
        /// The Id of the Employee
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The first name of the employee
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// The last name of the employee
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// The employee number
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        ///The Id of the position
        /// </summary>
        public int? PositionId { get; set; }
    }
}
