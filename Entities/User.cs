namespace DiplomaAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
/*        public string VerificationToken { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }*/
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public double Progress { get; set; }
        public int? GroupId { get; set; }
            
        public bool OwnsToken(string token)
        {
            return RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}
