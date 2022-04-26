namespace Xero.Products.App.Authentication;

public interface ITokenService
{
    string BuildToken(string key, UserDTO user);
    bool IsTokenValid(string key, string token);
}

