using System.Reflection;

namespace CustomObjectComparer
{
	/// <summary>
	/// Defines different types of differences between objects.
	/// </summary>
	public enum DifferenceType
	{
		NullReference,
		TypeMismatch,
		EnumerationSizeMismatch,
		ValueMismatch,
		ElementValueMismatch
	}

	/// <summary>
	/// Struct that contains information about a particular difference between objects.
	/// </summary>
	public struct ObjectDifference
	{
		/// <summary>
		/// First object. Can be null.
		/// </summary>
		public object Obj1 { get; }

		/// <summary>
		/// Second object. Can be null.
		/// </summary>
		public object Obj2 { get; }

		/// <summary>
		/// Type of the difference between the two held objects.
		/// </summary>
		public DifferenceType DifferenceType { get; }

		/// <summary>
		/// When not null, holds the information about the member which is different within <see cref="Obj1"/> and <see cref="Obj2"/>.
		/// </summary>
		public MemberInfo Member { get; }

		/// <summary>
		/// Gets the value of <see cref="Obj1"/>.
		/// </summary>
		public object Val1 => Obj1?.GetMemberValue(Member);

		/// <summary>
		/// Gets the value of <see cref="Obj2"/>.
		/// </summary>
		public object Val2 => Obj2?.GetMemberValue(Member);

		/// <summary>
		/// Initializes an instance of <see cref="ObjectDifference"/>.
		/// </summary>
		/// <param name="obj1">First object.</param>
		/// <param name="obj2">Second object.</param>
		/// <param name="differenceType">Type of the difference between the objects.</param>
		/// <param name="member">Member which introduces a difference (if applicable - otherwise leave null).</param>
		public ObjectDifference(object obj1, object obj2, DifferenceType differenceType, MemberInfo member = null)
		{
			Obj1 = obj1;
			Obj2 = obj2;
			DifferenceType = differenceType;
			Member = member;
		}

		#region Static Initializers

		/// <summary>
		/// Initializes an instance of <see cref="ObjectDifference"/> with <see cref="DifferenceType.NullReference"/>.
		/// </summary>
		/// <param name="obj1">First object.</param>
		/// <param name="obj2">Second object.</param>
		/// <param name="member">Member which introduces a difference (if applicable - otherwise leave null).</param>
		/// <returns>The instance.</returns>
		public static ObjectDifference NullReference(object obj1, object obj2, MemberInfo member = null)
		{
			return new ObjectDifference(obj1, obj2, DifferenceType.NullReference, member);
		}

		/// <summary>
		/// Initializes an instance of <see cref="ObjectDifference"/> with <see cref="DifferenceType.TypeMismatch"/>.
		/// </summary>
		/// <param name="obj1">First object.</param>
		/// <param name="obj2">Second object.</param>
		/// <param name="member">Member which introduces a difference (if applicable - otherwise leave null).</param>
		/// <returns>The instance.</returns>
		public static ObjectDifference TypeMismatch(object obj1, object obj2, MemberInfo member = null)
		{
			return new ObjectDifference(obj1, obj2, DifferenceType.TypeMismatch, member);
		}

		/// <summary>
		/// Initializes an instance of <see cref="ObjectDifference"/> with <see cref="DifferenceType.EnumerationSizeMismatch"/>.
		/// </summary>
		/// <param name="obj1">First object.</param>
		/// <param name="obj2">Second object.</param>
		/// <param name="member">Member which introduces a difference (if applicable - otherwise leave null).</param>
		/// <returns>The instance.</returns>
		public static ObjectDifference EnumerationSizeMismatch(object obj1, object obj2, MemberInfo member = null)
		{
			return new ObjectDifference(obj1, obj2, DifferenceType.EnumerationSizeMismatch, member);
		}

		/// <summary>
		/// Initializes an instance of <see cref="ObjectDifference"/> with <see cref="DifferenceType.ValueMismatch"/>.
		/// </summary>
		/// <param name="obj1">First object.</param>
		/// <param name="obj2">Second object.</param>
		/// <param name="member">Member which introduces a difference (if applicable - otherwise leave null).</param>
		/// <returns>The instance.</returns>
		public static ObjectDifference ValueMismatch(object obj1, object obj2, MemberInfo member = null)
		{
			return new ObjectDifference(obj1, obj2, DifferenceType.ValueMismatch, member);
		}

		/// <summary>
		/// Initializes an instance of <see cref="ObjectDifference"/> with <see cref="DifferenceType.ElementValueMismatch"/>.
		/// </summary>
		/// <param name="obj1">First object.</param>
		/// <param name="obj2">Second object.</param>
		/// <param name="member">Member which introduces a difference (if applicable - otherwise leave null).</param>
		/// <returns>The instance.</returns>
		public static ObjectDifference ElementValueMismatch(object obj1, object obj2, MemberInfo member = null)
		{
			return new ObjectDifference(obj1, obj2, DifferenceType.ElementValueMismatch, member);
		}

		#endregion
	}
}
