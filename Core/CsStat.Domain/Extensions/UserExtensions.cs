using CsStat.Domain.Entities;

namespace DataService.Extensions
{
    public static class UserExtensions
    {
        public static bool VerifyPassword(this User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}