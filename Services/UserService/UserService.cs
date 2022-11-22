using System.Security.Cryptography;
using System.Text.RegularExpressions;

using ConnectPgSql;

namespace ConnectPgSql
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;

        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public string Login(UserDto user)
        {
            var loggedUser = _userContext.Users.FirstOrDefault(u => u.Username == user.Username);
            if(loggedUser == null) return "";

            if(!VerifyPasswordHash(user.Password, loggedUser.Password, loggedUser.Salt)) {
                return "";
            }

            return "Token";
        }

        public async Task<User> Register(UserDto user)
        {
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User newUser = new User {
                Username = user.Username,
                Password = passwordHash, 
                Salt = passwordSalt
                };
            await _userContext.Users.AddAsync(newUser);
            await _userContext.SaveChangesAsync();
            return newUser;
        }

        public bool CheckIfUsernameExists(UserDto user)
        {
            var existingUser = _userContext.Users.FirstOrDefault(u => u.Username == user.Username);
            if(existingUser != null) return true;
            return false;
        }

        public bool CheckIfStrongPassword(UserDto user)
        {
            string pattern = "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$";
            Match m = Regex.Match(user.Password, pattern, RegexOptions.IgnoreCase);
            if(m.Success)
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}