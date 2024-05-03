namespace StudentPortal.Web.Models.Entities
{
    public class Student // this is domain model or entity
    {
        public Guid Id  { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Subscribed { get; set; }
        public string Phone { get; set; }
    }
}
