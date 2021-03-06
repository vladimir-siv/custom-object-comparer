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
			// Comparison types:
			//  1) Objects
			//		a) Primitive Types
			//		b) Value + Reference Types
			//  2) Enumerables (includes Arrays)

			if (obj1 == obj2)
			{
				// Only an optimization
				yield break;
			}

			var queue = new OneTimeQueue<(object obj1, object obj2)>(64);

			queue.Enqueue((obj1, obj2));

			while (queue.Count > 0)
			{
				(obj1, obj2) = queue.Dequeue();

				if (obj1 == obj2)
				{
					continue;
				}

				if (obj1 == null ^ obj2 == null)
				{
					yield return ObjectDifference.NullReference(obj1, obj2);
					continue;
				}

				Type objectType = obj1.GetType();

				if (objectType != obj2.GetType())
				{
					yield return ObjectDifference.TypeMismatch(obj1, obj2);
					continue;
				}

				if (objectType.IsTerminal())
				{
					if (!obj1.Equals(obj2))
					{
						yield return ObjectDifference.ValueMismatch(obj1, obj2);
					}

					continue;
				}

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
							yield return ObjectDifference.EnumerationSizeMismatch(obj1, obj2);
							break;
						}

						if (!e1HasNext)
						{
							break;
						}

						object e1obj = e1.Current;
						object e2obj = e2.Current;

						if (e1obj == e2obj)
						{
							continue;
						}

						if (e1obj == null ^ e2obj == null)
						{
							yield return ObjectDifference.NullReference(e1obj, e2obj);
							continue;
						}

						Type elementType = e1obj.GetType();

						if (elementType != e2obj.GetType())
						{
							yield return ObjectDifference.TypeMismatch(e1obj, e2obj);
							continue;
						}

						if (elementType.IsTerminal())
						{
							if (!e1obj.Equals(e2obj))
							{
								yield return ObjectDifference.ElementValueMismatch(obj1, obj2);
							}

							continue;
						}

						queue.Enqueue((e1.Current, e2.Current));
					}
				}
				else
				{
					foreach (MemberInfo member in objectType.GetObjectStateMembers())
					{
						object val1 = obj1.GetMemberValue(member);
						object val2 = obj2.GetMemberValue(member);

						if (val1 == val2)
						{
							continue;
						}

						if (val1 == null ^ val2 == null)
						{
							yield return ObjectDifference.NullReference(obj1, obj2, member);
							continue;
						}

						Type memberType = val1.GetType();

						if (memberType != val2.GetType())
						{
							yield return ObjectDifference.TypeMismatch(obj1, obj2, member);
							continue;
						}

						if (memberType.IsTerminal())
						{
							if (!val1.Equals(val2))
							{
								yield return ObjectDifference.ValueMismatch(obj1, obj2, member);
							}

							continue;
						}

						queue.Enqueue((val1, val2));
					}
				}
			}
		}

		/// <summary>
		/// Executes a deep generic comparison between two objects and yields information
		/// about differences as it finds any.
		/// </summary>
		/// <param name="obj1">First object.</param>
		/// <param name="obj2">Second object.</param>
		/// <returns>Enumeration of differences between the objects.</returns>
		public IEnumerable<ObjectDifference> DeepCompare(object obj1, object obj2)
		{
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
