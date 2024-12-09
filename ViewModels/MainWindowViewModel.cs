using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookstore.Models;
using System.Collections.ObjectModel;

namespace Bookstore.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly DataBaseContext _db;

        public ObservableCollection<Product> Products { get; set; } = [];
        public MainWindowViewModel()
        {
            var connectionString = App.Current.Resources["ConnectionString"] as string;
            Console.WriteLine("Connection: " + connectionString);
            _db = new DataBaseContext(connectionString);
            _db.Products.Include(p => p.publisherHouse).Load();

            Products = _db.Products.Local.ToObservableCollection();
        }

    }
}
