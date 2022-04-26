using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Xero.Products.App.Authentication;
using Xero.Products.App.Authentication.Models;
using Xunit.Abstractions;

namespace Xero.Products.App.Tests;

public abstract class EndToEndTest<TStartup> : IClassFixture<AppFactory<TStartup>> where TStartup : class
{
    protected readonly ITestOutputHelper Output;
    protected readonly AppFactory<TStartup> AppFactory;

    //use this for same shared factory
    public EndToEndTest(AppFactory<TStartup> factory, ITestOutputHelper output)
    {
        AppFactory = factory;
        Output = output;
    }

    //user this for new factory for each test
    public EndToEndTest(ITestOutputHelper output) : this(new AppFactory<TStartup>(), output)
    {
    }

    protected virtual string GetTokenFromJwtProvider(UserDTO userDTO)
    {
        var tokenService = AppFactory.GetService<ITokenService>();
        var key = Configuration["JwtSecret"].ToString();
        if (tokenService == null)
            throw new Exception("tokenService is not added");

        var token = tokenService.BuildToken(key, userDTO);
        return token;
    }

    protected HttpClient CreateHttpClient(UserDTO? userDTO = null)
    {
        var client = AppFactory.CreateClient();

        if (userDTO != null)
        {
            var token = GetTokenFromJwtProvider(userDTO);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        return client;
    }

    protected IConfiguration Configuration => AppFactory.Configuration;

    protected T GetService<T>() => AppFactory.GetService<T>();
    protected bool TryGetService<T>(out T service) => AppFactory.TryGetService<T>(out service);
    protected T GetConfig<T>(string section) => AppFactory.GetConfig<T>(section);
}
