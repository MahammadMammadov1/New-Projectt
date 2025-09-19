namespace deneme_2.Models
{
    public class Catagory : BaseEntity
    {
        public string Name { get; set; } 
        public string Description { get; set; } 
        public List<Book> Books { get; set; } 

    }
}
