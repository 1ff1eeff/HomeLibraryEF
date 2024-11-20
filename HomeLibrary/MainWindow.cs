using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace HomeLibrary
{
    public partial class MainWindow : Form
    {
        private HomeLibraryContext? db;                         // Объявляем контекст данных для представления базы данных.

        public MainWindow()
        {
            InitializeComponent();                              // Инициализация компонентов формы.
        }

        protected override void OnLoad(EventArgs e)             // При загрузке формы.
        {
            base.OnLoad(e);                                     

            db = new HomeLibraryContext();                      // Инициализируем контекст для представления базы данных.

            //db.Database.EnsureDeleted();                      // Если база данных существует, то она удаляется.

            bool isCreated = db.Database.EnsureCreated();       // Если база данных отсутствует, то данный метод создает ее.
                                                                // Если база данных имеется, но она не имеет таблиц,
                                                                // то этот метод создает таблицы, которые соответствуют схеме данных.

            if (isCreated)                                      // Если база данных не была обнаружена, добавляем произвольные записи.
            {
                label6.Text = "База данных не обнаружена. " +
                    "Создана новая база данных.";               // Информируем пользователя о создании новой базы данных.

                Book? voinaIMir = new Book();                   // Объявляем и инициализируем экземпляр класса Book, для хранения информации о книге.
                voinaIMir.Name = "Война и мир";                 // Указываем имя книги.
                voinaIMir.Description = "Роман-эпопея, " +
                    "описывающий русское общество " +
                    "в эпоху войн против Наполеона.";           // Указываем описание книги.

                Book? evgenijOnegin = new Book();               // Объявляем и инициализируем экземпляр класса Book, для хранения информации о книге.
                evgenijOnegin.Name = "Евгений Онегин";          // Указываем имя книги.
                evgenijOnegin.Description = "Роман в стихах, " +
                    "одно из самых значительных произведений " +
                    "русской словесности.";                     // Указываем описание книги.

                db?.Books.AddRange(voinaIMir, evgenijOnegin);   // Добавляем созданные экземпляры книг в таблицу Books.

                Author? tolstoi = new Author();                 // Объявляем и инициализируем экземпляр класса Author, для хранения информации об авторе книги.
                tolstoi.Name = "Л. Н. Толстой";                 // Указываем имя автора.

                Author? pushkin = new Author();                 // Объявляем новый экземпляр класса Author, для хранения информации об авторе книги.                                                                            
                pushkin.Name = "А. С. Пушкин";                  // Указываем имя автора.

                db?.Authors.AddRange(tolstoi, pushkin);         // Добавляем авторов в таблицу Authors.

                Genre? roman = new Genre() { Name = "Роман" };  // Объявляем новый экземпляр класса Genre, для хранения информации о жанре книги.
                                                                // Заполняем соответсвующее поле, содержащее наименование жанра.
                roman.Name = "Роман";

                Genre? historyProse = new Genre();              // Объявляем новый экземпляр класса Genre, для хранения информации о жанре книги.
                historyProse.Name = "Историческая проза";       // Заполняем соответсвующее поле, содержащее наименование жанра.

                db?.Genres.AddRange(roman, historyProse);       // Добавляем новые жанры в таблицу Genres.

                voinaIMir.Author = tolstoi;                     // Заполняем поле, содержащее информацию об авторе книги.
                voinaIMir.Genres.Add(roman);                    // Добавляем ранее определённый жанр в список жанров книги.

                evgenijOnegin.Author = pushkin;                 // Заполняем поле, содержащее информацию об авторе книги.
                evgenijOnegin.Genres.Add(roman);                // Добавляем ранее определённый жанр в список жанров книги.
                evgenijOnegin.Genres.Add(historyProse);         // Добавляем ранее определённый жанр в список жанров книги.

                db?.SaveChanges();                              // Сохраняем внесённые в базу данных изменения.
            }
            else
            {
                label6.Text = 
                    "Соединение с базой данных установлено.";   // Если база данных существует, информируем пользователя об успешном соединении с ней.
            }

            db?.Books.Load();                                   // Загружаем таблицу, хранящую информацию о книгах, в память.
            db?.Authors.Load();                                 // Загружаем таблицу, хранящую информацию об авторах, в память.
            db?.Genres.Load();                                  // Загружаем таблицу, хранящую информацию о жанрах, в память.

            FillDataGridView(dataGridView1, db);                // Заполняем элемент DataGridView информацией из базы даных.

            if (dataGridView1.RowCount > 0)                     // Если DataGridView содержит строки.
            {
                FillCurrentBook();                              // Заполняем элементы формы, предназначенные для хранения информации о выбранной книге.
            }

            cbAuthor.DataSource = 
                db.Authors.Select(a => a.Name).ToList();        // Помещаем все имена авторов из базы данных в элемент ComboBox.
            cbGenre.DataSource = 
                db.Genres.Select(g => g.Name).ToList();         // Помещаем все наименования жанров из базы данных в элемент ComboBox.
        }

        protected override void OnClosing(CancelEventArgs e)    // При закрытии формы.
        {
            base.OnClosing(e);                                  
            db?.Dispose();                                      // Освобождаем ресурсы выделенные под контекст базы данных.
            db = null;                                          // Обнуляем контекст базы данных. 
        }

        /// <summary>
        /// Заполняем элемент DataGridView информацией из базы даных.
        /// </summary>
        /// <param name="dgv">  Элемент DataGridView.   </param>
        /// <param name="db">   База даннх.             </param>
        private static void FillDataGridView(DataGridView dgv, HomeLibraryContext db)
        {
            int selectedRowIndex = 0;                       // Переменная для хранения номера строки.
            if (dgv.CurrentCell is not null)                // Если активная ячейка существует.
            {
                selectedRowIndex = 
                    dgv.SelectedRows[0].Index;              // Сохраняем номер выделенной строки в переменную.
            }

            List<BookInfo> booksList = [];                  // Объявляем и инициализируем список для хранения информации о книгах.
            List<Genre> listGenres = 
                db.Genres.Include(c => c.Books).ToList();   // Объявляем и инициализируем список для хранения жанров.
            
            foreach (Book b in db.Books)                    // Для каждой книги в списке книг из базы данных.
            {
                List<string> genres = new();                // Объявляем и инициализируем список для хранения названий жанров.
                foreach (Genre genre in listGenres)         // Для каждого наименования жанра в списке жанров.
                    foreach (Book book in genre.Books)      // Для каждой книги, указанной в таблице жанров.
                        if (book == b)                      // Если указанная книга совпадает с рассматриваемой в данный момент книгой.
                            genres.Add(genre.Name);         // Добавляем название жанра в список, предназначенный для хранения названий жанров текущей книги.

                string genresInOneLine = 
                    string.Join(", ", [.. genres]);         // Преобразуем полученный список в строку, с разделителем: запятая и пробел после неё.

                BookInfo selectedBook = new BookInfo();     // Объявляем и инициализируем экземпляр класса, предназначенного для хранения информации о книге. 
                selectedBook.ID = b.Id.ToString();          // Заполняем поле, предназначенное для хранения идентефикационного номера книги в БД.
                selectedBook.Name = b.Name;                 // Заполняем поле, предназначенное для хранения названия книги.
                selectedBook.Author = b.Author.Name;        // Заполняем поле, предназначенное для хранения имени автора.
                selectedBook.Genres = genresInOneLine;      // Заполняем поле, предназначенное для хранения списка жанров.
                selectedBook.Description = b.Description;   // Заполняем поле, предназначенное для хранения описания книги.

                booksList.Add(selectedBook);                // Добавляем полученный объект в список.
            }

            BindingSource source = new BindingSource();     // Объявляем и инициализируем компонент BindingSource.
            source.DataSource = booksList;                  // Указываем список с информацией о книгах, как источник данных.
            dgv.DataSource = source;                        // Привязываем созданный ранее компонент к элементу формы DataGridView.
            
            dgv.Columns[0].Visible = false;                 // Скрываем первую колонку таблицы элемента формы DataGridView.
            dgv.Columns[1].HeaderText = "Название";         // Устанавливаем заголовок второй колонки.
            dgv.Columns[2].HeaderText = "Автор";            // Устанавливаем заголовок третьей колонки.
            dgv.Columns[3].HeaderText = "Жанр";             // Устанавливаем заголовок четвёртой колонки.
            dgv.Columns[4].HeaderText = "Описание";         // Устанавливаем заголовок пятой колонки.

            if (selectedRowIndex >= dgv.RowCount)           // Если номер выделенной строки равен или превышает общее количество строк.
                selectedRowIndex--;                         // Уменьшаем значение номера выделенной строки на 1.
            dgv.CurrentCell = dgv[1, selectedRowIndex];     // Выделяем ячейку определённого номера строки.
        }

        /// <summary>
        /// Формируем строку из полученного списка жанров.
        /// </summary>
        /// <param name="genres"> Список жанров. </param>
        /// <returns> Строка, содержащая жанры. </returns>
        private static string ListGenresToString(List<Genre> genres)
        {
            List<string> lines = new List<string>();        // Объявляем и инициализируем список строк для хранения  
            foreach (Genre g in genres)                     // Для каждого жанра в списке жанров.
                lines.Add(g.Name);                          // Добавляем наименование жанра в список.                           
            string line = string.Join(", ", [.. lines]);    // Объединяем список в строку с разделителем: запятая и пробел после неё.

            return line;
        }

        /// <summary>
        /// Находим автора в базе данных.
        /// </summary>
        /// <param name="db">       Контекст базы данных.   </param>
        /// <param name="name">     Имя автора.             </param>
        /// <returns> Экземпляр класса Author.</returns>
        private Author? FindAuthor(string name, HomeLibraryContext db)
        {
            IQueryable<Author> authors = 
                db.Authors.Where(a => a.Name == name);      // Находим авторов в базе данных, с именем равным значению полученного параметра "name".

            Author? author = new();                         // Объявляем и инициализируем экземпляр класса Author, для хранения информации об авторе книги.
            if (authors.Any())                              // Если найден хотя бы один автор.
            {
                author = authors.First();                   // Выбираем первого автора из списка.
            }
            else
            {                                               // Иначе (если ни один автор не найден).
                author = new Author();                      // Объявляем и инициализируем экземпляр класса Author, для хранения информации об авторе книги. 
                author.Name = name;                         // Задаём значение параметра "name", как значение поля "Name" – имя автора.
                db.Add(author);                             // Добавляем нового автора в базу данных.
            }

            return author;
        }

        /// <summary>
        /// Находим выбранную книгу в базе данных.
        /// </summary>
        /// <returns> Экземпляр книги. </returns>
        private Book? FindSelectedBook()
        {
            int index = dataGridView1.SelectedRows[0].Index;                    // Получаем значение номера выделенной строки в элементе DataGridView.
            int id = 0;                                                         // Переменная для хранения значения ячейки столбца Id выделенной строки.
            Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);   // Получаем значение Id из элемента DataGridView.
                                                                                // Результат выводим в ранее созданную целочисленную переменную "id".
            Book? book = new Book();                                            // Объявляем и инициализируем экземпляр класса Book.
            book = db?.Books.Find(id);                                          // Находим запись с соответствующим значением поля Id в таблице Books.

            return book;
        }

        /// <summary>
        /// Заполняем элементы формы, предназначенные для хранения информации о выбранной книге.
        /// </summary>
        private void FillCurrentBook()
        {
            Book? book = FindSelectedBook();                    // Находим в базе данных книгу, соответствующую выделенной в DataGridView.
            tbName.Text = book?.Name;                           // Помещаем информацию о наименовании книги из базы данных в поле Text элемента TextBox.
            cbAuthor.Text = book?.Author.Name;                  // Помещаем информацию об авторе книги из базы данных в поле Text элемента ComboBox.
            cbGenre.Text = ListGenresToString(book.Genres);     // Помещаем информацию о жанрах книги из базы данных в поле Text элемента ComboBox. 
            rtbDescription.Text = book?.Description;            // Помещаем описание книги из базы данных в поле Text элемента RichTextBox.
        }

        /// <summary>
        /// Добавляем новую книгу в базу данных.
        /// </summary>
        private void CreateBook(HomeLibraryContext db)
        {
            if (!string.IsNullOrWhiteSpace(tbName.Text))            // Если элемент TextBox "tbName" на форме содержит текст.
            { 
                Author? author = new Author();                      // Объявляем и инициализируем экземпляр класса Author.
                author = FindAuthor(cbAuthor.Text.ToString(), db);  // Находим автора в базе данных и устанавливаем в качестве значения.

                Book book = new Book();                             // Объявляем и инициализируем экземпляр класса Book.
                book.Name = tbName.Text;                            // Получаем наименование книги из поля Text элемента TextBox.
                book.Author = author;                               // Устанавливаем ранее созданного автора, как автора книги.
                book.Description = rtbDescription.Text;             // Получаем описание из поля Text элемента RichTextBox.

                db?.Add(book);

                List<string> genresFromDb = [.. db?.Genres.Select(g => g.Name)];    // Формируем список строк,
                                                                                    // содержащий все наименования жанров, хранящихся в базе данных.

                List<string> genresFromForm = [.. cbGenre.Text.Split(", ")];        // Формируем список строк из наименований жанров,
                                                                                    // указанных в поле Text элемента ComboBox.

                foreach (string genre in genresFromForm)                            // Для каждого такого наименования жанра.
                {
                    if (!genresFromDb.Contains(genre))                              // Если в базе данных не содержится текущее наименование жанра.
                    {
                        Genre newGenre = new Genre();                               // Объявляем и инициализируем экземпляр класса Genre.
                        newGenre.Name = genre;                                      // Указываем наименование жанра.
                        db?.Genres.Add(newGenre);                                   // Добавляем созданный жанр в базу данных.        
                        db?.SaveChanges();                                          // Сохраняем внесённые в базу данных изменения.
                    }
                    db?.Genres.FirstOrDefault(g => g.Name == genre).Books.Add(book);// Добавляем созданный ранее экземпляр книги в поле Books,
                                                                                    // таблицы Genres для записи с именем текущего наименования жанра.
                }

                int? result = db?.SaveChanges();                                     // Сохраняем внесённые в базу данных изменения.
                if (result > 0)
                {
                    label6.Text = "Информация о книге: \"" + book.Name + 
                                  "\" добавлена в базу данных.";                    // Информируем пользоватаеля о добавлении записи в базу данных.
                }
            }
            else
            {                                                                       // Иначе (если элемент TextBox "tbName" на форме не содержит текст).
                label6.Text = "Поле \"Название\" должно быть заполнено!";           // Информируем пользоватаеля о проблеме.
            }     
        }

        /// <summary>
        /// Модифицируем данные выбранной книги в базе данных.
        /// </summary>
        private void UpdateBook(HomeLibraryContext db)
        {
            Author? author = new Author();                          // Объявляем и инициализируем экземпляр класса Author.
            author = FindAuthor(cbAuthor.Text.ToString(), db);      // Находим в базе данных автора с указанным в элементе ComboBox именем.
                                                                    // Устанавливаем найденного автора в качестве значения созданного объекта.

            Book? book = new Book();                                // Объявляем и инициализируем экземпляр класса Book. 
            book = FindSelectedBook();                              // Находим выбранную книгу в базе данных и устанавливаем в качестве значения. 
            
            if (author is not null & book is not null)              // Если автор и книга определены.
            {
                string? prevName = book.Name;                       // Сохраняем текущее наименование книги в переменную.
                book.Name = tbName.Text;                            // Устанавливаем значение поля "Name" равным значению поля Text элемента TextBox.
                book.Author = author;                               // Устанавливаем значением поля "Author" созданный ранее объект класса Author.

                List<string> allGenres = new List<string>();        // Объявляем и инициализируем список для хранения наименований жанров из базы данных. 
                allGenres = [.. db?.Genres.Select(g => g.Name)];    // Помещаем все наименования жанров, найденные в базе данных, в список.
         
                List<string> genresString = new List<string>();     // Объявляем и инициализируем список для хранения наименований жанров. 
                genresString = [.. cbGenre.Text.Split(", ")];       // Помещаем наименования жанров, указанные в элементе поле Text, элемента ComboBox, в список.
                book.Genres.Clear();                                // Удаляем элементы из поля Genres.
                foreach (string genre in genresString)              // Для каждого наименования жанра в списке.
                {
                    if (!allGenres.Contains(genre))                 // Если текущее наименование не содержется в базе данных.
                    {
                        Genre newGenre = new Genre();               // Объявляем и инициализируем экземпляр класса Genre.
                        newGenre.Name = genre;                      // Указываем наименование жанра.
                        db?.Genres.Add(newGenre);                   // Добавляем созданный жанр в базу данных.   
                        db?.SaveChanges();                          // Сохраняем внесённые в базу данных изменения.
                    }
                    book.Genres.Add(db?.Genres.FirstOrDefault(
                                        g => g.Name == genre));     // Добавляем в поле Genres экземпляр жанра с именем равным текущему наименованию жанра.         
                }
                
                book.Description = rtbDescription.Text;             // Устанавливаем значение поля "Description" равным значению поля Text элемента RichTextBox. 
                dataGridView1.Update();                             // Обновляем элемент DataGridView.                
                int? result = db?.SaveChanges();
                if (result > 0)                                     // Сохраняем внесённые в базу данных изменения.
                    label6.Text = "Информация о книге: \"" 
                                  + prevName + "\" обновлена.";     // Информируем пользователя о произведённой модификации.
            }
        }

        /// <summary>
        /// Удаляем выбранную книгу из базы данных.
        /// </summary>
        private void DeleteBook(HomeLibraryContext db)
        {
            Book? book = new Book();                                // Объявляем и инициализируем экземпляр класса Book. 
            book = FindSelectedBook();                              // Находим выделенную в элементе DataGridView книгу в базе данных
                                                                    // и устанавливаем в качестве значения объекта.

            if (book is not null)                                   // Если книга определена.
            {
                db?.Books.Remove(book);                             // Удаляем экземпляр книги из базы данных.
                int? result = db?.SaveChanges();                    // Сохраняем внесённые в базу данных изменения. 
                if (result > 0)
                    label6.Text = "Книга \"" + tbName.Text + 
                                "\" удалена из базы данных.";       // Информируем пользователя об удалении записи из базы данных. 
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillCurrentBook();                                      // Заполняем элементы формы, предназначенные для хранения информации о выбранной книге.
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateBook(db);                                         // Добавляем новую книгу в базу данных.
            FillDataGridView(dataGridView1, db);                    // Заполняем элемент DataGridView информацией из базы даных.
            dataGridView1.CurrentCell = 
                this.dataGridView1[1, dataGridView1.RowCount - 1];  // Выделяем последнюю строку в таблице элемента формы DataGridView.
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateBook(db);                                         // Модифицируем данные выбранной книги в базе данных.
            FillDataGridView(dataGridView1, db);                    // Заполняем элемент DataGridView информацией из базы даных.
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteBook(db);                                         // Удаляем выбранную книгу из базы данных.
            FillDataGridView(dataGridView1, db);                    // Заполняем элемент DataGridView информацией из базы даных.
            if (dataGridView1.RowCount > 0)
            {
                FillCurrentBook();                                  // Заполняем элементы формы, предназначенные для хранения информации о выбранной книге.
            }
        }

        private void cbAuthor_DropDown(object sender, EventArgs e)
        {
            cbAuthor.DataSource = 
                db?.Authors.Select(a => a.Name).ToList();           // Заполняем элемент ComboBox списком имён всех авторов из базы даных.
        }

        private void cbGenre_DropDown(object sender, EventArgs e)
        {
            cbGenre.DataSource = 
                db?.Genres.Select(g => g.Name).ToList();            // Заполняем элемент ComboBox списком всех жанров из базы даных.
        }
    }
}
