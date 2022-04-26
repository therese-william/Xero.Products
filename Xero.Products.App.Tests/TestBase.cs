namespace Xero.Products.App.Tests;

public class TestBase : EndToEndTest<Startup>
{
    protected CancellationToken _ct => It.IsAny<CancellationToken>();
    protected Guid _anyGuid => It.IsAny<Guid>();

    public TestBase(ITestOutputHelper output) : base(output)
    {
    }
    protected HttpClient CreateUnauthorizedClient() => AppFactory.CreateClient();
    protected HttpClient CreateAdminAuthorizedClient()
    {
        var userRepo = AppFactory.GetService<IUserRepository>();
        var userModel = new UserModel
        {
            UserName = "admin",
            Password = "admin123"
        };
        var userDTO = userRepo.GetUser(userModel);
        return CreateHttpClient(userDTO);
    }
    protected HttpClient CreateUserAuthorizedClient()
    {
        var userRepo = AppFactory.GetService<IUserRepository>();
        var userModel = new UserModel
        {
            UserName = "user",
            Password = "user123"
        };
        var userDTO = userRepo.GetUser(userModel);
        return CreateHttpClient(userDTO);
    }
    protected HttpClient CreateGuestAuthorizedClient()
    {
        var userRepo = AppFactory.GetService<IUserRepository>();
        var userModel = new UserModel
        {
            UserName = "guest",
            Password = "guest123"
        };
        var userDTO = userRepo.GetUser(userModel);
        return CreateHttpClient(userDTO);
    }

    [Fact]
    public void AuthorizedClient_Success()
    {
        using var client = CreateAdminAuthorizedClient();
        var token = client.DefaultRequestHeaders.Authorization?.ToString();
        Assert.True(!string.IsNullOrEmpty(token));
    }
}
