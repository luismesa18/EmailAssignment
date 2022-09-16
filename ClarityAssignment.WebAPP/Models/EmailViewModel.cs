using System.ComponentModel.DataAnnotations;

namespace ClarityAssignment.WebAPP.Models
{
    public class EmailViewModel
    {
        [Required]
        [EmailAddress]
        public string To { get; set; }
        [Required]
        [MaxLength(500)]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }

    }
}
