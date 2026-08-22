namespace HSMDataAccess.Entities
{
    public class User
    {
        public string UserID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Status { get; set; }
        public string HashPassword { get; set; } = null!;
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        
    }
}
