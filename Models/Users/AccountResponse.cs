﻿namespace DiplomaAPI.Models.Users
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public double Progress { get; set; }
        public int? GroupId { get; set; }
    }
}
