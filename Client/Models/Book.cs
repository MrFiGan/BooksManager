using System;

namespace Client.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }

        public Book()
        {

        }

        public Book(string title, string author, string isbn, int publicationYear, int quantity, Guid category)
        {
            Id = new Guid();
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
            Quantity = quantity;
            CategoryId = category;
        }
    }
}