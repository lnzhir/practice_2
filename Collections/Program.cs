using Collections;
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




