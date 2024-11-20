using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeLibrary
{
    /// <summary>
    /// Классс для работы с таблицей, 
    /// предназначенной для хранения информации о книгах.
    /// </summary>
    
    [Table("Books")]    // Представляет таблицу "Books".
    public class Book
    {
        [Column("Id")]  // Представляет столбец "Id".
        [Key]           // Столбец хранит значение первичных ключей.
        [Required]      // Столбец обязателен к заполнению.
        public int Id { get; set; } // Индекс.

        [Column("Name")]    // Представляет столбец "Name".
        [Required]          // Столбец обязателен к заполнению.
        public string? Name { get; set; }

        [Column("AuthorId")]    // Представляет внешний ключ для связи с внешней таблицей "Authors".
        public int? AuthorId { get; set; } 
                
        [Column("Author")]      // Представляет столбец для связи с внешней таблицей "Authors".
        public virtual Author Author { get; set; }

        [Column("Genres")]       // Представляет столбец для связи с внешней таблицей "Genres".
        public virtual List<Genre> Genres { get; set; } = new List<Genre>();

        [Column("Description")] // Представляет столбец Description.
        public string? Description { get; set; }
    }
}
