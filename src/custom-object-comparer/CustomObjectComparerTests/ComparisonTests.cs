using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using System.Collections.Generic;

using CustomObjectComparer;

namespace CustomObjectComparerTests
{
	[TestClass]
	public class ComparisonTests
	{
		private Country Serbia;
		private Country Serbia_2;
		private Country England;
		private Country England_2;

		private City Belgrade;
		private City Belgrade_2;
		private City Kikinda;
		private City Kikinda_2;
		private City London;
		private City London_2;
		
		private Address RadeTrnica137;
		private Address RadeTrnica137_2;
		private Address RadeTrnica139;
		private Address BulevarKraljaAleksandra176;
		private Address BulevarKraljaAleksandra176_2;
		private Address Wallstreet78;
		private Address Wallstreet78_2;

		private Client VladimirSivcev;
		private Client VladimirSivcev_2;

		[TestInitialize]
		public void Setup()
		{
			#region Countries

			Serbia = new Country
			{
				ID = "RS",
				Name = "Serbia"
			};

			Serbia_2 = new Country
			{
				ID = "RS",
				Name = "Serbia"
			};

			England = new Country
			{
				ID = "EN",
				Name = "England"
			};

			England_2 = new Country
			{
				ID = "EN",
				Name = "England"
			};

			#endregion

			#region Cities
			
			Belgrade = new City
			{
				ID = 1,
				Name = "Belgrade",
				Country = Serbia
			};

			Belgrade_2 = new City
			{
				ID = 1,
				Name = "Belgrade",
				Country = Serbia_2
			};

			Kikinda = new City
			{
				ID = 3,
				Name = "Kikinda",
				Country = Serbia
			};

			Kikinda_2 = new City
			{
				ID = 3,
				Name = "Kikinda",
				Country = Serbia_2
			};

			London = new City
			{
				ID = 4,
				Name = "London",
				Country = England
			};

			London_2 = new City
			{
				ID = 4,
				Name = "London",
				Country = England_2
			};

			#endregion

			#region Addresses

			RadeTrnica137 = new Address
			{
				ID = 1,
				Street = "Rade Trnica",
				Number = 137,
				City = Kikinda
			};

			RadeTrnica137_2 = new Address
			{
				ID = 1,
				Street = "Rade Trnica",
				Number = 137,
				City = Kikinda_2
			};

			RadeTrnica139 = new Address
			{
				ID = 2,
				Street = "Rade Trnica",
				Number = 139,
				City = Kikinda
			};

			BulevarKraljaAleksandra176 = new Address
			{
				ID = 5,
				Street = "Bulevar Kralja Aleksandra",
				Number = 176,
				City = Belgrade
			};

			BulevarKraljaAleksandra176_2 = new Address
			{
				ID = 5,
				Street = "Bulevar Kralja Aleksandra",
				Number = 176,
				City = Belgrade_2
			};

			Wallstreet78 = new Address
			{
				ID = 6,
				Street = "Wallstreet",
				Number = 78,
				City = London
			};

			Wallstreet78_2 = new Address
			{
				ID = 6,
				Street = "Wallstreet",
				Number = 78,
				City = London_2
			};

			#endregion

			#region Clients

			VladimirSivcev = new Client
			(
				new int[]
				{
					5,
					4,
					63
					-111
				},
				new Dictionary<int, int>()
				{
					{ 2, 3 },
					{ 8, 2 },
					{ 1, 5 }
				}
			)
			{
				ID = 1,
				FirstName = "Vladimir",
				LastName = "Sivčev",
				Email = "vladimir.sivcev@gmail.com",
				Rank = 47.32,

				PrimaryPhone = new Phone
				{
					CountryCode = "RS",
					Number = "069/42-41-506"
				},
				AlternativePhones = new List<Phone>(3)
				{
					new Phone
					{
						CountryCode = "RS",
						Number = "062/78-78-788"
					},
					new Phone
					{
						CountryCode = "RS",
						Number = "063/11-22-333"
					},
					new Phone
					{
						CountryCode = "RS",
						Number = "069/77-88-999"
					},
				},

				PrimaryTitle = new Title
				{
					Name = "Software Engineer",
					Rank = 1
				},
				AlternativeTitles = new List<Title>(3)
				{
					new Title
					{
						Name = "MSc. Software Engineering",
						Rank = 2
					},
					new Title
					{
						Name = "BSc. Software Engineering",
						Rank = 1
					},
					new Title
					{
						Name = "Driving B Category",
						Rank = 3
					}
				},

				PrimaryAddress = RadeTrnica137,
				AlternativeAddresses = new List<Address>(3)
				{
					RadeTrnica139,
					BulevarKraljaAleksandra176,
					Wallstreet78
				}
			};

			VladimirSivcev_2 = new Client
			(
				new int[]
				{
					5,
					4,
					63
					-111
				},
				new Dictionary<int, int>()
				{
					{ 2, 3 },
					{ 8, 2 },
					{ 1, 5 }
				}
			)
			{
				ID = 1,
				FirstName = "Vladimir",
				LastName = "Sivčev",
				Email = "vladimir.sivcev@gmail.com",
				Rank = 47.32,

				PrimaryPhone = new Phone
				{
					CountryCode = "RS",
					Number = "069/42-41-506"
				},
				AlternativePhones = new List<Phone>(3)
				{
					new Phone
					{
						CountryCode = "RS",
						Number = "062/78-78-788"
					},
					new Phone
					{
						CountryCode = "RS",
						Number = "063/11-22-333"
					},
					new Phone
					{
						CountryCode = "RS",
						Number = "069/77-88-999"
					},
				},

				PrimaryTitle = new Title
				{
					Name = "Software Engineer",
					Rank = 1
				},
				AlternativeTitles = new List<Title>(3)
				{
					new Title
					{
						Name = "MSc. Software Engineering",
						Rank = 2
					},
					new Title
					{
						Name = "BSc. Software Engineering",
						Rank = 1
					},
					new Title
					{
						Name = "Driving B Category",
						Rank = 3
					}
				},

				PrimaryAddress = RadeTrnica137_2,
				AlternativeAddresses = new List<Address>(3)
				{
					RadeTrnica139,
					BulevarKraljaAleksandra176_2,
					Wallstreet78_2
				}
			};

			#endregion
		}

		[TestMethod]
		public void ComparisonTest_WhenObjectsAreSame_ShouldReturnWithZeroDifferences()
		{
			// Act
			var differences = ObjectComparer.Default.DeepCompare(VladimirSivcev, VladimirSivcev_2);

			// Assert
			Assert.AreEqual(0, differences.Count());
		}

		[TestMethod]
		public void ComparisonTest_WhenPropertyValueIsChanged_ShouldReturnWithValueMismatchDifference()
		{
			// Arrange
			VladimirSivcev_2.PrimaryTitle.Name = "Intentional change";

			// Act
			var differences = ObjectComparer.Default.DeepCompare(VladimirSivcev, VladimirSivcev_2);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(VladimirSivcev.PrimaryTitle, differences.First().Obj1);
			Assert.AreEqual(VladimirSivcev_2.PrimaryTitle, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.ValueMismatch, differences.First().DifferenceType);
			Assert.AreEqual("<Name>k__BackingField", differences.First().Member.Name);
		}

		[TestMethod]
		public void ComparisonTest_WhenArrayElementIsMissing_ShouldReturnWithEnumerationSizeMismatch()
		{
			// Arrange
			List<Phone> alternativePhones = (List<Phone>)VladimirSivcev_2.AlternativePhones;
			alternativePhones.RemoveAt(2);

			// Act
			var differences = ObjectComparer.Default.DeepCompare(VladimirSivcev, VladimirSivcev_2);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(VladimirSivcev.AlternativePhones, differences.First().Obj1);
			Assert.AreEqual(VladimirSivcev_2.AlternativePhones, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.EnumerationSizeMismatch, differences.First().DifferenceType);
			Assert.IsNull(differences.First().Member);
		}

		[TestMethod]
		public void ComparisonTest_WhenSubReferentObjectIsChanged_ShouldReturnWithValueMismatchDifference()
		{
			// Arrange
			RadeTrnica137_2.Street = "Intentional change";
			
			// Act
			var differences = ObjectComparer.Default.DeepCompare(VladimirSivcev, VladimirSivcev_2);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(VladimirSivcev.PrimaryAddress, differences.First().Obj1);
			Assert.AreEqual(VladimirSivcev_2.PrimaryAddress, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.ValueMismatch, differences.First().DifferenceType);
			Assert.AreEqual("<Street>k__BackingField", differences.First().Member.Name);
		}

		[TestMethod]
		public void ComparisonTest_WhenMultipllySubReferentObjectIsChanged_ShouldReturnWithSingleValueMismatchDifference()
		{
			// Arrange
			Serbia_2.Name = "Intentional change";

			// Act
			var differences = ObjectComparer.Default.DeepCompare(VladimirSivcev, VladimirSivcev_2);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(Serbia, differences.First().Obj1);
			Assert.AreEqual(Serbia_2, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.ValueMismatch, differences.First().DifferenceType);
			Assert.AreEqual("<Name>k__BackingField", differences.First().Member.Name);
		}

		[TestMethod]
		public void ComparisonTest_WhenMultipleSubReferentObjectsAreChanged_ShouldReturnWithValueMismatchDifferenceForEachChange()
		{
			// Arrange
			RadeTrnica137_2.Street = "Intentional change";
			Serbia_2.Name = "Intentional change";

			// Act
			var differences = ObjectComparer.Default.DeepCompare(VladimirSivcev, VladimirSivcev_2);

			// Assert
			Assert.AreEqual(2, differences.Count());

			Assert.AreEqual(VladimirSivcev.PrimaryAddress, differences.First().Obj1);
			Assert.AreEqual(VladimirSivcev_2.PrimaryAddress, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.ValueMismatch, differences.First().DifferenceType);
			Assert.AreEqual("<Street>k__BackingField", differences.First().Member.Name);

			Assert.AreEqual(Serbia, differences.Last().Obj1);
			Assert.AreEqual(Serbia_2, differences.Last().Obj2);
			Assert.AreEqual(DifferenceType.ValueMismatch, differences.Last().DifferenceType);
			Assert.AreEqual("<Name>k__BackingField", differences.Last().Member.Name);
		}

		[TestMethod]
		public void ComparisonTest_WhenDictionaryPrivateFieldIsChanged_ShouldReturnEnumerationSizeMismatch()
		{
			// Arrange
			Dictionary<int, int> privateDict_2 = VladimirSivcev_2.GetPrivateDict();
			privateDict_2.Add(-15, 2);

			// Act
			var differences = ObjectComparer.Default.DeepCompare(VladimirSivcev, VladimirSivcev_2);

			// Assert
			Assert.AreEqual(1, differences.Count());

			Assert.AreEqual(VladimirSivcev.GetPrivateDict(), differences.First().Obj1);
			Assert.AreEqual(VladimirSivcev_2.GetPrivateDict(), differences.First().Obj2);
			Assert.AreEqual(DifferenceType.EnumerationSizeMismatch, differences.First().DifferenceType);
			Assert.IsNull(differences.First().Member);
		}
	}
}
