using Ardalis.Specification;
using ApplicationCore.Models.Files;

namespace ApplicationCore.Specifications;
public class DepartmentsSpecification : Specification<Department>
{
   public DepartmentsSpecification()
   {
      Query.Where(item => !item.Removed);
   }
}

