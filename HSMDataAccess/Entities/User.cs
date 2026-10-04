namespace HSMDataAccess.Entities
{
    public class User
    {
        public string ID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        
        public bool Status { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string PersonID { get; set; } = null!;
        public virtual Person Person { get; set; } = null!;
        public virtual  Doctor  doctor { get; set; } = null!;
        public ICollection<Notifiction> notifiction { get; set; } = new List<Notifiction>();
        public ICollection<Report> report { get; set; } = new List<Report>();
        public ICollection<AuditLog> auditLog { get; set; } = new List<AuditLog>();

    }
}
