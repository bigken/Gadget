namespace Gadget.Data.Entity
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarUrl { get; set; }

        public string Email { get; set; }
    }
}
