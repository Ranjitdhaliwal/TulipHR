using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TulipHR.API.Models;

namespace TulipHR.API.Entities
{
    /// <summary>
    /// Entity class reprsenting Position table
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Unique Id of the Position
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// The Title of the Position
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The number assigned to the Position
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Number { get; set; } = string.Empty;
        
        /// <summary>
        ///The Id of the manager position
        /// </summary>
        public int? ManagerPositionId { get; set; }
        [ForeignKey("ManagerPositionId ")]
        public Position? ManagerPosition { get; set; }
    }
}
