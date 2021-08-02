﻿using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using Universalis.DbAccess.MarketBoard;
using Universalis.DbAccess.Queries.MarketBoard;
using Xunit;

namespace Universalis.DbAccess.Tests.MarketBoard
{
    public class CurrentlyShownDbAccessTests
    {
        public CurrentlyShownDbAccessTests()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            client.DropDatabase(Constants.DatabaseName);
        }

        [Fact]
        public async Task Create_DoesNotThrow()
        {
            var db = new CurrentlyShownDbAccess(Constants.DatabaseName);
            var document = SeedDataGenerator.MakeCurrentlyShown(74, 5333);
            await db.Create(document);
        }

        [Fact]
        public async Task Retrieve_DoesNotThrow()
        {
            var db = new CurrentlyShownDbAccess(Constants.DatabaseName);
            var output = await db.Retrieve(new CurrentlyShownQuery { WorldId = 74, ItemId = 5333 });
            Assert.Null(output);
        }

        [Fact]
        public async Task RetrieveMany_DoesNotThrow()
        {
            var db = new CurrentlyShownDbAccess(Constants.DatabaseName);
            var output = await db.RetrieveMany(new CurrentlyShownManyQuery { WorldIds = new uint[] { 74 }, ItemId = 5333 });
            Assert.NotNull(output);
            Assert.Empty(output);
        }

        [Fact]
        public async Task Update_DoesNotThrow()
        {
            var db = new CurrentlyShownDbAccess(Constants.DatabaseName);
            var document = SeedDataGenerator.MakeCurrentlyShown(74, 5333);
            await db.Update(document, new CurrentlyShownQuery { WorldId = document.WorldId, ItemId = document.ItemId });
        }

        [Fact]
        public async Task Delete_DoesNotThrow()
        {
            var db = new CurrentlyShownDbAccess(Constants.DatabaseName);
            await db.Delete(new CurrentlyShownQuery { WorldId = 74, ItemId = 5333 });
        }

        [Fact]
        public async Task Create_DoesInsert()
        {
            var db = new CurrentlyShownDbAccess(Constants.DatabaseName);

            var document = SeedDataGenerator.MakeCurrentlyShown(74, 5333);
            await db.Create(document);

            var output = await db.Retrieve(new CurrentlyShownQuery { WorldId = document.WorldId, ItemId = document.ItemId });
            Assert.NotNull(output);
        }

        [Fact]
        public async Task RetrieveMany_ReturnsData()
        {
            var db = new CurrentlyShownDbAccess(Constants.DatabaseName);

            var document = SeedDataGenerator.MakeCurrentlyShown(74, 5333);
            await db.Create(document);

            var output = (await db.RetrieveMany(new CurrentlyShownManyQuery { WorldIds = new[] { document.WorldId }, ItemId = document.ItemId }))?.ToList();
            Assert.NotNull(output);
            Assert.Single(output);
            Assert.Equal(document.WorldId, output[0].WorldId);
            Assert.Equal(document.ItemId, output[0].ItemId);
            Assert.Equal(document.LastUploadTimeUnixMilliseconds, output[0].LastUploadTimeUnixMilliseconds);
            Assert.Equal(document.Listings, output[0].Listings);
            Assert.Equal(document.RecentHistory, output[0].RecentHistory);
            Assert.Equal(document.UploaderIdHash, output[0].UploaderIdHash);
        }
    }
}