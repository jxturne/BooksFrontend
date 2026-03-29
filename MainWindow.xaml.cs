using BooksFrontend.Services;
using System.Windows;
using System.Windows.Controls;
using BooksFrontend.Models;
using System.Net.Http;
using System;

namespace BooksFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient booksApiClient = new();
        private readonly ICRUD crud;
        private Book? selectedBook = null;

        public MainWindow(ICRUD crud)
        {
            InitializeComponent();
            this.crud = crud;
            booksApiClient.BaseAddress = new Uri("http://localhost:5038/api/Books");
            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                var books = crud.GetBook();
                BooksDataGrid.ItemsSource = books;
                
                if (books.Count == 0)
                {
                    MessageBox.Show("No books found. Let's add some now!", 
                        "Empty Database", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadBookButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Create new book with object initializer syntax
            var newBook = new Book
            {
                Isbn = (int)(DateTime.Now.Ticks % int.MaxValue), // Convert to int
                Title = "Sample Book Title",
                Author = "Sample Author",
                Description = "Sample Description"
            };

            try
            {
                crud.Create(newBook);
                MessageBox.Show("Book added successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding book: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BooksDataGrid_Select(object sender, SelectionChangedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book book)
            {
                selectedBook = book;
                MessageBox.Show($"Book selected: {book.Title}", "Selection", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                selectedBook = null;
                // Don't show message when nothing is selected
            }
        }

        private void UpdateBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook != null)
            {
                try
                {
                    crud.Update(selectedBook);
                    MessageBox.Show("Book updated successfully!", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadBooks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating book: {ex.Message}", 
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to update.", "No Selection", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

       
        private async void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook != null)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete '{selectedBook.Title}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        crud.Delete(selectedBook);
                        MessageBox.Show("Book deleted successfully!", "Success", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        selectedBook = null;
                        LoadBooks();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting book: {ex.Message}", 
                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.", "No Selection", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the update method
            UpdateBookButton_Click(sender, e);
        }
    }
}