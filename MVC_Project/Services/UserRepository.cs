using MVC_Project.Data;
using MVC_Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Project.Services
{
    public interface IUserRepository
    {
        List<User> GetUserList();
        User GetUser(string id);
        bool AddUser(RegisterUser user);
    }

    public class UserRepository : IUserRepository
    {
        StoreDataContext _context;

        public UserRepository(StoreDataContext ctx) => _context = ctx;

        public List<User> GetUserList() => _context.Users.ToList();
        public User GetUser(string id) => _context.Users.FirstOrDefault(u => u.Id == id);

        public bool AddUser(RegisterUser newUser)
        {
            if (!ValidateRegisterUser(newUser))
                return false;

            var user = new User
            {
                UserName = newUser.UserName,
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                BirthDate = newUser.BirthDate,
            };
            _context.Users.Add(user);
            return _context.SaveChanges() > 0;
        }

        bool ValidateRegisterUser(RegisterUser newUser)
        {
            return newUser != null
                && !string.IsNullOrEmpty(newUser.UserName)
                && !string.IsNullOrEmpty(newUser.Password)
                && !string.IsNullOrEmpty(newUser.Email)
                && !string.IsNullOrEmpty(newUser.FirstName)
                && !string.IsNullOrEmpty(newUser.LastName)
                && newUser.BirthDate != null;
        }
    }
}
