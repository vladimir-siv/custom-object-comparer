using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using System.Collections.Generic;

using CustomObjectComparer;

namespace CustomObjectComparerTests
{
	[TestClass]
	public class ComparisonTests
	{
		#region Models

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
		
		private Address Svetosavska55;
		private Address Svetosavska55_2;
		private Address BraceTatica60;
		private Address BulevarKraljaAleksandra73;
		private Address BulevarKraljaAleksandra73_2;
		private Address Wallstreet78;
		private Address Wallstreet78_2;

		private Client JohnDoe;
		private Client JohnDoe_2;

		#endregion

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

			Svetosavska55 = new Address
			{
				ID = 1,
				Street = "Svetosavska",
				Number = 55,
				City = Kikinda
			};

			Svetosavska55_2 = new Address
			{
				ID = 1,
				Street = "Svetosavska",
				Number = 55,
				City = Kikinda_2
			};

			BraceTatica60 = new Address
			{
				ID = 2,
				Street = "Brace Tatica",
				Number = 60,
				City = Kikinda
			};

			BulevarKraljaAleksandra73 = new Address
			{
				ID = 5,
				Street = "Bulevar Kralja Aleksandra",
				Number = 73,
				City = Belgrade
			};

			BulevarKraljaAleksandra73_2 = new Address
			{
				ID = 5,
				Street = "Bulevar Kralja Aleksandra",
				Number = 73,
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

			JohnDoe = new Client
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
				FirstName = "John",
				LastName = "Doe",
				Email = "john.doe@gmail.com",
				Rank = 47.32,

				PrimaryPhone = new Phone
				{
					CountryCode = "RS",
					Number = "061/11-22-333"
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

				PrimaryAddress = Svetosavska55,
				AlternativeAddresses = new List<Address>(3)
				{
					BraceTatica60,
					BulevarKraljaAleksandra73,
					Wallstreet78
				}
			};

			JohnDoe_2 = new Client
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
				FirstName = "John",
				LastName = "Doe",
				Email = "john.doe@gmail.com",
				Rank = 47.32,

				PrimaryPhone = new Phone
				{
					CountryCode = "RS",
					Number = "061/11-22-333"
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

				PrimaryAddress = Svetosavska55_2,
				AlternativeAddresses = new List<Address>(3)
				{
					BraceTatica60,
					BulevarKraljaAleksandra73_2,
					Wallstreet78_2
				}
			};

			#endregion
		}

		[TestMethod]
		public void ComparisonTest_WhenObjectsAreTerminalAndEqual_ShouldReturnWithZeroDifferences()
		{
			// Act
			var differences = ObjectComparer.Default.DeepCompare(17, 17);

			// Assert
			Assert.AreEqual(0, differences.Count());
		}

		[TestMethod]
		public void ComparisonTest_WhenObjectsAreTerminalAndDifferent_ShouldReturnWithValueMismatchDifference()
		{
			// Arrange
			object obj1 = 21;
			object obj2 = 53;

			// Act
			var differences = ObjectComparer.Default.DeepCompare(obj1, obj2);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(obj1, differences.First().Obj1);
			Assert.AreEqual(obj2, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.ValueMismatch, differences.First().DifferenceType);
			Assert.IsNull(differences.First().Member);
		}

		[TestMethod]
		public void ComparisonTest_WhenObjectTypesAreNotEqual_ShouldReturnWithTypeMismatchDifference()
		{
			// Arrange
			object obj1 = 7;
			object obj2 = 3.14;

			// Act
			var differences = ObjectComparer.Default.DeepCompare(obj1, obj2);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(obj1, differences.First().Obj1);
			Assert.AreEqual(obj2, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.TypeMismatch, differences.First().DifferenceType);
			Assert.IsNull(differences.First().Member);
		}

		[TestMethod]
		public void ComparisonTest_WhenObjectReferencesAreEqual_ShouldReturnWithZeroDifferences()
		{
			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe);

			// Assert
			Assert.AreEqual(0, differences.Count());
		}

		[TestMethod]
		public void ComparisonTest_WhenObjectsAreSame_ShouldReturnWithZeroDifferences()
		{
			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

			// Assert
			Assert.AreEqual(0, differences.Count());
		}

		[TestMethod]
		public void ComparisonTest_WhenOneObjectIsNull_ShouldReturnWithNullReferenceDifference()
		{
			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, null);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(JohnDoe, differences.First().Obj1);
			Assert.AreEqual(null, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.NullReference, differences.First().DifferenceType);
			Assert.IsNull(differences.First().Member);
		}

		[TestMethod]
		public void ComparisonTest_WhenPropertyValueIsSetToNull_ShouldReturnWithNullReferenceDifference()
		{
			// Arrange
			JohnDoe_2.PrimaryTitle = null;

			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(JohnDoe, differences.First().Obj1);
			Assert.AreEqual(JohnDoe_2, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.NullReference, differences.First().DifferenceType);
			Assert.AreEqual("<PrimaryTitle>k__BackingField", differences.First().Member.Name);
		}

		[TestMethod]
		public void ComparisonTest_WhenElementIsSetToNull_ShouldReturnWithNullReferenceDifference()
		{
			// Arrange
			var alternativeTitles = (List<Title>)JohnDoe.AlternativeTitles;
			var alternativeTitles_2 = (List<Title>)JohnDoe_2.AlternativeTitles;

			alternativeTitles[1] = null;

			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(alternativeTitles[1], differences.Last().Obj1);
			Assert.AreEqual(alternativeTitles_2[1], differences.Last().Obj2);
			Assert.AreEqual(DifferenceType.NullReference, differences.Last().DifferenceType);
			Assert.IsNull(differences.Last().Member);
		}

		[TestMethod]
		public void ComparisonTest_WhenPropertyValueIsChanged_ShouldReturnWithValueMismatchDifference()
		{
			// Arrange
			JohnDoe_2.PrimaryTitle.Name = "Intentional change";

			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(JohnDoe.PrimaryTitle, differences.First().Obj1);
			Assert.AreEqual(JohnDoe_2.PrimaryTitle, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.ValueMismatch, differences.First().DifferenceType);
			Assert.AreEqual("<Name>k__BackingField", differences.First().Member.Name);
		}

		[TestMethod]
		public void ComparisonTest_WhenArrayElementIsMissing_ShouldReturnWithEnumerationSizeMismatch()
		{
			// Arrange
			List<Phone> alternativePhones = (List<Phone>)JohnDoe_2.AlternativePhones;
			alternativePhones.RemoveAt(2);

			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(JohnDoe.AlternativePhones, differences.First().Obj1);
			Assert.AreEqual(JohnDoe_2.AlternativePhones, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.EnumerationSizeMismatch, differences.First().DifferenceType);
			Assert.IsNull(differences.First().Member);
		}

		[TestMethod]
		public void ComparisonTest_WhenSubReferentObjectIsChanged_ShouldReturnWithValueMismatchDifference()
		{
			// Arrange
			Svetosavska55_2.Street = "Intentional change";
			
			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

			// Assert
			Assert.AreEqual(1, differences.Count());
			Assert.AreEqual(JohnDoe.PrimaryAddress, differences.First().Obj1);
			Assert.AreEqual(JohnDoe_2.PrimaryAddress, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.ValueMismatch, differences.First().DifferenceType);
			Assert.AreEqual("<Street>k__BackingField", differences.First().Member.Name);
		}

		[TestMethod]
		public void ComparisonTest_WhenMultipllySubReferentObjectIsChanged_ShouldReturnWithSingleValueMismatchDifference()
		{
			// Arrange
			Serbia_2.Name = "Intentional change";

			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

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
			Svetosavska55_2.Street = "Intentional change";
			Serbia_2.Name = "Intentional change";

			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

			// Assert
			Assert.AreEqual(2, differences.Count());

			Assert.AreEqual(JohnDoe.PrimaryAddress, differences.First().Obj1);
			Assert.AreEqual(JohnDoe_2.PrimaryAddress, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.ValueMismatch, differences.First().DifferenceType);
			Assert.AreEqual("<Street>k__BackingField", differences.First().Member.Name);

			Assert.AreEqual(Serbia, differences.Last().Obj1);
			Assert.AreEqual(Serbia_2, differences.Last().Obj2);
			Assert.AreEqual(DifferenceType.ValueMismatch, differences.Last().DifferenceType);
			Assert.AreEqual("<Name>k__BackingField", differences.Last().Member.Name);
		}

		[TestMethod]
		public void ComparisonTest_WhenArrayPrivateFieldElementValueIsChanged_ShouldReturnWithElementValueMismatchDifference()
		{
			// Arrange
			int[] privateArray_2 = JohnDoe_2.GetPrivateArray();
			privateArray_2[1] = 778;

			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

			// Assert
			Assert.AreEqual(1, differences.Count());

			Assert.AreEqual(JohnDoe.GetPrivateArray(), differences.First().Obj1);
			Assert.AreEqual(JohnDoe_2.GetPrivateArray(), differences.First().Obj2);
			Assert.AreEqual(DifferenceType.ElementValueMismatch, differences.First().DifferenceType);
			Assert.IsNull(differences.First().Member);
		}

		[TestMethod]
		public void ComparisonTest_WhenDictionaryPrivateFieldElementValueIsChanged_ShouldReturnWithValueMismatchDifference()
		{
			// Arrange
			Dictionary<int, int> privateDict_2 = JohnDoe_2.GetPrivateDict();
			privateDict_2[8] = 7;

			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

			// Assert
			Assert.AreEqual(1, differences.Count());

			Assert.AreEqual(new KeyValuePair<int, int>(8, JohnDoe.GetPrivateDict()[8]), differences.First().Obj1);
			Assert.AreEqual(new KeyValuePair<int, int>(8, JohnDoe_2.GetPrivateDict()[8]), differences.First().Obj2);
			Assert.AreEqual(DifferenceType.ValueMismatch, differences.First().DifferenceType);
			Assert.AreEqual("value", differences.Last().Member.Name);
		}

		[TestMethod]
		public void ComparisonTest_WhenDictionaryPrivateFieldIsChanged_ShouldReturnWithEnumerationSizeMismatch()
		{
			// Arrange
			Dictionary<int, int> privateDict_2 = JohnDoe_2.GetPrivateDict();
			privateDict_2.Add(-15, 2);

			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

			// Assert
			Assert.AreEqual(1, differences.Count());

			Assert.AreEqual(JohnDoe.GetPrivateDict(), differences.First().Obj1);
			Assert.AreEqual(JohnDoe_2.GetPrivateDict(), differences.First().Obj2);
			Assert.AreEqual(DifferenceType.EnumerationSizeMismatch, differences.First().DifferenceType);
			Assert.IsNull(differences.First().Member);
		}

		[TestMethod]
		public void ComparisonTest_WhenObjectsContainSubtypeMembers_ShouldReturnWithTypeMismatchDifference()
		{
			// Arrange
			JohnDoe.PrimaryTitle = new UserTitle
			{
				Name = JohnDoe.PrimaryTitle.Name,
				Rank = JohnDoe.PrimaryTitle.Rank,
				Designation = "Addition"
			};

			var alternativeTitles = (List<Title>)JohnDoe.AlternativeTitles;
			var alternativeTitles_2 = (List<Title>)JohnDoe_2.AlternativeTitles;

			alternativeTitles[1] = new UserTitle
			{
				Name = alternativeTitles[1].Name,
				Rank = alternativeTitles[1].Rank,
				Designation = "Addition"
			};

			// Act
			var differences = ObjectComparer.Default.DeepCompare(JohnDoe, JohnDoe_2);

			// Assert
			Assert.AreEqual(2, differences.Count());

			Assert.AreEqual(JohnDoe, differences.First().Obj1);
			Assert.AreEqual(JohnDoe_2, differences.First().Obj2);
			Assert.AreEqual(DifferenceType.TypeMismatch, differences.First().DifferenceType);
			Assert.AreEqual("<PrimaryTitle>k__BackingField", differences.First().Member.Name);

			Assert.AreEqual(alternativeTitles[1], differences.Last().Obj1);
			Assert.AreEqual(alternativeTitles_2[1], differences.Last().Obj2);
			Assert.AreEqual(DifferenceType.TypeMismatch, differences.Last().DifferenceType);
			Assert.IsNull(differences.Last().Member);
		}
	}
}
