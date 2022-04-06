using System;
using System.ComponentModel.DataAnnotations;

namespace INTEX.Models
{
    public class Severity
    {
        [Key]
        [Required]
        public int CRASH_SEVERITY_ID { get; set; }

        [Required]
        public string DESCRIPTION { get; set; }
    }
}
