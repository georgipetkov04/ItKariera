using Microsoft.AspNetCore.Identity;

namespace Project_7.Data
{
    public class AppUser : IdentityUser

    {
        public AppUser()
        {
            Lists = new List<ToDoList>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public virtual List<ToDoList> Lists { get; set; }
    }
}
