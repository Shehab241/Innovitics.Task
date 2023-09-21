
using System.ComponentModel.DataAnnotations;

namespace Innovitics.Task
{
    public class CashWithdrawalRequest
    {
        [Required]
        [Range(10000000000000, long.MaxValue, ErrorMessage = "Card number must be 14 digits or more.")]
        public long CardNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
