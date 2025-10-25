using System.ComponentModel.DataAnnotations;

namespace EmailService.Domain.Shared.DataTransferModel
{
    public class EmailDto
    {
        [Required]

        public string? To { get; set; }
        [Required]

        public string? Subject { get; set; }
        [Required]

        public string? Body { get; set; }
    }
}
