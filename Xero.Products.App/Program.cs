using Xero.Products.App.Middleware;
using Microsoft.EntityFrameworkCore;
using Xero.Products.DataLayer.Repositories;
using Xero.Products.DataLayer.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Xero.Products.App.Authentication;
using Microsoft.AspNetCore.Builder;
using Xero.Products.App;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
var environment = builder.Environment;

var startup = new Startup(config, environment);
startup.ConfigureServices(services);

var app = builder.Build();

startup.Configure(app);

app.Run();
