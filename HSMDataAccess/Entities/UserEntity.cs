namespace HSMDataAccess.Entities
{
    public class UserEntity
    {
        public string UserID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; } = null;
        public string AccessLevel { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string Password { get; set; } = null!;
        public UserEntity? Creator { get; set; }
        public UserEntity? Updater { get; set; }
        public ICollection<UserEntity> CreatedUsers { get; set; } = new List<UserEntity>();
        public ICollection<UserEntity> UpdatedUsers { get; set; } = new List<UserEntity>();
    }
}
