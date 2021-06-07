using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace CustomObjectComparer
{
	/// <summary>
	/// Represents a generic object by-value deep comparer.
	/// </summary>
	public class ObjectComparer
	{
		/// <summary>
		/// Default ObjectComparer for easy access.
		/// </summary>
		public static ObjectComparer Default { get; } = new ObjectComparer();

		/// <summary>
		/// Runs the actual DeepCompare Algorithm. Check <see cref="DeepCompare{T}(T, T)"/>
		/// for more information. This method assumes that the objects passed are of the same
		/// type.
		/// </summary>
		/// <remarks>
		/// NOTE1: This method can be improved in many ways. First of, a potential sorting algorithm
		/// or a sorting comparer could be passed as an argument of the constructor of this class,
		/// which would be used in the algorithm in case order of some enumerations is not of a concirn.
		/// Some other potential arguments could also be passed as well as a form of a "configuration" of
		/// the comparer, to parameterize some behavior, hence the method is not static (as it can be).
		/// NOTE2: If any object in the hierarchy represents a collection which implements <see cref="IEnumerable"/>,
		/// for optimization purposes, internal state of the object is ignored, but only the elements are
		/// considered. This can be changed as well, if necessary.
		/// NOTE3: Any ValueType that is present somewhere within the type hierarchy will introduce boxing.
		/// </remarks>
		/// <param name="obj1">First object.</param>
		/// <param name="obj2">Second object.</param>
		/// <returns>Differences between the objects.</returns>
		private IEnumerable<ObjectDifference> DeepCompare_Algorithm(object obj1, object obj2)
		{
			// Assumes obj1.GetType() == obj2.GetType()

			// Comparison types:
			//  1) Objects
			//		a) Primitive Types
			//		b) Value + Reference Types
			//  2) Enumerables (includes Arrays)

			if (obj1 != null && obj2 != null)
			{
				Type objectType = obj1.GetType();

				if (objectType.IsPrimitive || objectType == typeof(string))
				{
					if (!obj1.Equals(obj2))
					{
						yield return new ObjectDifference
						(
							obj1,
							obj2,
							DifferenceType.ValueMismatch
						);
					}

					yield break;
				}
			}

			var queue = new OneTimeQueue<(object obj1, object obj2)>(64);

			queue.Enqueue((obj1, obj2));

			while (queue.Count > 0)
			{
				(obj1, obj2) = queue.Dequeue();

				if (obj1 == obj2)
				{
					// Only an optimization
					continue;
				}

				if (obj1 == null ^ obj2 == null)
				{
					yield return new ObjectDifference
					(
						obj1,
						obj2,
						DifferenceType.NullReference
					);

					continue;
				}

				Type objectType = obj1.GetType();

				if (objectType.IsEnumerable())
				{
					// Possible order insensitive comparison
					IEnumerator e1 = ((IEnumerable)obj1).GetEnumerator();
					IEnumerator e2 = ((IEnumerable)obj2).GetEnumerator();

					while (true)
					{
						bool e1HasNext = e1.MoveNext();
						bool e2HasNext = e2.MoveNext();

						if (e1HasNext != e2HasNext)
						{
							yield return new ObjectDifference
							(
								obj1,
								obj2,
								DifferenceType.EnumerationSizeMismatch
							);

							break;
						}

						if (!e1HasNext)
						{
							break;
						}

						object e1obj = e1.Current;
						object e2obj = e2.Current;

						Type elementType = e1obj.GetType();

						if (elementType.IsPrimitive || elementType == typeof(string))
						{
							if (!e1obj.Equals(e2obj))
							{
								yield return new ObjectDifference
								(
									obj1,
									obj2,
									DifferenceType.ElementValueMismatch
								);
							}
						}
						else
						{
							queue.Enqueue((e1.Current, e2.Current));
						}
					}
				}
				else
				{
					foreach (MemberInfo member in objectType.GetObjectStateMembers())
					{
						object val1 = obj1.GetMemberValue(member);
						object val2 = obj2.GetMemberValue(member);

						Type memberType = val1.GetType();

						if (memberType.IsPrimitive || memberType == typeof(string))
						{
							if (!val1.Equals(val2))
							{
								yield return new ObjectDifference
								(
									obj1,
									obj2,
									DifferenceType.ValueMismatch,
									member
								);
							}
						}
						else
						{
							queue.Enqueue((val1, val2));
						}
					}
				}
			}
		}

		/// <summary>
		/// Executes a deep generic comparison between two objects and yields information
		/// about differences as it finds any. The objects must be of the same type.
		/// </summary>
		/// <param name="obj1">First object.</param>
		/// <param name="obj2">Second object.</param>
		/// <returns>Enumeration of differences between the objects.</returns>
		public IEnumerable<ObjectDifference> DeepCompare(object obj1, object obj2)
		{
			if (obj1?.GetType() != obj2?.GetType())
			{
				throw new InvalidOperationException("DeepCompare requires both arguments to be of the same type.");
			}

			return DeepCompare_Algorithm(obj1, obj2);
		}

		/// <summary>
		/// Executes a deep generic comparison between two objects and yields information
		/// about differences as it finds any.
		/// </summary>
		/// <typeparam name="T">Type of the objects.</typeparam>
		/// <param name="obj1">First object.</param>
		/// <param name="obj2">Second object.</param>
		/// <returns>Enumeration of differences between the objects.</returns>
		public IEnumerable<ObjectDifference> DeepCompare<T>(T obj1, T obj2)
		{
			return DeepCompare_Algorithm(obj1, obj2);
		}
	}
}
