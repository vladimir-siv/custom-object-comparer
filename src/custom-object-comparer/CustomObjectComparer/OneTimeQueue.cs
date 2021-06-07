using System.Collections.Generic;

namespace CustomObjectComparer
{
	/// <summary>
	/// Represents a queue where a particular object can be enqueued only once.
	/// </summary>
	/// <typeparam name="T">Type of the elements.</typeparam>
	public sealed class OneTimeQueue<T>
	{
		/// <summary>
		/// Set containing all objects which were/still are enqueued.
		/// </summary>
		private readonly HashSet<T> Set;

		/// <summary>
		/// Queue of the objects.
		/// </summary>
		private readonly Queue<T> Queue;

		/// <summary>
		/// Gets the number of elements queued.
		/// </summary>
		public int Count => Queue.Count;

		/// <summary>
		/// Initializes an instance of a <see cref="OneTimeQueue{T}"/>.
		/// </summary>
		public OneTimeQueue()
		{
			Set = new HashSet<T>();
			Queue = new Queue<T>();
		}

		/// <summary>
		/// Initializes an instance of a <see cref="OneTimeQueue{T}"/>.
		/// </summary>
		/// <param name="capacity">Initial queue capacity.</param>
		public OneTimeQueue(int capacity)
		{
			Set = new HashSet<T>();
			Queue = new Queue<T>(capacity);
		}
		
		/// <summary>
		/// Enqueues an object.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns>True if enqueue is successful. False if the object was already enqueued before.</returns>
		public bool Enqueue(T obj)
		{
			if (!Set.Add(obj))
			{
				return false;
			}

			Queue.Enqueue(obj);
			return true;
		}

		/// <summary>
		/// Retrieves the next element in the queue.
		/// </summary>
		/// <returns>The element.</returns>
		public T Dequeue()
		{
			return Queue.Dequeue();
		}
	}
}
