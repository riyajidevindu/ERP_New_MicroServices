namespace ERP.Authentication.Core.Entity
{
    public class UserAccount : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
