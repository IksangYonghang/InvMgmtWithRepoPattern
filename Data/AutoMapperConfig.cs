using AutoMapper;
using Models.Dtos;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			CreateMap<VendorCreateDto, Vendor>();
			CreateMap<Vendor, VendorCreateDto>();

			CreateMap<VendorUpdateDto, Vendor>();
			CreateMap<Vendor, VendorUpdateDto>();

			CreateMap<CategoryCreateDto, Category>();
			CreateMap<Category, CategoryCreateDto>();

			CreateMap<CategoryUpdateDto, Category>();
			CreateMap<Category, CategoryUpdateDto>();

			CreateMap<ProductCreateDto, Product>();
			CreateMap<Product, ProductCreateDto>();

			CreateMap<ProductUpdateDto, Product>();
			CreateMap<Product, ProductUpdateDto>();
		}
	}
}
