using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksAPI.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int Quantity { get; set; }

        public Guid CategoryId { get; set; }  // Foreign key
        [JsonIgnore]
        public virtual Category Category { get; set; }  // Navigation property

        public Book() { }

        public Book(BookViewModel bookView)
        {
            Id = new Guid();
            Title = bookView.Title;
            Author = bookView.Author;
            ISBN = bookView.ISBN;
            PublicationYear = bookView.PublicationYear;
            Quantity = bookView.Quantity;
            CategoryId = bookView.CategoryId;
        }
    }
}