using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace HomeLibrary
{
    public partial class MainWindow : Form
    {
        private HomeLibraryContext? db;                         // ��������� �������� ������ ��� ������������� ���� ������.

        public MainWindow()
        {
            InitializeComponent();                              // ������������� ����������� �����.
        }

        protected override void OnLoad(EventArgs e)             // ��� �������� �����.
        {
            base.OnLoad(e);                                     

            db = new HomeLibraryContext();                      // �������������� �������� ��� ������������� ���� ������.

            //db.Database.EnsureDeleted();                      // ���� ���� ������ ����������, �� ��� ���������.

            bool isCreated = db.Database.EnsureCreated();       // ���� ���� ������ �����������, �� ������ ����� ������� ��.
                                                                // ���� ���� ������ �������, �� ��� �� ����� ������,
                                                                // �� ���� ����� ������� �������, ������� ������������� ����� ������.

            if (isCreated)                                      // ���� ���� ������ �� ���� ����������, ��������� ������������ ������.
            {
                label6.Text = "���� ������ �� ����������. " +
                    "������� ����� ���� ������.";               // ����������� ������������ � �������� ����� ���� ������.

                Book? voinaIMir = new Book();                   // ��������� � �������������� ��������� ������ Book, ��� �������� ���������� � �����.
                voinaIMir.Name = "����� � ���";                 // ��������� ��� �����.
                voinaIMir.Description = "�����-������, " +
                    "����������� ������� �������� " +
                    "� ����� ���� ������ ���������.";           // ��������� �������� �����.

                Book? evgenijOnegin = new Book();               // ��������� � �������������� ��������� ������ Book, ��� �������� ���������� � �����.
                evgenijOnegin.Name = "������� ������";          // ��������� ��� �����.
                evgenijOnegin.Description = "����� � ������, " +
                    "���� �� ����� ������������ ������������ " +
                    "������� �����������.";                     // ��������� �������� �����.

                db?.Books.AddRange(voinaIMir, evgenijOnegin);   // ��������� ��������� ���������� ���� � ������� Books.

                Author? tolstoi = new Author();                 // ��������� � �������������� ��������� ������ Author, ��� �������� ���������� �� ������ �����.
                tolstoi.Name = "�. �. �������";                 // ��������� ��� ������.

                Author? pushkin = new Author();                 // ��������� ����� ��������� ������ Author, ��� �������� ���������� �� ������ �����.                                                                            
                pushkin.Name = "�. �. ������";                  // ��������� ��� ������.

                db?.Authors.AddRange(tolstoi, pushkin);         // ��������� ������� � ������� Authors.

                Genre? roman = new Genre() { Name = "�����" };  // ��������� ����� ��������� ������ Genre, ��� �������� ���������� � ����� �����.
                                                                // ��������� �������������� ����, ���������� ������������ �����.
                roman.Name = "�����";

                Genre? historyProse = new Genre();              // ��������� ����� ��������� ������ Genre, ��� �������� ���������� � ����� �����.
                historyProse.Name = "������������ �����";       // ��������� �������������� ����, ���������� ������������ �����.

                db?.Genres.AddRange(roman, historyProse);       // ��������� ����� ����� � ������� Genres.

                voinaIMir.Author = tolstoi;                     // ��������� ����, ���������� ���������� �� ������ �����.
                voinaIMir.Genres.Add(roman);                    // ��������� ����� ����������� ���� � ������ ������ �����.

                evgenijOnegin.Author = pushkin;                 // ��������� ����, ���������� ���������� �� ������ �����.
                evgenijOnegin.Genres.Add(roman);                // ��������� ����� ����������� ���� � ������ ������ �����.
                evgenijOnegin.Genres.Add(historyProse);         // ��������� ����� ����������� ���� � ������ ������ �����.

                db?.SaveChanges();                              // ��������� �������� � ���� ������ ���������.
            }
            else
            {
                label6.Text = 
                    "���������� � ����� ������ �����������.";   // ���� ���� ������ ����������, ����������� ������������ �� �������� ���������� � ���.
            }

            db?.Books.Load();                                   // ��������� �������, �������� ���������� � ������, � ������.
            db?.Authors.Load();                                 // ��������� �������, �������� ���������� �� �������, � ������.
            db?.Genres.Load();                                  // ��������� �������, �������� ���������� � ������, � ������.

            FillDataGridView(dataGridView1, db);                // ��������� ������� DataGridView ����������� �� ���� �����.

            if (dataGridView1.RowCount > 0)                     // ���� DataGridView �������� ������.
            {
                FillCurrentBook();                              // ��������� �������� �����, ��������������� ��� �������� ���������� � ��������� �����.
            }

            cbAuthor.DataSource = 
                db.Authors.Select(a => a.Name).ToList();        // �������� ��� ����� ������� �� ���� ������ � ������� ComboBox.
            cbGenre.DataSource = 
                db.Genres.Select(g => g.Name).ToList();         // �������� ��� ������������ ������ �� ���� ������ � ������� ComboBox.
        }

        protected override void OnClosing(CancelEventArgs e)    // ��� �������� �����.
        {
            base.OnClosing(e);                                  
            db?.Dispose();                                      // ����������� ������� ���������� ��� �������� ���� ������.
            db = null;                                          // �������� �������� ���� ������. 
        }

        /// <summary>
        /// ��������� ������� DataGridView ����������� �� ���� �����.
        /// </summary>
        /// <param name="dgv">  ������� DataGridView.   </param>
        /// <param name="db">   ���� �����.             </param>
        private static void FillDataGridView(DataGridView dgv, HomeLibraryContext db)
        {
            int selectedRowIndex = 0;                       // ���������� ��� �������� ������ ������.
            if (dgv.CurrentCell is not null)                // ���� �������� ������ ����������.
            {
                selectedRowIndex = 
                    dgv.SelectedRows[0].Index;              // ��������� ����� ���������� ������ � ����������.
            }

            List<BookInfo> booksList = [];                  // ��������� � �������������� ������ ��� �������� ���������� � ������.
            List<Genre> listGenres = 
                db.Genres.Include(c => c.Books).ToList();   // ��������� � �������������� ������ ��� �������� ������.
            
            foreach (Book b in db.Books)                    // ��� ������ ����� � ������ ���� �� ���� ������.
            {
                List<string> genres = new();                // ��������� � �������������� ������ ��� �������� �������� ������.
                foreach (Genre genre in listGenres)         // ��� ������� ������������ ����� � ������ ������.
                    foreach (Book book in genre.Books)      // ��� ������ �����, ��������� � ������� ������.
                        if (book == b)                      // ���� ��������� ����� ��������� � ��������������� � ������ ������ ������.
                            genres.Add(genre.Name);         // ��������� �������� ����� � ������, ��������������� ��� �������� �������� ������ ������� �����.

                string genresInOneLine = 
                    string.Join(", ", [.. genres]);         // ����������� ���������� ������ � ������, � ������������: ������� � ������ ����� ��.

                BookInfo selectedBook = new BookInfo();     // ��������� � �������������� ��������� ������, ���������������� ��� �������� ���������� � �����. 
                selectedBook.ID = b.Id.ToString();          // ��������� ����, ��������������� ��� �������� ������������������ ������ ����� � ��.
                selectedBook.Name = b.Name;                 // ��������� ����, ��������������� ��� �������� �������� �����.
                selectedBook.Author = b.Author.Name;        // ��������� ����, ��������������� ��� �������� ����� ������.
                selectedBook.Genres = genresInOneLine;      // ��������� ����, ��������������� ��� �������� ������ ������.
                selectedBook.Description = b.Description;   // ��������� ����, ��������������� ��� �������� �������� �����.

                booksList.Add(selectedBook);                // ��������� ���������� ������ � ������.
            }

            BindingSource source = new BindingSource();     // ��������� � �������������� ��������� BindingSource.
            source.DataSource = booksList;                  // ��������� ������ � ����������� � ������, ��� �������� ������.
            dgv.DataSource = source;                        // ����������� ��������� ����� ��������� � �������� ����� DataGridView.
            
            dgv.Columns[0].Visible = false;                 // �������� ������ ������� ������� �������� ����� DataGridView.
            dgv.Columns[1].HeaderText = "��������";         // ������������� ��������� ������ �������.
            dgv.Columns[2].HeaderText = "�����";            // ������������� ��������� ������� �������.
            dgv.Columns[3].HeaderText = "����";             // ������������� ��������� �������� �������.
            dgv.Columns[4].HeaderText = "��������";         // ������������� ��������� ����� �������.

            if (selectedRowIndex >= dgv.RowCount)           // ���� ����� ���������� ������ ����� ��� ��������� ����� ���������� �����.
                selectedRowIndex--;                         // ��������� �������� ������ ���������� ������ �� 1.
            dgv.CurrentCell = dgv[1, selectedRowIndex];     // �������� ������ ������������ ������ ������.
        }

        /// <summary>
        /// ��������� ������ �� ����������� ������ ������.
        /// </summary>
        /// <param name="genres"> ������ ������. </param>
        /// <returns> ������, ���������� �����. </returns>
        private static string ListGenresToString(List<Genre> genres)
        {
            List<string> lines = new List<string>();        // ��������� � �������������� ������ ����� ��� ��������  
            foreach (Genre g in genres)                     // ��� ������� ����� � ������ ������.
                lines.Add(g.Name);                          // ��������� ������������ ����� � ������.                           
            string line = string.Join(", ", [.. lines]);    // ���������� ������ � ������ � ������������: ������� � ������ ����� ��.

            return line;
        }

        /// <summary>
        /// ������� ������ � ���� ������.
        /// </summary>
        /// <param name="db">       �������� ���� ������.   </param>
        /// <param name="name">     ��� ������.             </param>
        /// <returns> ��������� ������ Author.</returns>
        private Author? FindAuthor(string name, HomeLibraryContext db)
        {
            IQueryable<Author> authors = 
                db.Authors.Where(a => a.Name == name);      // ������� ������� � ���� ������, � ������ ������ �������� ����������� ��������� "name".

            Author? author = new();                         // ��������� � �������������� ��������� ������ Author, ��� �������� ���������� �� ������ �����.
            if (authors.Any())                              // ���� ������ ���� �� ���� �����.
            {
                author = authors.First();                   // �������� ������� ������ �� ������.
            }
            else
            {                                               // ����� (���� �� ���� ����� �� ������).
                author = new Author();                      // ��������� � �������������� ��������� ������ Author, ��� �������� ���������� �� ������ �����. 
                author.Name = name;                         // ����� �������� ��������� "name", ��� �������� ���� "Name" � ��� ������.
                db.Add(author);                             // ��������� ������ ������ � ���� ������.
            }

            return author;
        }

        /// <summary>
        /// ������� ��������� ����� � ���� ������.
        /// </summary>
        /// <returns> ��������� �����. </returns>
        private Book? FindSelectedBook()
        {
            int index = dataGridView1.SelectedRows[0].Index;                    // �������� �������� ������ ���������� ������ � �������� DataGridView.
            int id = 0;                                                         // ���������� ��� �������� �������� ������ ������� Id ���������� ������.
            Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);   // �������� �������� Id �� �������� DataGridView.
                                                                                // ��������� ������� � ����� ��������� ������������� ���������� "id".
            Book? book = new Book();                                            // ��������� � �������������� ��������� ������ Book.
            book = db?.Books.Find(id);                                          // ������� ������ � ��������������� ��������� ���� Id � ������� Books.

            return book;
        }

        /// <summary>
        /// ��������� �������� �����, ��������������� ��� �������� ���������� � ��������� �����.
        /// </summary>
        private void FillCurrentBook()
        {
            Book? book = FindSelectedBook();                    // ������� � ���� ������ �����, ��������������� ���������� � DataGridView.
            tbName.Text = book?.Name;                           // �������� ���������� � ������������ ����� �� ���� ������ � ���� Text �������� TextBox.
            cbAuthor.Text = book?.Author.Name;                  // �������� ���������� �� ������ ����� �� ���� ������ � ���� Text �������� ComboBox.
            cbGenre.Text = ListGenresToString(book.Genres);     // �������� ���������� � ������ ����� �� ���� ������ � ���� Text �������� ComboBox. 
            rtbDescription.Text = book?.Description;            // �������� �������� ����� �� ���� ������ � ���� Text �������� RichTextBox.
        }

        /// <summary>
        /// ��������� ����� ����� � ���� ������.
        /// </summary>
        private void CreateBook(HomeLibraryContext db)
        {
            if (!string.IsNullOrWhiteSpace(tbName.Text))            // ���� ������� TextBox "tbName" �� ����� �������� �����.
            { 
                Author? author = new Author();                      // ��������� � �������������� ��������� ������ Author.
                author = FindAuthor(cbAuthor.Text.ToString(), db);  // ������� ������ � ���� ������ � ������������� � �������� ��������.

                Book book = new Book();                             // ��������� � �������������� ��������� ������ Book.
                book.Name = tbName.Text;                            // �������� ������������ ����� �� ���� Text �������� TextBox.
                book.Author = author;                               // ������������� ����� ���������� ������, ��� ������ �����.
                book.Description = rtbDescription.Text;             // �������� �������� �� ���� Text �������� RichTextBox.

                db?.Add(book);

                List<string> genresFromDb = [.. db?.Genres.Select(g => g.Name)];    // ��������� ������ �����,
                                                                                    // ���������� ��� ������������ ������, ���������� � ���� ������.

                List<string> genresFromForm = [.. cbGenre.Text.Split(", ")];        // ��������� ������ ����� �� ������������ ������,
                                                                                    // ��������� � ���� Text �������� ComboBox.

                foreach (string genre in genresFromForm)                            // ��� ������� ������ ������������ �����.
                {
                    if (!genresFromDb.Contains(genre))                              // ���� � ���� ������ �� ���������� ������� ������������ �����.
                    {
                        Genre newGenre = new Genre();                               // ��������� � �������������� ��������� ������ Genre.
                        newGenre.Name = genre;                                      // ��������� ������������ �����.
                        db?.Genres.Add(newGenre);                                   // ��������� ��������� ���� � ���� ������.        
                        db?.SaveChanges();                                          // ��������� �������� � ���� ������ ���������.
                    }
                    db?.Genres.FirstOrDefault(g => g.Name == genre).Books.Add(book);// ��������� ��������� ����� ��������� ����� � ���� Books,
                                                                                    // ������� Genres ��� ������ � ������ �������� ������������ �����.
                }

                int? result = db?.SaveChanges();                                     // ��������� �������� � ���� ������ ���������.
                if (result > 0)
                {
                    label6.Text = "���������� � �����: \"" + book.Name + 
                                  "\" ��������� � ���� ������.";                    // ����������� ������������� � ���������� ������ � ���� ������.
                }
            }
            else
            {                                                                       // ����� (���� ������� TextBox "tbName" �� ����� �� �������� �����).
                label6.Text = "���� \"��������\" ������ ���� ���������!";           // ����������� ������������� � ��������.
            }     
        }

        /// <summary>
        /// ������������ ������ ��������� ����� � ���� ������.
        /// </summary>
        private void UpdateBook(HomeLibraryContext db)
        {
            Author? author = new Author();                          // ��������� � �������������� ��������� ������ Author.
            author = FindAuthor(cbAuthor.Text.ToString(), db);      // ������� � ���� ������ ������ � ��������� � �������� ComboBox ������.
                                                                    // ������������� ���������� ������ � �������� �������� ���������� �������.

            Book? book = new Book();                                // ��������� � �������������� ��������� ������ Book. 
            book = FindSelectedBook();                              // ������� ��������� ����� � ���� ������ � ������������� � �������� ��������. 
            
            if (author is not null & book is not null)              // ���� ����� � ����� ����������.
            {
                string? prevName = book.Name;                       // ��������� ������� ������������ ����� � ����������.
                book.Name = tbName.Text;                            // ������������� �������� ���� "Name" ������ �������� ���� Text �������� TextBox.
                book.Author = author;                               // ������������� ��������� ���� "Author" ��������� ����� ������ ������ Author.

                List<string> allGenres = new List<string>();        // ��������� � �������������� ������ ��� �������� ������������ ������ �� ���� ������. 
                allGenres = [.. db?.Genres.Select(g => g.Name)];    // �������� ��� ������������ ������, ��������� � ���� ������, � ������.
         
                List<string> genresString = new List<string>();     // ��������� � �������������� ������ ��� �������� ������������ ������. 
                genresString = [.. cbGenre.Text.Split(", ")];       // �������� ������������ ������, ��������� � �������� ���� Text, �������� ComboBox, � ������.
                book.Genres.Clear();                                // ������� �������� �� ���� Genres.
                foreach (string genre in genresString)              // ��� ������� ������������ ����� � ������.
                {
                    if (!allGenres.Contains(genre))                 // ���� ������� ������������ �� ���������� � ���� ������.
                    {
                        Genre newGenre = new Genre();               // ��������� � �������������� ��������� ������ Genre.
                        newGenre.Name = genre;                      // ��������� ������������ �����.
                        db?.Genres.Add(newGenre);                   // ��������� ��������� ���� � ���� ������.   
                        db?.SaveChanges();                          // ��������� �������� � ���� ������ ���������.
                    }
                    book.Genres.Add(db?.Genres.FirstOrDefault(
                                        g => g.Name == genre));     // ��������� � ���� Genres ��������� ����� � ������ ������ �������� ������������ �����.         
                }
                
                book.Description = rtbDescription.Text;             // ������������� �������� ���� "Description" ������ �������� ���� Text �������� RichTextBox. 
                dataGridView1.Update();                             // ��������� ������� DataGridView.                
                int? result = db?.SaveChanges();
                if (result > 0)                                     // ��������� �������� � ���� ������ ���������.
                    label6.Text = "���������� � �����: \"" 
                                  + prevName + "\" ���������.";     // ����������� ������������ � ������������ �����������.
            }
        }

        /// <summary>
        /// ������� ��������� ����� �� ���� ������.
        /// </summary>
        private void DeleteBook(HomeLibraryContext db)
        {
            Book? book = new Book();                                // ��������� � �������������� ��������� ������ Book. 
            book = FindSelectedBook();                              // ������� ���������� � �������� DataGridView ����� � ���� ������
                                                                    // � ������������� � �������� �������� �������.

            if (book is not null)                                   // ���� ����� ����������.
            {
                db?.Books.Remove(book);                             // ������� ��������� ����� �� ���� ������.
                int? result = db?.SaveChanges();                    // ��������� �������� � ���� ������ ���������. 
                if (result > 0)
                    label6.Text = "����� \"" + tbName.Text + 
                                "\" ������� �� ���� ������.";       // ����������� ������������ �� �������� ������ �� ���� ������. 
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillCurrentBook();                                      // ��������� �������� �����, ��������������� ��� �������� ���������� � ��������� �����.
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateBook(db);                                         // ��������� ����� ����� � ���� ������.
            FillDataGridView(dataGridView1, db);                    // ��������� ������� DataGridView ����������� �� ���� �����.
            dataGridView1.CurrentCell = 
                this.dataGridView1[1, dataGridView1.RowCount - 1];  // �������� ��������� ������ � ������� �������� ����� DataGridView.
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateBook(db);                                         // ������������ ������ ��������� ����� � ���� ������.
            FillDataGridView(dataGridView1, db);                    // ��������� ������� DataGridView ����������� �� ���� �����.
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteBook(db);                                         // ������� ��������� ����� �� ���� ������.
            FillDataGridView(dataGridView1, db);                    // ��������� ������� DataGridView ����������� �� ���� �����.
            if (dataGridView1.RowCount > 0)
            {
                FillCurrentBook();                                  // ��������� �������� �����, ��������������� ��� �������� ���������� � ��������� �����.
            }
        }

        private void cbAuthor_DropDown(object sender, EventArgs e)
        {
            cbAuthor.DataSource = 
                db?.Authors.Select(a => a.Name).ToList();           // ��������� ������� ComboBox ������� ��� ���� ������� �� ���� �����.
        }

        private void cbGenre_DropDown(object sender, EventArgs e)
        {
            cbGenre.DataSource = 
                db?.Genres.Select(g => g.Name).ToList();            // ��������� ������� ComboBox ������� ���� ������ �� ���� �����.
        }
    }
}
