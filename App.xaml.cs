using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using BooksFrontend.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksFrontend
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider provider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddDbContext<BooksContext>(options =>
            {
                options.UseSqlite("Data Source=books.db");
            });
            services.AddSingleton<Services.ICRUD, Services.CRUD>();
            services.AddSingleton<MainWindow>();

            provider = services.BuildServiceProvider();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = provider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
