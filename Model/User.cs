using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API_JWT.Model
{
    public class User
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string? username { get; set; }
        [Required]
        public string? password { get; set; }
        public string? role { get; set; } = "Customer";
        public int isActive { get; set; } = 1;
        [EmailAddress]
        public string? email { get; set; } = "";

    }
}
