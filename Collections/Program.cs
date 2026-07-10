using System.Collections;




var smartStack = new SmartStack<string>();

smartStack.Push("first");
smartStack.Push("second");
smartStack.Push("third");
smartStack.Push("fourth");
smartStack.Push("fiveth");


foreach (var item in smartStack)
{
	Console.WriteLine(item);
}

Console.WriteLine(smartStack.Pop());
Console.WriteLine(smartStack.Peek());
Console.WriteLine(smartStack.Count);

smartStack.PushRange(new string[] { "asd", "qwe", "zxc", "vcb", "fdg", "dfg", "rty" });

Console.WriteLine(smartStack.Capacity);

smartStack[0] = "last";

foreach (var item in smartStack)
{
	Console.WriteLine(item);
}

Console.WriteLine(smartStack.Contains("asd"));
Console.WriteLine(smartStack.Contains("123"));




class SmartStack<T> : IEnumerable<T>
{
	private T[] _data;

	public int Count { get; private set; } = 0;
	public int Capacity { get { return _data.Length; } }

	public SmartStack() : this(4)
	{
	}

	public SmartStack(int size)
	{
		_data = new T[size];
	}

	public SmartStack(IEnumerable<T> enumerable)
	{
		Count = enumerable.Count();
		_data = enumerable.ToArray();
	}

	public IEnumerator<T> GetEnumerator()
	{
		for (var  i = Count - 1; i >= 0; i--)
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

	public void Push(T elem)
	{
		_CheckSize(1);

		_data[Count++] = elem;
	}

	public void PushRange(IEnumerable<T> enumerable)
	{
		_CheckSize(enumerable.Count());

		for (var i = 0; i < enumerable.Count(); i++)
		{
			_data[Count + i] = enumerable.ElementAt(i);
		}

		Count += enumerable.Count();
	}

	public T Pop()
	{
		if (Count == 0)
		{
			throw new InvalidOperationException("Count == 0");
		}

		return _data[--Count];
	}

	public T Peek()
	{
		return _data[Count - 1];
	}

	public bool Contains(T elem)
	{
		return _data.Contains(elem);
	}

	public T this[int index]
	{
		get
		{
			if (index < 0 || index >= Count)
			{
				throw new ArgumentOutOfRangeException();
			}

			return _data[Count - 1 - index];
		}
		set
		{
			if (index < 0 || index >= Count)
			{
				throw new ArgumentOutOfRangeException();
			}

			_data[Count - 1 - index] = value;
		}
	}
}