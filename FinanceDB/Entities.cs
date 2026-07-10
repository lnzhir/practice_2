

using System;
using System.Text.Json.Serialization;

namespace FinanceDB
{
	public class Category
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public decimal Budget { get; set; }
		public bool Active { get; set; }

		[JsonIgnore]
		public List<Expense> Expenses { get; set; } = new ();

		public override string ToString()
		{
			return $"{Id} {Name} {Budget} {Active}";
		}
	}

	public class Expense
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public bool Active { get; set; }
		public int CategoryId { get; set; }

		public Category? Category { get; set; }

		[JsonIgnore]
		public List<Transaction> Transactions { get; set; } = new();

		public override string ToString()
		{
			return $"{Id} {Name} {Active} {CategoryId}";
		}
	}

	public class Transaction
	{
		public Guid Id { get; set; }
		public DateOnly Date { get; set; }
		public decimal Price { get; set; }
		public string? Description { get; set; }
		public int ExpenseId { get; set; }

		public Expense? Expense { get; set; }

		public override string ToString()
		{
			return $"{Id} {Date} {Price} {Description} {ExpenseId}";
		}
	}

	public class TransactionFilter
	{
		public DateOnly? DateFrom { get; set; }
		public DateOnly? DateTo { get; set; }
		public int? ExpenseId { get; set; }
		public int? CategoryId { get; set; }
		public bool? ExpenseActive { get; set; }
		public bool? CategoryActive { get; set; }
	}

	public class ExpenseFilter
	{
		public bool? Active { get; set; }
	}

	public class CategoryFilter
	{
		public bool? Active { get; set; }
	}
}