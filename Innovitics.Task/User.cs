﻿
using System.ComponentModel.DataAnnotations;

namespace Innovitics.Task
{
     public class User
    {
        public int UserID { get; set; }
        [Required]

        [Range(10000000000000, long.MaxValue, ErrorMessage = "Card number must be 14 digits or more.")]
        public long CardNumber { get; set; }

        [Required]
        [Range(100000, int.MaxValue, ErrorMessage = "PIN must be more than 6 digits.")]
        public int PIN { get; set; }
        public decimal Balance { get; set; }
    }
}
