﻿namespace TienThoBookStore.WebApp.Models
{
    public class UserDto
    {
        public string Id { get; set; } = "";
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";     // thêm property Name
        public bool Verified { get; set; }
    }
}
