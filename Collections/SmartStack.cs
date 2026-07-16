using System.Collections;

namespace Collections
{
	public class SmartStack<T> : IEnumerable<T>
	{
		private T[] _data;

		/// <summary>
		/// Текущее количество элементов в стеке.
		/// </summary>
		public int Count { get; private set; } = 0;

		/// <summary>
		/// Максимальный размер стека.
		/// </summary>
		public int Capacity { get { return _data.Length; } }

		/// <summary>
		/// С начальным размером в 4.
		/// </summary>
		public SmartStack() : this(4)
		{
		}

		/// <summary>
		/// Конструктор с заданием размера.
		/// </summary>
		/// <param name="size"></param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public SmartStack(int size)
		{
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("Размер отрицательный"
					, nameof(size));
			}

			_data = new T[size];
		}

		/// <summary>
		/// Инициализация по перечисляемому объекту.
		/// </summary>
		/// <param name="enumerable"></param>
		public SmartStack(IEnumerable<T> enumerable)
		{
			Count = enumerable.Count();
			_data = enumerable.ToArray();
		}

		/// <summary>
		/// Вернуть перечисление.
		/// </summary>
		/// <returns>Перечисление</returns>
		public IEnumerator<T> GetEnumerator()
		{
			for (var i = Count - 1; i >= 0; i--)
			{
				yield return _data[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private void _SetSize(int newSize)
		{
			T[] newData = new T[newSize];

			for (var i = 0; i < Count; i++)
			{
				newData[i] = _data[i];
			}

			_data = newData;
		}

		private void _CheckSize(int size)
		{
			int newSize = Count + size;
			if (Capacity < newSize)
			{
				_SetSize(newSize * 2);
			}
		}

		private int _CheckIndexAndReverse(int index)
		{
			if (index < 0 || index >= Count)
			{
				throw new ArgumentOutOfRangeException("Индекс вышел за пределы размера массива");
			}

			return Count - 1 - index;
		}

		/// <summary>
		/// Подставить элемент к верху стека.
		/// </summary>
		/// <param name="elem"></param>
		public void Push(T elem)
		{
			_CheckSize(1);

			_data[Count++] = elem;
		}

		/// <summary>
		/// Подставить перечисляемый объект к верху стека.
		/// </summary>
		/// <param name="enumerable"></param>
		public void PushRange(IEnumerable<T> enumerable)
		{
			_CheckSize(enumerable.Count());

			for (var i = 0; i < enumerable.Count(); i++)
			{
				_data[Count + i] = enumerable.ElementAt(i);
			}

			Count += enumerable.Count();
		}

		/// <summary>
		/// Убрать и вернуть элемента с верха стека.
		/// </summary>
		/// <returns>Элемент с верха стека</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public T Pop()
		{
			if (Count == 0)
			{
				throw new InvalidOperationException("Count == 0");
			}

			return _data[--Count];
		}

		/// <summary>
		/// Вернуть элемент с верха стека.
		/// </summary>
		/// <returns>Элемент с верха стека</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public T Peek()
		{
			if (Count == 0)
			{
				throw new InvalidOperationException("Count == 0");
			}

			return _data[Count - 1];
		}

		/// <summary>
		/// Содержится ли такой элемент.
		/// </summary>
		/// <param name="elem"></param>
		/// <returns>True, если содержится</returns>
		public bool Contains(T elem)
		{
			return _data.Contains(elem);
		}

		/// <summary>
		/// Вернуть или вставить элемент по индексу.
		/// </summary>
		/// <param name="index"></param>
		/// <returns>Элемент по индексу</returns>
		public T this[int index]
		{
			get => _data[_CheckIndexAndReverse(index)];
			set
			{
				_data[_CheckIndexAndReverse(index)] = value;
			}
		}
	}
}
