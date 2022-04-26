global using System;
global using System.Threading.Tasks;
global using System.Linq;
global using System.Collections;
global using System.Collections.Generic;
global using System.Threading;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Storage;

global using Xero.Products.DataLayer.Repositories;
global using Entities = Xero.Products.DataLayer.Entities;
global using Xero.Products.BusinessLogic.Models;
global using Xero.Products.BusinessLogic.Services;

global using Xunit;
global using FluentAssertions;
global using Moq;