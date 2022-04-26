namespace Xero.Products.App.Authentication.Repositories;

public class UserRepository: IUserRepository
{
    private readonly List<UserDTO> users = new List<UserDTO>();

    public UserRepository()
    {
        users.Add(new UserDTO
        {
            UserName = "admin",
            Password = "admin123",
            Role = "Admin"
        });
        users.Add(new UserDTO
        {
            UserName = "user",
            Password = "user123",
            Role = "User"
        });
        users.Add(new UserDTO
        {
            UserName = "guest",
            Password = "guest123",
            Role = "Guest"
        });
    }
    public UserDTO? GetUser(UserModel userModel)
    {
        return users.FirstOrDefault(x => x.UserName.ToLower() == userModel.UserName.ToLower()
                                 && x.Password == userModel.Password);
    }
}

