namespace Xero.Products.App.Authentication.Repositories;
public interface IUserRepository
{
    UserDTO? GetUser(UserModel userMode);
}
