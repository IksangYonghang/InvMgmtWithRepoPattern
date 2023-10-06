using AutoMapper;
using Data.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;
using Models.Entities;
using Models.Repositories;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ICategoryRepository _categoryRepository;

		public CategoryController(IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository)
		{

			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_categoryRepository = categoryRepository;
		}

		[HttpPost]
		public async Task<ActionResult<Category>> CreateCategory(CategoryCreateDto categoryCreateDto)
		{
			var existingCategory = await _categoryRepository.GetCategoryByName(categoryCreateDto.CategoryName);
			if (existingCategory != null)
			{
				return Conflict("Category you want to create already exists");
			}

			var newCategory = _mapper.Map<Category>(categoryCreateDto);
			await _unitOfWork.Category.AddAsync(newCategory);
			await _unitOfWork.SaveAsync();
			return Ok("Category created successfully");
		}



		[HttpGet("All")]
		public async Task<ActionResult<List<Category>>> GetCategories()
		{
			var categories = await _unitOfWork.Category.GetAllAsync();
			if (categories != null)
			{
				return Ok(categories);
			}
			return NotFound("Categories list is empty");
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Category>> GetCategory(int id)
		{
			var category = await _unitOfWork.Category.GetByIdAsync(id);
			if (category != null)
			{
				return Ok(category);
			}
			return NotFound("Category you are looking for not found");
		}

		[HttpPut]
		public async Task<ActionResult<Category>> UpdateCategory(CategoryUpdateDto categoryUpdateDto, int id)
		{
			var categoryToUpdate = await _unitOfWork.Category.GetByIdAsync(id);
			if (categoryToUpdate == null)
			{
				return NotFound("Category to be updated not found");
			}
			_mapper.Map(categoryUpdateDto, categoryToUpdate);
			categoryToUpdate.UpdatedAt = DateTime.UtcNow;
			await _unitOfWork.SaveAsync();
			return Ok("Category updated successfylly");
		}

		[HttpDelete]
		public async Task<ActionResult<Category>> DeleteCategory(int id)
		{
			var categoryToBeDeleted = await _unitOfWork.Category.GetByIdAsync(id);
			if (categoryToBeDeleted == null)
			{
				return NotFound("Category to be deleted not found");
			}


			var productsWithCategory = await _unitOfWork.Product.WhereAsync(p => p.CategoryId == id);
			var productList = productsWithCategory.ToList();

			if (productList.Count > 0)
			{
				return BadRequest("Category cannot be deleted because it is associated with products");
			}

			await _unitOfWork.Category.DeleteAsync(id);
			await _unitOfWork.SaveAsync();
			return Ok("Category deleted successfully");
		}


	}
}
