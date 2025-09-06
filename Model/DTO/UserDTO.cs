using System.ComponentModel.DataAnnotations;

namespace API_JWT.Model.DTO
{
    public class UserDTO
    {
        public string? username { get; set; }
        public string? role { get; set; } = "Customer";
        public int isActive { get; set; } = 0;
        public string? email { get; set; } = "";
    }
}

