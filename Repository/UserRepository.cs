using DummyProject.Data;
using DummyProject.Interface;
using DummyProject.Models;

namespace DummyProject.Repository
{
    public class UserRepository : IUserInteface
    {
        public DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;

        }
        public int LastUser()
        {
            User? user = _context.main.OrderBy(u => u.Id).LastOrDefault();
            if (user == null)
                return 0;
            return user.Id;
        }
        public bool AddUser(User user)
        {
            _context.main.Add(user);
            return Save();
        }
        public User GetUser(string userEmail)
        {
            return _context.main.Where(u => u.Email == userEmail).FirstOrDefault();
        }
        public bool UserExist(string userEmail)
        {
            return _context.main.Any(u => u.Email == userEmail);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}