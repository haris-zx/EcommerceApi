using DummyProject.Models;

namespace DummyProject.Interface
{
    public interface IAuthServiceInterface
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public string CreateToken(User user);
        public RefreshToken GenerateRefreshToken();
        public User SetRefreshToken(RefreshToken newRefreshToken, User user);
        public bool IsValid(string email);
    }
}
