using System;
using System.IO;
using System.Linq;
using DeepEqual.Syntax;
using RatStash;
using Xunit;

namespace RatStashTest;

public class DatabaseTest : TestEnvironment
{
	[Fact]
	public void LoadEnglishDatabase()
	{
		var database = GetDatabase();
		var item = database.GetItem("5bc9b720d4351e450201234b");
		Assert.Equal("Golden 1GPhone smartphone", item.Name);
		Assert.Equal((1, 1), item.GetSlotSize());
	}

	[Fact]
	public void LoadGermanDatabase()
	{
		var database = GetDatabase("ge");
		var item = database.GetItem("5bc9b720d4351e450201234b");
		Assert.Equal("Goldenes 1GPhone", item.Name);
		Assert.Equal((1, 1), item.GetSlotSize());
	}

	[Fact]
	public void LoadRussianDatabase()
	{
		var database = GetDatabase("ru");
		var item = database.GetItem("5bc9b720d4351e450201234b");
		Assert.Equal("Золотой смартфон 1GPhone", item.Name);
		Assert.Equal((1, 1), item.GetSlotSize());
	}

	[Fact]
	public void LoadAllLocalizedDatabases()
	{
		foreach (var locale in Enum.GetValues<Language>())
		{
			var database = GetDatabase(locale.ToBSGCode());
			var item = database.GetItem("5bc9b720d4351e450201234b");
			Assert.False(item.Name == "");
		}
	}

	[Fact]
	public void QueryItemById()
	{
		var database = GetDatabase();
		var item = database.GetItem("5644bd2b4bdc2d3b4c8b4572");
		Assert.NotNull(item);
		Assert.Equal(typeof(AssaultRifle), item.GetType());
		var arItem = (AssaultRifle)item;
		var slots = arItem.Slots;
		Assert.Equal(10, slots.Count);
		Assert.Equal("mod_muzzle", slots[2].Name);
		Assert.NotEmpty(slots[2].Filters);
		Assert.Contains(slots[2].Filters[0].Whitelist, id => id == "5ac72e945acfc43f3b691116");
	}

	[Fact]
	public void QueryItemByDiscriminator()
	{
		var database = GetDatabase();
		var items = database.GetItems(item =>
		{
			File.AppendAllText("test.txt", $"{item.Id} -> {item.ShortName}\n");
			return item.ShortName == "MRE";
		}).ToArray();
		Assert.True(items.Any());
		var expected = database.GetItem("590c5f0d86f77413997acfab");
		var actual = items.First();
		Assert.True(expected.IsDeepEqual(actual));
	}

	[Fact]
	public void QueryMaxItem()
	{
		var database = GetDatabase();
		var maxItem = database.GetItem(item => item.Name.Length);
		Assert.Equal(78, maxItem.Name.Length);
		Assert.Equal("AR-15 Strike Industries Advanced Receiver Extension buffer tube (Anodized Red)", maxItem.Name);
	}

	[Fact]
	public void QueryByName()
	{
		var database = GetDatabase().Filter(i => i.Name != "" && i.ShortName != "");
		var query = "A8h 12 p0Iymer";
		var result = database.GetItem(item => NormedLevenshteinDistance(item.Name, query));
		Assert.StartsWith("ASh-12 polymer handguard", result.Name);
	}

	[Fact]
	public void QueryAllItems()
	{
		var database = GetDatabase();
		var items = database.GetItems().ToArray();
		Assert.Equal(3731, items.Length);
		Assert.DoesNotContain(null, items);
	}

	[Fact]
	public void CheckCleanDatabase()
	{
		var database = GetDatabase(null, true);
		var item = database.GetItem("5bf3e0490db83400196199af");
		Assert.Equal("Aвтомат Kалашникова AK-74H 5.45x39", item.Name);  // "AK" are latin chars
		Assert.Equal("AK-74N 5.45x39", item.ShortName);                 // "AK" are latin chars
	}

	[Fact]
	public void CheckUncleanDatabase()
	{
		var database = GetDatabase(null, false);
		var item = database.GetItem("5bf3e0490db83400196199af");
		Assert.Equal("Автомат Калашникова АК-74Н 5.45x39", item.Name);  // "АК" are cyrillic chars
		Assert.Equal("АК-74N 5.45x39", item.ShortName);                 // "АК" are cyrillic chars
	}

	[Fact]
	public void FilterDatabase()
	{
		var database = GetDatabase();
		var filteredDatabase = GetDatabase().Filter(item => !item.QuestItem);

		Assert.NotEqual(database.GetItems().Count(), filteredDatabase.GetItems().Count());

		Assert.NotNull(database.GetItem("5939a00786f7742fe8132936"));
		Assert.Null(filteredDatabase.GetItem("5939a00786f7742fe8132936"));
		Assert.NotNull(database.GetItem("56742c2e4bdc2d95058b456d"));
		Assert.NotNull(filteredDatabase.GetItem("56742c2e4bdc2d95058b456d"));
	}
}
