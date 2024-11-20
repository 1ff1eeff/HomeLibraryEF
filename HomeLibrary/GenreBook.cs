namespace HomeLibrary
{
    /// <summary>
    /// Класс, представляющий промежуточную таблицу.
    /// </summary>
    public class GenreBook
    {
        public Book? Book { get; set; }     // Представляет столбец для связи с внешней таблицей "Books". 

        public Genre? Genre { get; set; }   // Представляет столбец для связи с внешней таблицей "Genres".
    }
}
