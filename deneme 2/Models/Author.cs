namespace deneme_2.Models
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public DateTime BirthDate { get; set; } 
        public List<Book> Books { get; set; } 

    }
}
