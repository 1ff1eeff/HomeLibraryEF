using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeLibrary
{
    /// <summary>
    /// Классс для работы с таблицей, 
    /// предназначенной для хранения информации об авторах книг.
    /// </summary> 
    [Table("Authors")]
    public class Author
    {
        [Column("Id")]  // Представляет столбец "Id".
        [Key]           // Столбец хранит значение первичных ключей.
        [Required]      // Столбец обязателен к заполнению.
        public int Id { get; set; } 

        [Column("Books")]    // Столбец для связи с внешней таблицей "Book".
        public virtual List<Book> Books { get; set; } = new List<Book>();

        [Column("Name")]    // Столбец Name.
        [Required]          // Столбец обязателен к заполнению.
        public string? Name { get; set; } 
    }
}
