using ApplicationCore.Models;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Helpers;
using ApplicationCore.DataAccess;
using Newtonsoft.Json;
using ApplicationCore.Views.Jud;
using ApplicationCore.Settings;
using Microsoft.Extensions.Options;
using ApplicationCore.Settings.Files;
using ApplicationCore.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Azure.Core;
using ApplicationCore.Services.Files;
using ApplicationCore.Helpers.Files;
using ApplicationCore.Consts;
using System.Data;
using ApplicationCore.Models.Files;
using ApplicationCore.Exceptions;
using Ardalis.Specification;

namespace Web.Controllers.Tests;

public class ATestsController : BaseTestController
{
   private readonly IFileStoragesService _fileStoragesService;
   private readonly IJudgebookFilesService _judgebooksService;

   private readonly JudgebookFileSettings _judgebookSettings;
   public ATestsController(IJudgebookFilesService judgebooksService, IOptions<JudgebookFileSettings> judgebookSettings)
   {
      _judgebookSettings = judgebookSettings.Value;
      _judgebooksService = judgebooksService;

      if (String.IsNullOrEmpty(_judgebookSettings.Host))
      {
         _fileStoragesService = new LocalStoragesService(_judgebookSettings.Directory);
      }
      else
      {
         _fileStoragesService = new FtpStoragesService(_judgebookSettings.Host, _judgebookSettings.UserName,
         _judgebookSettings.Password, _judgebookSettings.Directory);
      }
   }
   [HttpGet]
   public async Task<ActionResult> Index()
   {
      JudgebookType? type = null;
      Department? department = null;
      string include = "type,department";
      var judgebooks = await _judgebooksService.FetchAllAsync();

      var ids = new List<int>();
      foreach (var judgebook in judgebooks)
      { 
         if(!judgebook.Removed)
         {
            byte[] bytes;
            try
            {
               bytes = _fileStoragesService.GetBytes(judgebook.DirectoryPath, judgebook.FileName);
            }
            catch (Exception ex)
            {
               ids.Add(judgebook.Id);
            }

         }
      }
      return Ok(ids);
   }


   [HttpGet("ex")]
   public ActionResult Ex()
   {
      throw new Exception("Test Throw Exception");
   }
}
