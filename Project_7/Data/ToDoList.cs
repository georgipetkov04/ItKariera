using System.ComponentModel.DataAnnotations;

namespace Project_7.Data
{
    public class ToDoList
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual AppUser? User { get; set; }
        public string Title { get; set; }
        public virtual List<ToDoItem> Items { get; set; }

        public ToDoList()
        {
            Items = new List<ToDoItem>();
        }
    }
}