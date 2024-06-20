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

namespace Web.Controllers.Tests;

public class ATestsController : BaseTestController
{
   
   public ATestsController()
   {
      
   }
   [HttpGet]
   public async Task<ActionResult> Index()
   {
     
      return Ok();
   }


   [HttpGet("ex")]
   public ActionResult Ex()
   {
      throw new Exception("Test Throw Exception");
   }
}
