using System.ComponentModel.DataAnnotations;

namespace TulipHR.API.Models
{
    /// <summary>
    /// A DTO for creating new position
    /// </summary>
    public class PositionCreationDto
    {
        /// <summary>
        /// The Title of the position
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// The number assigned to the position
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Number { get; set; } = string.Empty;
        
        /// <summary>
        ///The Id of manager position
        /// </summary>
        [Required]
        public int? ManagerPositionId { get; set; }
    }
}
