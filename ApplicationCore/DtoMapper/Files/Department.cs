using ApplicationCore.Models.Files;
using ApplicationCore.Views.Files;
using AutoMapper;

namespace ApplicationCore.DtoMapper;

public class DepartmentMappingProfile : Profile
{
	public DepartmentMappingProfile()
	{
		CreateMap<Department, DepartmentViewModel>();

		CreateMap<DepartmentViewModel, Department>();
	}
}
