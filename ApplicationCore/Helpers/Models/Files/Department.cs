using Infrastructure.Helpers;
using ApplicationCore.Models;
using ApplicationCore.Views.Files;
using AutoMapper;
using Infrastructure.Paging;
using System;
using ApplicationCore.Models.Files;
using Ardalis.Specification;

namespace ApplicationCore.Helpers.Files;
public static class DepartmentHelpers
{
   public static DepartmentViewModel MapViewModel(this Department entity, IMapper mapper)
      => mapper.Map<DepartmentViewModel>(entity);

   public static List<DepartmentViewModel> MapViewModelList(this IEnumerable<Department> entities, IMapper mapper)
      => entities.Select(item => MapViewModel(item, mapper)).ToList();

   public static Department MapEntity(this DepartmentViewModel model, IMapper mapper, string currentUserId, Department? entity = null)
   {
      if (entity == null) entity = mapper.Map<DepartmentViewModel, Department>(model);
      else entity = mapper.Map<DepartmentViewModel, Department>(model, entity);

      return entity;
   }

   public static IEnumerable<Department> GetOrdered(this IEnumerable<Department> entities)
     => entities.OrderBy(item => item.Order);
}
