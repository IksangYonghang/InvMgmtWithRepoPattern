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
	public class ProductController : ControllerBase
	{
		
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IProductRepository _productRepository;

		public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository)
		{
			
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_productRepository = productRepository;
		}

		[HttpPost]
		public async Task<ActionResult<Product>> CreatePost(ProductCreateDto productCreateDto)
		{
			var categoryExists = await _unitOfWork.Category.AnyAsync(c => c.Id == productCreateDto.CategoryId);
			if (!categoryExists)
			{
				return NotFound("Category not found");
			}

			var vendorExists = await _unitOfWork.Vendor.AnyAsync(v => v.Id == productCreateDto.VendorId);
			if (!vendorExists)
			{
				return NotFound("Vendor not found");
			}

			var existingProduct = await _productRepository.GetProductByName(productCreateDto.ProductName);
			if (existingProduct != null)
			{
				return Conflict("Product you are trying to create already exist");
			}

			var newProduct = _mapper.Map<Product>(productCreateDto);
			await _unitOfWork.Product.AddAsync(newProduct);
			await _unitOfWork.SaveAsync();
			return Ok("Product created successfully");
		}

		[HttpGet("All")]
		public async Task<ActionResult<List<Product>>> GetProducts()
		{
			var products = await _unitOfWork.Product.GetAllAsync();
			if (products !=null)
			{
				return Ok(products);
			}
			return NotFound("Product list is empty");
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Product>>GetProduct(int id)
		{
			var product= await _unitOfWork.Product.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound("Product you are looking for not found");
			}
			return Ok(product);
		}

		[HttpPut]
		public async Task<ActionResult<Product>>UpdateProduct(ProductUpdateDto productUpdateDto, int id)
		{
			var categoryExists = await _unitOfWork.Category.AnyAsync(c => c.Id == productUpdateDto.CategoryId);
			if (!categoryExists)
			{
				return NotFound("Category not found");
			}

			var vendorExists = await _unitOfWork.Vendor.AnyAsync(v => v.Id == productUpdateDto.VendorId);
			if (!vendorExists)
			{
				return NotFound("Vendor not found");
			}

			var productToBeUpdated = await _unitOfWork.Product.GetByIdAsync(id);
			if (productToBeUpdated == null)
			{
				return NotFound("Product to be updated not found");
			}
			_mapper.Map(productUpdateDto, productToBeUpdated);
			productToBeUpdated.UpdatedAt=DateTime.UtcNow;
			await _unitOfWork.SaveAsync();
			return Ok("Product updated successfully");
		}

		[HttpDelete]
		public async Task<ActionResult<Product>>DeleteProduct(int id)
		{
			var product = await _unitOfWork.Product.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound("Product to be deleted not found");
			}
			await _unitOfWork.Product.DeleteAsync(id);
			await _unitOfWork.SaveAsync();
			return Ok("Product Deleted successfully");
		}
	}
}
