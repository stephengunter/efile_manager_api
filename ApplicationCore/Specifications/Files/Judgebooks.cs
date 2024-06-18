using Ardalis.Specification;
using ApplicationCore.Models.Files;
using ApplicationCore.Models;
using Infrastructure.Helpers;

namespace ApplicationCore.Specifications;
public class JudgebookFilesSpecification : Specification<JudgebookFile>
{
   public static ICollection<string> FetchIncludes(string include)
   {
      var result = new List<string>();
      foreach (var item in include.SplitToList())
      {
         if (item.EqualTo("type")) result.Add("Type");
         else if (item.EqualTo("department")) result.Add("Department");
      }
      return result;
      
   }
   
   public JudgebookFilesSpecification(string include = "")
   {
      foreach (var item in FetchIncludes(include))
      {
         Query.Include(item);
      }
      Query.Where(item => !item.Removed);
   }
   public JudgebookFilesSpecification(Department department, string include = "")
   {
      foreach (var item in FetchIncludes(include))
      {
         Query.Include(item);
      }
      Query.Where(item => !item.Removed && item.DepartmentId == department.Id);
   }
   public JudgebookFilesSpecification(JudgebookType type, string include = "")
   {
      foreach (var item in FetchIncludes(include))
      {
         Query.Include(item);
      }
      Query.Where(item => !item.Removed && item.TypeId == type.Id);
   }
   public JudgebookFilesSpecification(JudgebookType type, Department department, string include = "")
   {
      foreach (var item in FetchIncludes(include))
      {
         Query.Include(item);
      }
      Query.Where(item => !item.Removed && item.TypeId == type.Id && item.DepartmentId == department.Id);
   }
   public JudgebookFilesSpecification(int id, string include = "")
   {
      foreach (var item in FetchIncludes(include))
      {
         Query.Include(item);
      }
      Query.Where(item => !item.Removed && item.Id == id);
   }
   public JudgebookFilesSpecification(IEnumerable<int> ids, string include = "")
   {
      foreach (var item in FetchIncludes(include))
      {
         Query.Include(item);
      }
      Query.Where(item => !item.Removed && ids.Contains(item.Id));
   }
}

public class JudgebookFileSameSaceSpecification : Specification<JudgebookFile>
{
   public JudgebookFileSameSaceSpecification(JudgebookFile model, string include = "")
   {
      foreach (var item in JudgebookFilesSpecification.FetchIncludes(include))
      {
         Query.Include(item);
      }
      Query.Where(entry => entry.Id != model.Id && !entry.Removed
          && entry.TypeId == model.TypeId && entry.CourtType == model.CourtType && entry.Year == model.Year
             && entry.Category == model.Category && entry.Num == model.Num);
   }
}

