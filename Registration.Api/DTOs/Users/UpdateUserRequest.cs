using System.ComponentModel.DataAnnotations;

namespace Registration.Api.DTOs.Users
{
    public class UpdateUserRequest
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public long? CompanyId { get; set; }
    }
}
