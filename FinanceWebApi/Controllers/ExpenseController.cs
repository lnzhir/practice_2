using FinanceDB;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ExpenseController : Controller
	{
		private readonly DBProvider _dbContext;

		public ExpenseController(DBProvider context)
		{
			_dbContext = context;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public List<Expense> Get([FromQuery] ExpenseFilter filter)
		{
			return _dbContext.GetExpenses(filter);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public Expense? Get(int id)
		{
			return _dbContext.GetExpense(id);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public void Post([FromBody] Expense expense)
		{
			expense.Id = 0;
			_dbContext.AddExpense(expense);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public void Put([FromBody] Expense expense)
		{
			_dbContext.UpdateExpense(expense);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public void Delete(int id)
		{
			_dbContext.DeleteExpense(id);
		}
	}
}
