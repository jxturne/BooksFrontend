using BooksFrontend.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BooksFrontend.Models;

namespace BooksFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ICRUD crud;
        Book newBook = new Book();
        Book? selectedBook = null;

        public MainWindow(ICRUD crud)
        {
            InitializeComponent();
            BooksDataGrid.ItemsSource = crud.GetBook();
            this.crud = crud;
            LoadBooks();

        }
        private void LoadBooks()
        {
            BooksDataGrid.ItemsSource = crud.GetBook();
        }
        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            crud.Create(newBook);
            MessageBox.Show("Book added successfully!");
            LoadBooks();
            newBook = new Book();

        }
        private void BooksDataGrid_Select(object sender, SelectionChangedEventArgs e)
        {
            selectedBook = (Book)BooksDataGrid.SelectedItem;
            if (selectedBook != null)
            {
              
                MessageBox.Show("Book selected: " + selectedBook.Title);
            }
            MessageBox.Show("No book selected.");
        }
        private void UpdateBookButton_Click(object sender, RoutedEventArgs e)
            
        {
            if (selectedBook != null)
            {
                crud.Update(selectedBook);
                MessageBox.Show("Book updated successfully!");
                BooksDataGrid.ItemsSource = crud.GetBook();
            }
        }
        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook != null)
            {
                crud.Delete(selectedBook);
                MessageBox.Show("Book deleted successfully!");
                BooksDataGrid.ItemsSource = crud.GetBook();
                selectedBook = null;
                LoadBooks();
            }
        }
        
    }
}