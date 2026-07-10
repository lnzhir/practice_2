using FinanceDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : Controller
	{
		private readonly DBProvider _dbContext;

		public CategoryController(DBProvider context)
		{
			_dbContext = context;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public List<Category> Get([FromQuery] CategoryFilter filter)
		{
			return _dbContext.GetCategories(filter);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public Category? Get(int id)
		{
			return _dbContext.GetCategory(id);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public void Post([FromBody] Category category)
		{
			category.Id = 0;
			_dbContext.AddCategory(category);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public void Put([FromBody] Category category)
		{
			_dbContext.UpdateCategory(category);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public void Delete(int id)
		{
			_dbContext.DeleteCategory(id);
		}
	}
}
