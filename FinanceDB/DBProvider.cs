using Microsoft.EntityFrameworkCore;

namespace FinanceDB
{
    public class DBProvider : DbContext
	{
		public DbSet<Category> Categories { get; set; } = null!;
		public DbSet<Expense> Expenses { get; set; } = null!;
		public DbSet<Transaction> Transactions { get; set; } = null!;


		public DBProvider(DbContextOptions<DBProvider> options)
		: base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Каскадное удаление.
			modelBuilder.Entity<Transaction>()
				.HasOne(t => t.Expense)
				.WithMany(e => e.Transactions)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Expense>()
				.HasOne(e => e.Category)
				.WithMany(c => c.Expenses)
				.OnDelete(DeleteBehavior.Cascade);

			// Каст DateTime в DateOnly и обратно.
			modelBuilder.Entity<Transaction>()
				.Property(e => e.Date)
				.HasConversion(
					d => d.ToDateTime(TimeOnly.MinValue),
					d => DateOnly.FromDateTime(d)
				);
		}

		private void _Add<T>(DbSet<T> entities, T entity) where T : class
		{
			entities.Add(entity);
			SaveChanges();
		}

		private void _Delete<T>(DbSet<T> entities, T entity) where T : class
		{
			if (entities.Contains(entity))
			{
				entities.Remove(entity);
				SaveChanges();
			}
		}

		private void _Update<T>(DbSet<T> entities, T entity) where T : class
		{
			if (entities.Contains(entity))
			{
				ChangeTracker.Clear();
				entities.Update(entity);
				SaveChanges();
			}
		}

		// Категории.

		public List<Category> GetCategories() => Categories.ToList();

		public List<Category> GetCategories(CategoryFilter filter)
		{
			var query = Categories.AsQueryable();

			if (filter.Active.HasValue)
			{
				query = query.Where(e => e.Active == filter.Active.Value);
			}

			return query.ToList();
		}

		public List<Category> GetActiveCategories() =>
			Categories.Where(e => e.Active).ToList();

		public Category? GetCategory(int id)
		{
			var query = Categories.Where(e => e.Id == id);
			return query.Count() > 0 ? query.First() : null;
		}

		public void AddCategory(Category category) =>
			_Add(Categories, category);

		public void DeleteCategory(int id)
		{
			Category? category = GetCategory(id);
			if (category != null)
			{
				Categories.Remove(category);
				SaveChanges();
			}
		}

		public void DeleteCategory(Category category) =>
			_Delete(Categories, category);

		public void UpdateCategory(Category category) =>
			_Update(Categories, category);


		// Статьи.

		public List<Expense> GetExpenses() => 
			Expenses
				.Include(e => e.Category)
				.ToList();

		public List<Expense> GetExpenses(ExpenseFilter filter)
		{
			var query = Expenses.AsQueryable();

			if (filter.Active.HasValue)
			{
				query = query.Where(e => e.Active == filter.Active.Value);
			}

			return query.Include(e => e.Category).ToList();
		}

		public List<Expense> GetActiveExpenses() => 
			Expenses
				.Where(e => e.Active)
				.Include(e => e.Category)
				.ToList();

		public Expense? GetExpense(int id)
		{
			var query = Expenses
							.Where(e => e.Id == id)
							.Include(e => e.Category);
			return query.Count() > 0 ? query.First() : null;
		}


		public void AddExpense(Expense expense) =>
			_Add(Expenses, expense);

		public void DeleteExpense(int id)
		{
			Expense? expense = GetExpense(id);
			if (expense != null)
			{
				Expenses.Remove(expense);
				SaveChanges();
			}
		}

		public void DeleteExpense(Expense expense) =>
			_Delete(Expenses, expense);

		public void UpdateExpense(Expense expense) =>
			_Update(Expenses, expense);


		// Транзакции.

		public List<Transaction> GetTransactions() => 
			Transactions
				.Include(e => e.Expense)
				.ToList();

		public List<Transaction> GetTransactions(DateOnly from, DateOnly to) => 
			Transactions
				.Where(e => e.Date >= from && e.Date <= to)
				.Include(e => e.Expense)
				.ToList();

		public List<Transaction> GetTransactionsByExpense(int expenseId) =>
			Transactions
				.Where(e => e.ExpenseId == expenseId)
				.Include(e => e.Expense)
				.ToList();

		public List<Transaction> GetTransactionsByCategory(int categoryId) =>
			Transactions
				.Where(e => e.Expense.CategoryId == categoryId)
				.Include(e => e.Expense)
				.ToList();

			//Expenses
			//	.Where(e => e.CategoryId == categoryId)
			//	.Join(Transactions, e => e.Id, t => t.ExpenseId, (e, t) => t)
			//	.Include(e => e.Expense)
			//	.ToList();

			//(from transaction in Transactions
			// join expense in Expenses on transaction.ExpenseId equals expense.Id
			// where expense.Id == categoryId
			// select transaction).ToList();

		public List<Transaction> GetTransactions(TransactionFilter filter) {
			var query = Transactions.AsQueryable();

			if (filter.DateFrom.HasValue || filter.DateTo.HasValue)
			{
				DateOnly from = filter.DateFrom.HasValue ?
					filter.DateFrom.Value : DateOnly.MinValue;
				DateOnly to = filter.DateTo.HasValue ?
					filter.DateTo.Value : DateOnly.MaxValue;

				query = query.Where(e => e.Date >= from && e.Date <= to);
			}

			if (filter.ExpenseId.HasValue)
			{
				query = query.Where(e => e.ExpenseId == filter.ExpenseId.Value);
			}

			if (filter.CategoryId.HasValue)
			{
				query = query.Where(e => e.Expense.CategoryId == filter.CategoryId.Value);
			}

			if (filter.ExpenseActive.HasValue)
			{
				query = query.Where(e => e.Expense.Active == filter.ExpenseActive.Value);
			}

			if (filter.CategoryActive.HasValue)
			{
				query = query.Where(e 
					=> e.Expense.Category.Active == filter.CategoryActive.Value);
			}
			
			return query
					.OrderByDescending(e => e.Date)
					.Include(e => e.Expense)
					.Include(e => e.Expense.Category)
					.ToList();
		}


		public Transaction? GetTransaction(Guid id)
		{
			var query = Transactions
							.Where(e => e.Id == id)
							.Include(e => e.Expense);
			return query.Count() > 0 ? query.First() : null;
		}

		public void AddTransaction(Transaction transaction) =>
			_Add(Transactions, transaction);

		public void AddTransaction(decimal price, string description, int expenseId) => 
			AddTransaction(new Transaction()
			{
				Date = DateOnly.FromDateTime(DateTime.Now),
				Price = price,
				Description = description,
				ExpenseId = expenseId
			});

		public void DeleteTransaction(Guid id)
		{
			Transaction? transaction = GetTransaction(id);
			if (transaction != null)
			{
				Transactions.Remove(transaction);
				SaveChanges();
			}
		}

		public void DeleteTransaction(Transaction transaction) =>
			_Delete(Transactions, transaction);

		public void UpdateTransaction(Transaction transaction) =>
			_Update(Transactions, transaction);

	}
}
