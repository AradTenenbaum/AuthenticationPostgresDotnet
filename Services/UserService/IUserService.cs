using ConnectPgSql;

namespace ConnectPgSql
{
    public interface IUserService
    {
        Task<User> Register(UserDto user);
        string Login(UserDto user);
        List<User> GetUsers();
        bool CheckIfUsernameExists(UserDto user);
        bool CheckIfStrongPassword(UserDto user);
    }
}