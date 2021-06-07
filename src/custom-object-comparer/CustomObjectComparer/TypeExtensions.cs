using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomObjectComparer
{
	/// <summary>
	/// Provides type inheritance extension methods.
	/// </summary>
	public static class TypeInheritanceExtensions
	{
		#region Core Methods

		/// <summary>
		/// Checks whether @this type is the same as the argument type. Apart from reference check,
		/// this method also checks generic equivalence.
		/// </summary>
		/// <param name="this">This type.</param>
		/// <param name="type">Referent type.</param>
		/// <returns>True if types are the same. False otherwise.</returns>
		public static bool Is(this Type @this, Type type)
		{
			return
			(
				@this == type
				||
				@this.IsGenericType && @this.GetGenericTypeDefinition() == type
			);
		}

		/// <summary>
		/// Checks whether @this type inherits from a base type. This can be either a class type, or
		/// an interface type. Apart from reference checks, this method also checks generic equivalence.
		/// </summary>
		/// <param name="this">This type.</param>
		/// <param name="baseType">Referent base type.</param>
		/// <returns>True if this type inherits from baseType. False otherwise.</returns>
		public static bool Expands(this Type @this, Type baseType)
		{
			return
			(
				@this.BaseType.Is(baseType)
				||
				@this.GetInterfaces().Any(type => type.Is(baseType))
			);
		}

		/// <summary>
		/// Checks whether @this type is or inherits from another type. This can be either a class type, or
		/// an interface type. Apart from reference checks, this method also checks generic equivalence.
		/// </summary>
		/// <param name="this">This type.</param>
		/// <param name="type">Referent (base) type.</param>
		/// <returns>True if types are the same or if this type inherits from the referent type. False otherwise.</returns>
		public static bool IsOrExpands(this Type @this, Type type)
		{
			return
			(
				@this.Is(type)
				||
				@this.Expands(type)
			);
		}

		#endregion

		#region Generic Methods

		/// <summary>
		/// Checks whether @this type is the same as the argument type. Apart from reference check,
		/// this method also checks generic equivalence.
		/// </summary>
		/// <typeparam name="T">Referent type.</typeparam>
		/// <param name="this">This type.</param>
		/// <returns>True if types are the same. False otherwise.</returns>
		public static bool Is<T>(this Type @this)
		{
			return @this.Is(typeof(T));
		}

		/// <summary>
		/// Checks whether @this type inherits from a base type. This can be either a class type, or
		/// an interface type. Apart from reference checks, this method also checks generic equivalence.
		/// </summary>
		/// <typeparam name="T">Referent base type.</typeparam>
		/// <param name="this">This type.</param>
		/// <returns>True if this type inherits from baseType. False otherwise.</returns>
		public static bool Expands<T>(this Type @this)
		{
			return @this.Expands(typeof(T));
		}

		/// <summary>
		/// Checks whether @this type is or inherits from another type. This can be either a class type, or
		/// an interface type. Apart from reference checks, this method also checks generic equivalence.
		/// </summary>
		/// <typeparam name="T">Referent (base) type.</typeparam>
		/// <param name="this">This type.</param>
		/// <returns>True if types are the same or if this type inherits from the referent type. False otherwise.</returns>
		public static bool IsOrExpands<T>(this Type @this)
		{
			return @this.IsOrExpands(typeof(T));
		}

		#endregion

		#region Collection Methods

		/// <summary>
		/// Checks if a type is an enumerable.
		/// </summary>
		/// <param name="this">This type.</param>
		/// <returns>True if the type is or expands IEnumerable interface. False otherwise.</returns>
		public static bool IsEnumerable(this Type @this)
		{
			return @this.IsOrExpands<IEnumerable>();
		}

		/// <summary>
		/// Checks if a type is a generic enumerable.
		/// </summary>
		/// <param name="this">This type.</param>
		/// <returns>True if the type is or expands IEnumerable<> interface. False otherwise.</returns>
		public static bool IsGenericEnumerable(this Type @this)
		{
			return @this.IsOrExpands(typeof(IEnumerable<>));
		}

		/// <summary>
		/// Checks if a type is a collection.
		/// </summary>
		/// <param name="this">This type.</param>
		/// <returns>True if the type is or expands ICollection interface. False otherwise.</returns>
		public static bool IsCollection(this Type @this)
		{
			return @this.IsOrExpands<ICollection>();
		}

		/// <summary>
		/// Checks if a type is a generic collection.
		/// </summary>
		/// <param name="this">This type.</param>
		/// <returns>True if the type is or expands ICollection<> interface. False otherwise.</returns>
		public static bool IsGenericCollection(this Type @this)
		{
			return @this.IsOrExpands(typeof(ICollection<>));
		}

		/// <summary>
		/// Checks if an object is an enumerable.
		/// </summary>
		/// <param name="this">The object.</param>
		/// <returns>True if the type of the object is or expands IEnumerable interface. False otherwise.</returns>
		public static bool IsEnumerable(this object @this)
		{
			return @this.GetType().IsEnumerable();
		}

		/// <summary>
		/// Checks if an object is a generic enumerable.
		/// </summary>
		/// <param name="this">The object.</param>
		/// <returns>True if the type of the object is or expands IEnumerable<> interface. False otherwise.</returns>
		public static bool IsGenericEnumerable(this object @this)
		{
			return @this.GetType().IsGenericEnumerable();
		}

		/// <summary>
		/// Checks if an object is a collection.
		/// </summary>
		/// <param name="this">The object.</param>
		/// <returns>True if the type of the object is or expands ICollection interface. False otherwise.</returns>
		public static bool IsCollection(this object @this)
		{
			return @this.GetType().IsCollection();
		}

		/// <summary>
		/// Checks if an object is a generic collection.
		/// </summary>
		/// <param name="this">The object.</param>
		/// <returns>True if the type of the object is or expands ICollection<> interface. False otherwise.</returns>
		public static bool IsGenericCollection(this object @this)
		{
			return @this.GetType().IsGenericCollection();
		}

		#endregion
	}

	/// <summary>
	/// Provides methods for type member manipulation.
	/// </summary>
	public static class TypeMemberExtensions
	{
		/// <summary>
		/// Returns an array of members of the provided type which form the object's
		/// state.
		/// </summary>
		/// <param name="this">The type.</param>
		/// <returns>Members which form the state.</returns>
		public static MemberInfo[] GetObjectStateMembers(this Type @this)
		{
			return @this.FindMembers
			(
				MemberTypes.Field, // | MemberTypes.Property,
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
				null,
				null
			);
		}

		/// <summary>
		/// Retrieves the value of the member of the provided object.
		/// </summary>
		/// <param name="this">The object.</param>
		/// <param name="member">The member within the object.</param>
		/// <returns>The value of the member.</returns>
		public static object GetMemberValue(this object @this, MemberInfo member)
		{
			if (member is FieldInfo fieldInfo)
			{
				return fieldInfo.GetValue(@this);
			}

			if (member is PropertyInfo propertyInfo)
			{
				return propertyInfo.GetValue(@this);
			}

			return null;
		}
	}
}
