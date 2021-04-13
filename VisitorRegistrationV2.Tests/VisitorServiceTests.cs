using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Data;
using VisitorRegistrationV2.Data.Services.Visitors;
using Xunit;

namespace VisitorRegistrationV2.Tests
{
    public class VisitorServiceTests
    {
        (DbContextOptions, IOptions<OperationalStoreOptions>) options = InMemoryTestDbSetup.CreateNewContextOptions();
        UserManager<Registrar> manager;
        ApplicationDbContext db;
        IVisitors context;
        string RegistrarId;

        const int TestVisitorId1 = 1;
        const int TestVisitorId2= 2;
        const int TestVisitorId3 = 3;


        public VisitorServiceTests()
        {
            db = InMemoryTestDbSetup.CreateContext(options);
            manager = InMemoryTestDbSetup.CreateUsermanager(db);
            InMemoryTestDbSetup.Seed(manager, db);
            context = new Visitors(db);
            RegistrarId = db.Registrars.First().Id;
        }

        [Fact]
        public async void GetListOfVisitorsTest()
        {

            var result = await context.GetListOfVisitors();
            var value = result.Count();


            Assert.NotEqual(0, value);
        }

        [Fact]
        public async void GetVisitorByIdTest()
        {
            var result = await context.GetVisitorById(TestVisitorId1);

            string fullname = result.FullName;
            string expectedFullname = "Rowan Brouwer";

            Assert.Equal(fullname, expectedFullname);
        }

        [Fact]
        public async void UpdateVisitorTest()
        {
            var user = await context.GetVisitorById(TestVisitorId1);

            user.MiddleName = "Test";

            await context.UpdateVisitor(user);

            var userAfterUpdate = await context.GetVisitorById(TestVisitorId1);
            var ExpectedMiddleName = "Test";

            Assert.Equal(userAfterUpdate.MiddleName, ExpectedMiddleName);
        }

        [Fact]
        public async void DeleteVisitorById()
        {
            var user = await context.GetVisitorById(TestVisitorId1);

            await context.DeleteVisitor(TestVisitorId1);

            var DeletedUser = await context.GetVisitorById(TestVisitorId1);

            Assert.Null(DeletedUser);
        }

        [Fact]
        public async void AddVisitorTest()
        {
            Visitor newVisitor = new Visitor { ArrivalTime = DateTime.Now, FirstName = "Test", LastName = "Person" };

            var lookUpNewVisitor = await context.AddVisitor(newVisitor);

            Assert.Equal(7, lookUpNewVisitor.Id);
        }


        // need to figure the linq behind the method
        //[Fact]
        //public async void GetVisitorListBySearchTermTest()
        //{
        //    string Name = "Rowan Brouwer";

        //    var SearchResult = await context.GetVisitorListBySearchTerm(Name);

        //    var RowanVisitor = SearchResult.FirstOrDefault(v => v.FullName() == Name);

        //    Assert.Equal(Name, RowanVisitor.FullName());
        //}

    }
}
