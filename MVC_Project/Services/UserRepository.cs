using MVC_Project.Data;
using MVC_Project.Helpers;
using MVC_Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Project.Services
{
    public interface IUserRepository
    {
        IQueryable<User> GetUserList { get; }
        User GetUser(string id);
        bool AddUser(RegisterUser user);
        bool UpdateUser(string id, UpdateUser newDetails);
    }

    public class UserRepository : IUserRepository
    {
        StoreDataContext _context;

        public UserRepository(StoreDataContext ctx) => _context = ctx;

        public IQueryable<User> GetUserList => _context.Users;
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

        public bool UpdateUser(string id, UpdateUser newDetails)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id.Equals(id));
            if (user == null)
                return false;

            user.FirstName = newDetails.FirstName;
            user.LastName = newDetails.LastName;
            user.BirthDate = newDetails.BirthDate;


            return _context.SaveChanges() > 0;
        }

        bool ValidateRegisterUser(RegisterUser newUser) => newUser.Validate();
    }
}
