using ExamMSAppMVC.Contracts.Entities;

namespace ExamMSAppMVC.Models.Entities
{
    public class Role : BaseUser
    {
        public required string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();

    }

}
