﻿namespace API_JWT.Model.DTO
{
    public class CreateUserDTO
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; } = "";
    }
}
