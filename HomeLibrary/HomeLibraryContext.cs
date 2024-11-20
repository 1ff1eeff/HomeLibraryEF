using Microsoft.EntityFrameworkCore;

namespace HomeLibrary
{
    /// <summary>
    /// Класс, представляющий конекст данных.
    /// </summary>
    public class HomeLibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;     // Свойство для связи с таблицей, где будут храниться данные о книгах.
        public DbSet<Author> Authors { get; set; } = null!; // Свойство для связи с таблицей, где будут храниться данные об авторах.
        public DbSet<Genre> Genres { get; set; } = null!;   // Свойство для связи с таблицей, где будут храниться данные о жанрах.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseNpgsql("Server = localhost;" +
                                     "Port = 5432;" +
                                     "Database = HomeLibrary;" +
                                     "User Id = postgres;" +
                                     "Password = 1;");      // Задаём параметры подключения к базе данных.
        }
    }
}
