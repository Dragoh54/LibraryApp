namespace LibraryApp.Application.Book;

public class TakeBookRequest
{
    public Guid BookId { get; set; }
    public DateTime ReturnDate { get; set; }
}