using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary
{
    /// <summary>
    /// Класс, предназначенный для хранения информации о книге.
    /// </summary>
    /// <param name="id">           Id книги.       </param>
    /// <param name="name">         Название книги. </param>
    /// <param name="author">       Автор книги.    </param> 
    /// <param name="genres">       Жанр книги.     </param>
    /// <param name="description">  Описание книги. </param>
    internal class BookInfo
    {
        public BookInfo()
        {
            this.ID = string.Empty;             // Id книги.
            this.Name = string.Empty;           // Название книги.
            this.Author = string.Empty;         // Автор книги.
            this.Genres = string.Empty;         // Жанр книги.
            this.Description = string.Empty;    // Описание книги.
        }

        // Констуктор с параметрами.
        public BookInfo(string id, string name, string author, string genres, string description)
        {
            this.ID = id;                       // Id книги.
            this.Name = name;                   // Название книги.
            this.Author = author;               // Автор книги.
            this.Genres = genres;               // Жанр книги.
            this.Description = description;     // Описание книги.
        }

        // Аксессоры.
        public string ID { get; set; }          // Id книги.
        public string Name { get; set; }        // Название книги.
        public string Author { get; set; }      // Автор книги.
        public string Genres { get; set; }      // Жанр книги.
        public string Description { get; set; } // Описание книги.
    }
}
