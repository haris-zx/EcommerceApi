using DummyProject.Models;

namespace DummyProject.Interface
{
    public interface IUserInteface
    {

      /*  ICollection<User> GetUsers();
*/

        public int LastUser();
        public bool AddUser(User user);
        public bool UserExist(string userEmail);
        public User GetUser(string userEmail);
        public bool Save();
    }
}
