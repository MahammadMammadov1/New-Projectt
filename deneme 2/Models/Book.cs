namespace deneme_2.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime ReleaseDate { get; set; }  
        public decimal Price { get; set; } 
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CatagoryId { get; set; }
        public Catagory Catagory { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
