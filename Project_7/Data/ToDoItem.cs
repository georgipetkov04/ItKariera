namespace Project_7.Data
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string AssigneeId { get; set; } //UserId на зачисления потребител
        public virtual AppUser? Assignee { get; set; }
        public string Title { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public int ToDoListId { get; set; }
        public virtual ToDoList? ToDoList { get; set; }

    }
}