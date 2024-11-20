using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeLibrary
{
    /// <summary>
    /// Классс для работы с таблицей, 
    /// предназначенной для хранения информации о жанрах книг.
    /// </summary>    
    [Table("Genres")] // Представляет таблицу "Genres".
    public class Genre
    {
        [Column("Id")]  // Представляет столбец "Id".
        [Key]           // Столбец хранит значение первичных ключей.
        [Required]      // Столбец обязателен к заполнению.
        public int Id { get; set; }

        [Column("Name")]    // Представляет столбец "Name".
        [Required]          // Столбец обязателен к заполнению.
        public string? Name { get; set; }

        [Column("Books")]    // Представляет столбец для связи с внешней таблицей "Books". 
        public virtual List<Book>? Books { get; set; } = new List<Book>();
    }
}
