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
	public class VendorController : ControllerBase
	{

		private readonly IUnitOfWork _unitOfWork;
		protected readonly IMapper _mapper;
		private readonly IVendorRepository _vendorRepository;

		public VendorController(IUnitOfWork unitOfWork, IMapper mapper,IVendorRepository vendorRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_vendorRepository = vendorRepository;
		}

		[HttpPost]
		public async Task<ActionResult<Vendor>> CreateVendor(VendorCreateDto vendorCreateDto)
		{
			var existingVendor =await _vendorRepository.GetVendorByName(vendorCreateDto.VendorName);
			if (existingVendor != null)
			{
				return Conflict("Vendor with the same name already exist");
			}

			var newVendor = _mapper.Map<Vendor>(vendorCreateDto);
			await _unitOfWork.Vendor.AddAsync(newVendor);
			await _unitOfWork.SaveAsync();
			return Ok("Vendor created successfully");
		}

		[HttpGet("All")]
		public async Task<ActionResult<List<Vendor>>> GetAllVendors()
		{
			var vendors = await _unitOfWork.Vendor.GetAllAsync();
			if (vendors == null)
			{
				return NotFound("Vendors not found");
			}
			return Ok(vendors);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Vendor>> GetVendor(int id)
		{
			var vendor = await _unitOfWork.Vendor.GetByIdAsync(id);
			if (vendor == null)
			{
				return NotFound("Vendor you are looking for does not exist");
			}
			return Ok(vendor);
		}

		[HttpPut]
		public async Task<ActionResult<Vendor>> UpdateVendor(int id, VendorUpdateDto vendorUpdateDto)
		{
			var vendorToUpdate = await _unitOfWork.Vendor.GetByIdAsync(id);
			if (vendorToUpdate == null)
			{
				return NotFound("Vendor to be updated not found");
			}

			_mapper.Map(vendorUpdateDto, vendorToUpdate);
			vendorToUpdate.UpdatedAt = DateTime.UtcNow;
			await _unitOfWork.SaveAsync();
			return Ok("Vendor updated successfully");
		}

		[HttpDelete]
		public async Task<ActionResult<Vendor>> DeleteVendor(int id)
		{
			var vendorToDelete = await _unitOfWork.Vendor.GetByIdAsync(id);

			if (vendorToDelete == null)
			{
				return NotFound("Vendor you are looking to delete does not exist");
			}
			var productsWithVendor = await _unitOfWork.Product.WhereAsync(p => p.VendorId == id);
			var vendorList = productsWithVendor.ToList();
			if (vendorList.Count > 0)
			{
				return BadRequest("Vendor cannot be deleted because it is associated with products");
			}

			await _unitOfWork.Vendor.DeleteAsync(id);
			await _unitOfWork.SaveAsync();
			return Ok("Vendor has been deleted sccessfully");
		}

	}
}
