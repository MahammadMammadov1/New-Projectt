namespace deneme_2.DTOs.BookDtos
{
    public class BookUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public int CatagoryId { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}
