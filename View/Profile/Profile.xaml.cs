using Bookstore.Models;
using Bookstore.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bookstore.View
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        private readonly DataBaseContext _db;
        public User User { get; set; }
        public Profile()
        {
            InitializeComponent();
        }
        public Profile(User user, DataBaseContext db)
        {
            InitializeComponent();
            User = user;
            _db = db;
            DataContext = User;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _db.Entry(User).State = EntityState.Modified;
            _db.SaveChanges();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
