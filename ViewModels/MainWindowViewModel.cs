using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookstore.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Bookstore.Models.Users;
using Bookstore.View;
using System.Windows;
using Microsoft.Win32;

namespace Bookstore.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly DataBaseContext _db;
        public ObservableCollection<Product> Products { get; set; } = [];
        public ObservableCollection<User> Users { get; set; }
        private User? _authorizedUser;
        public User? AuthorizedUser
        {
            get
            {
                return _authorizedUser;
            }
            set
            { 
                SetField(ref _authorizedUser, value);
            }
        }

        RelayCommand? registerCommand;
        RelayCommand? loginCommand;
        RelayCommand? profileCommand;

        private string? _searchText;
        public string? SearchText
        {
            get => _searchText;
            set => SetField(ref _searchText, value);
        }
        public ICommand CommandSearch { get; }

        public MainWindowViewModel()
        {
            var connectionString = App.Current.Resources["ConnectionString"] as string;
            Console.WriteLine("Connection: " + connectionString);
            _db = new DataBaseContext(connectionString);
            var result = _db.Products.Include(p => p.publisherHouse).ToList();
            InitProducts(result);

            CommandSearch = new RelayCommand(Search);
        }

        private void InitProducts(IEnumerable<Product> products)
        {
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private void Search(object? parameter = null)
        {
            IEnumerable<Product> res;
            if (SearchText != null)
            {
                res = _db.Products
                     .Where(b => b.BookTitle.ToLower().Contains(SearchText.ToLower()) || b.Author.ToLower().Contains(SearchText.ToLower()))
                     .Include(p => p.publisherHouse)
                     .ToList();                           
            }
            else
            {
                res = _db.Products.Include(p => p.publisherHouse).ToList();
            }
            InitProducts(res);
        }

        public RelayCommand RegisterCommand
        {
            get
            {
                if (registerCommand != null)
                {
                    return registerCommand;
                }

                registerCommand = new RelayCommand((o) =>
                {
                    Register register = new Register(new RegisterUser());

                    if (register.ShowDialog() == true)
                    {
                        User user = register.User;

                        if (String.IsNullOrEmpty(register.User.UserName))
                        {
                            MessageBox.Show("Поле логин не заполнено", "Ошибка");
                            return;
                        }

                        var result = _db.Users
                                         .Where(un => un.UserName == register.User.UserName && un.Password == register.User.Password)
                                         .Include(un => un.Person)
                                         .Count();

                        if(result != 0)
                        {
                            MessageBox.Show("Такой пользователь уже существует", "Ошибка");
                            return;
                        }

                        if (String.IsNullOrEmpty(register.User.Password))
                        {
                            MessageBox.Show("Поле пароль не заполнено", "Ошибка");
                            return;
                        }

                        if (register.User.Password != register.User.RepeatPassword)
                        {
                            MessageBox.Show("Пароли не совпадают", "Ошибка");
                            return;
                        }


                        _db.Users.Add(user);
                        _db.SaveChanges();

                        AuthorizedUser = user;

                    }
                });
                return registerCommand;
            }
        }

        public RelayCommand LoginCommand
        {
            get 
            {
                if (loginCommand != null)
                {
                    return loginCommand;
                }

                loginCommand = new RelayCommand((o) =>
                {
                    Login login = new Login(new LoginUser());
                    if (login.ShowDialog() == true)
                    {
                        if (String.IsNullOrEmpty(login.LoginUser.UserName))
                        {
                            MessageBox.Show("Логин пустой", "Ошибка");
                            return;
                        }

                        if (String.IsNullOrEmpty(login.LoginUser.Password))
                        {
                            MessageBox.Show("Пароль пустой", "Ошибка");
                            return;
                        }
                        try
                        {
                            User user = _db.Users
                                .Where(un => un.UserName == login.LoginUser.UserName && un.Password == login.LoginUser.Password)
                                .Include(un => un.Person)
                                .First();
                            AuthorizedUser = user;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Пользователь не найден", "Ошибка");
                            return;
                        }
                    }
                });
                return loginCommand;
            }
        }

        public RelayCommand ProfileCommand
        {
            get
            {
                if (profileCommand != null)
                {
                    return profileCommand;
                }

                profileCommand = new RelayCommand((o) =>
                {
                    if(AuthorizedUser != null)
                    {
                        Profile profile = new Profile(AuthorizedUser, _db);
                        profile.Show();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден", "Ошибка");
                    }
                });
                return profileCommand;
            }
        }

    }
}
