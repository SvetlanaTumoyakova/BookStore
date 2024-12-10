using Bookstore.Models;
using Bookstore.Models.Users;
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
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public RegisterUser User { get; private set; }
        public Register()
        {
            InitializeComponent();
        }
        public Register(RegisterUser user)
        {
            InitializeComponent();

            if(user.Person == null)
            {
                user.Person = new Person();
            }

            User = user;
            DataContext = User;
        }
        void PasswordChangedHandler(Object sender, RoutedEventArgs args)
        {
            User.Password = ((PasswordBox)sender).Password;
        }

        void RepeatPasswordChangedHandler(Object sender, RoutedEventArgs args)
        {
            User.RepeatPassword = ((PasswordBox)sender).Password;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
