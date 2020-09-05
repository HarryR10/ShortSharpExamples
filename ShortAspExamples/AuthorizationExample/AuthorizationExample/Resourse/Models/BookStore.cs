using System;
using System.Collections.Generic;

namespace Resourse.Models
{
    public class BookStore
    {
        public List<Book> Books => new List<Book>
        {
            new Book {Id = 1, Author = "S. E. King", Title = "The Shining", Price = 10.45M},
            new Book {Id = 2, Author = "J. R. R. Tolkien", Title = "The Hobbit, or There and Back Again", Price = 8.45M},
            new Book {Id = 3, Author = "J. R. R. Tolkien", Title = "The Lord of the Rings", Price = 9.45M}
        };

        public Dictionary<Guid, int[]> Orders => new Dictionary<Guid, int[]>
        {
            {Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13a"), new int[] {1,2} },
            {Guid.Parse("7b0a1ec1-a849-4f3c-9004-df8fc921c13a"), new int[] {1,3} }
        };
    }
}
