using System.ComponentModel.DataAnnotations;

namespace Innovitics.Task
{
    public class VerifyUserVM
    {
        [Required]
        [Range(1000000000000, long.MaxValue, ErrorMessage = "Card number must be more than 14 digits.")]
        public long CardNumber { get; set; }
        
        [Required]
        [Range(100000, int.MaxValue, ErrorMessage = "PIN must be more than 6 digits.")]
        public int PIN { get; set; } 
    }
}
