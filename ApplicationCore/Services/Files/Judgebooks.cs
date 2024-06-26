using ApplicationCore.DataAccess;
using ApplicationCore.Models;
using ApplicationCore.Models.Files;
using ApplicationCore.Specifications;
using Ardalis.Specification;
using Infrastructure.Consts;
using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using System;

namespace ApplicationCore.Services.Files;

public interface IJudgebookFilesService
{
   Task<IEnumerable<Department>> FetchDepartmentsAsync();
   Task<IEnumerable<JudgebookType>> FetchTypesAsync();
   Task<Department?> GetDepartmentByIdAsync(int id);

   Task<Department?> FindDepartmentByTitleAsync(string title);
   Task<JudgebookType?> GetTypeByIdAsync(int id);

   Task<IEnumerable<JudgebookFile>> FetchAllAsync(string include = "");

   Task<IEnumerable<JudgebookFile>> FetchAsync(JudgebookType type, string include = "");
   Task<IEnumerable<JudgebookFile>> FetchAsync(Department department, string include = "");
   Task<IEnumerable<JudgebookFile>> FetchAsync(JudgebookType type, Department department, string include = "");
   Task<IEnumerable<JudgebookFile>> FetchAsync(IEnumerable<int> ids, string include = "");
   Task<JudgebookFile?> GetByIdAsync(int id, string include = "");

   Task<IEnumerable<JudgebookFile>> FetchSameCaseEntriesAsync(JudgebookFile model, string include = "");

   Task<JudgebookFile?> CreateAsync(JudgebookFile judgebook, string ip);
   Task UpdateAsync(JudgebookFile judgebook, string ip);
   Task RemoveAsync(JudgebookFile judgebook, string userId, string ip);

   Task ReviewAsync(JudgebookFile entity, string userId, string ip);
   Task ReviewRangeAsync(IEnumerable<JudgebookFile> judgebooks, string userId, string ip);

   Task AddDownloadRecordAsync(JudgebookFile entity, string userId, string ip);
   Task<IEnumerable<ModifyRecord>> FetchDownloadRecordsAsync(ICollection<int> ids);
}

public class JudgebooksService : BaseService, IJudgebookFilesService, IBaseService
{
   private readonly IDefaultRepository<JudgebookFile> _repository;
   private readonly IDefaultRepository<JudgebookType> _typeRepository;
   private readonly IDefaultRepository<Department> _departmentsRepository;

   public JudgebooksService(IDefaultRepository<ModifyRecord> modifyRecordrepository, IDefaultRepository<JudgebookFile> repository, 
      IDefaultRepository<JudgebookType> typeRepository, IDefaultRepository<Department> departmentsRepository)
      :base(modifyRecordrepository) 
   {
      _typeRepository = typeRepository;
      _repository = repository;
      _departmentsRepository = departmentsRepository;
   }
   public async Task<IEnumerable<Department>> FetchDepartmentsAsync()
      => await _departmentsRepository.ListAsync(new DepartmentsSpecification());
   public async Task<IEnumerable<JudgebookType>> FetchTypesAsync()
      => await _typeRepository.ListAsync(new JudgebookTypesSpecification());
   public async Task<Department?> GetDepartmentByIdAsync(int id)
      => await _departmentsRepository.GetByIdAsync(id);

   public async Task<Department?> FindDepartmentByTitleAsync(string title)
   { 
      var departments = await _departmentsRepository.ListAsync(new DepartmentsSpecification());
      return departments.FirstOrDefault(x => x.Title == title);
   }
   public async Task<JudgebookType?> GetTypeByIdAsync(int id)
      => await _typeRepository.GetByIdAsync(id);
   public async Task<IEnumerable<JudgebookFile>> FetchAllAsync(string include = "")
      => await _repository.ListAsync(new JudgebookFilesSpecification(include));

   public async Task<IEnumerable<JudgebookFile>> FetchAsync(JudgebookType type, string include = "")
      => await _repository.ListAsync(new JudgebookFilesSpecification(type, include));
   public async Task<IEnumerable<JudgebookFile>> FetchAsync(Department department, string include = "")
      => await _repository.ListAsync(new JudgebookFilesSpecification(department, include));
   public async Task<IEnumerable<JudgebookFile>> FetchAsync(JudgebookType type, Department department, string include = "")
      => await _repository.ListAsync(new JudgebookFilesSpecification(type, department, include));

   public async Task<IEnumerable<JudgebookFile>> FetchAsync(IEnumerable<int> ids, string include = "")
      => await _repository.ListAsync(new JudgebookFilesSpecification(ids, include));

   public async Task<JudgebookFile?> GetByIdAsync(int id, string include = "")
      => await _repository.FirstOrDefaultAsync(new JudgebookFilesSpecification(id, include));

   public async Task<IEnumerable<JudgebookFile>> FetchSameCaseEntriesAsync(JudgebookFile model, string include = "")
      => await _repository.ListAsync(new JudgebookFileSameSaceSpecification(model, include));

   public async Task<JudgebookFile?> CreateAsync(JudgebookFile entity, string ip)
   {
      entity = await _repository.AddAsync(entity);
      var modifyRecord = ModifyRecord.Create(entity!, ActionsTypes.Create, entity.CreatedBy, ip);
      if (entity != null)
      {
         await CreateModifyRecordAsync(modifyRecord);
      }
      return entity;
   }

   public async Task UpdateAsync(JudgebookFile entity, string ip)
   {
      var existingEntity = await _repository.GetByIdAsync(entity.Id);
      string userId = entity.UpdatedBy!;

      var modifyRecord = ModifyRecord.Create(existingEntity!, ActionsTypes.Update, entity.UpdatedBy!, ip);

      if (entity.Reviewed)
      {
         entity.ReviewedAt = DateTime.Now;
         entity.ReviewedBy = userId;
      } 

      await _repository.UpdateAsync(entity);
      await CreateModifyRecordAsync(modifyRecord);

      if (entity.Reviewed) 
      {
         await CreateModifyRecordAsync(ModifyRecord.Create(entity, ActionsTypes.Review, userId, ip));
      }
   }
   public async Task RemoveAsync(JudgebookFile entity, string userId, string ip)
   {
      var existingEntity = await _repository.GetByIdAsync(entity.Id);
      entity.Removed = true;
      entity.SetUpdated(userId);

      await _repository.UpdateAsync(entity);

      var modifyRecord = ModifyRecord.Create(existingEntity!, ActionsTypes.Remove, entity.UpdatedBy!, ip);
      await CreateModifyRecordAsync(modifyRecord);
   }

   public async Task ReviewAsync(JudgebookFile entity, string userId, string ip)
   {
      var modifyRecord = ModifyRecord.Create(entity, ActionsTypes.Review, userId, ip);
      entity.Reviewed = true;
      entity.ReviewedAt = DateTime.Now;
      entity.ReviewedBy = userId;
      await _repository.UpdateAsync(entity);
      await CreateModifyRecordAsync(modifyRecord);
   }

   public async Task ReviewRangeAsync(IEnumerable<JudgebookFile> judgebooks, string userId, string ip)
   {
      var modifyRecords = judgebooks.Select(judgebook => ModifyRecord.Create(judgebook, ActionsTypes.Review, userId, ip));
      foreach (var item in judgebooks)
      {
         item.Reviewed = true;
         item.ReviewedAt = DateTime.Now;
         item.ReviewedBy = userId;
      }
      await _repository.UpdateRangeAsync(judgebooks);
      await CreateModifyRecordListAsync(modifyRecords);
   }

   public async Task AddDownloadRecordAsync(JudgebookFile entity, string userId, string ip)
   {
      var record = ModifyRecord.Create(entity, ActionsTypes.Download, userId, ip);
      await CreateModifyRecordAsync(record);
   }

   public async Task<IEnumerable<ModifyRecord>> FetchDownloadRecordsAsync(ICollection<int> ids)
      => await FetchModifyRecordsAsync(nameof(JudgebookFile), ids.Select(id => id.ToString()).ToList(), new List<string> { ActionsTypes.Download });
   

}
