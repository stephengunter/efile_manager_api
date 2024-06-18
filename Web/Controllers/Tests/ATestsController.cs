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

namespace Web.Controllers.Tests;

public class ATestsController : BaseTestController
{
   
   private readonly IJudgebookFilesService _judgebooksService;
   private readonly IMapper _mapper;
   public ATestsController(IJudgebookFilesService judgebooksService, IMapper mapper)
   {
      _judgebooksService = judgebooksService;
      _mapper = mapper;
   }
   [HttpGet]
   public async Task<ActionResult> Index()
   {
      string includes = "type,department";
      var list = await _judgebooksService.FetchAsync([1, 24], includes);
      return Ok(list.MapViewModelList(_mapper));
   }


   [HttpGet("ex")]
   public ActionResult Ex()
   {
      throw new Exception("Test Throw Exception");
   }
}
