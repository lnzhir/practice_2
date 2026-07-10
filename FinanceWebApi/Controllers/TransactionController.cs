using FinanceDB;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TransactionController : Controller
	{
		private const decimal _maxDaySum = 1000000;
		private readonly DBProvider _dbContext;

		public TransactionController(DBProvider context)
		{
			_dbContext = context;
		}

		private ObjectResult _ValidateDaySum(Transaction transaction)
		{
			decimal daySum = _dbContext
				.Transactions
				.Where(e => e.Id != transaction.Id && e.Date == transaction.Date)
				.Sum(e => e.Price);

			if (daySum + transaction.Price > _maxDaySum)
			{
				return StatusCode(StatusCodes.Status403Forbidden, "Limit of 1M rub exceeded.");
			}
			return Ok(null);
		}

		/// <summary>
		/// Возвращает список транзакций.
		/// </summary>
		/// <param filter="filter"></param>
		/// <returns>Список транзакций</returns>
		/// <response code="200">Возвращает список транзакций</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public List<Transaction> Get([FromQuery] TransactionFilter filter)
		{
			return _dbContext.GetTransactions(filter);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public Transaction? Get(Guid id)
		{
			return _dbContext.GetTransaction(id);
		}

		/// <summary>
		/// Создается новая транзакция.
		/// </summary>
		/// <returns>Ничего</returns>
		/// <response code="200">Ничего не возвращается</response>
		/// <response code="403">Если сумма за конкретный день больше 1 млн. руб.</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public ActionResult Post([FromBody] Transaction transaction)
		{
			ObjectResult result = _ValidateDaySum(transaction);
			if (result.StatusCode != StatusCodes.Status200OK)
			{
				return result;
			}

			transaction.Id = Guid.Empty;
			_dbContext.AddTransaction(transaction);

			return Ok();
		}

		/// <summary>
		/// Изменяется запись транзакции.
		/// </summary>
		/// <returns>Ничего</returns>
		/// <response code="200">Ничего не возвращается</response>
		/// <response code="403">Если сумма за конкретный день больше 1 млн. руб.</response>
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public ActionResult Put([FromBody] Transaction transaction)
		{
			ObjectResult result = _ValidateDaySum(transaction);
			if (result.StatusCode != StatusCodes.Status200OK)
			{
				return result;
			}

			_dbContext.UpdateTransaction(transaction);

			return Ok();
		}

		/// <summary>
		/// Удаляется транзакция по id.
		/// </summary>
		/// <returns>Ничего</returns>
		/// <response code="200">Удаляется транзакция по id</response>
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public void Delete(Guid id)
		{
			_dbContext.DeleteTransaction(id);
		}
	}
}
