using System.Diagnostics;
using GibGameDatabase.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace GibGameDatabase.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, GibGameDbRepository repository)
    {
        _logger = logger;
        Repository = repository;
    }

    private GibGameDbRepository Repository { get; }

    public IActionResult Index()
    {
        try
        {
            return View();
        }
        catch (Exception ex)
        {
            var kjh = ex.Message;
        }

        return RedirectToAction("Error");
    }

    public async Task<IActionResult> ReadPlatforms()
    {
        var res = new List<string> {"All"};
        res.AddRange(Repository.GetPlatforms());
        return Json(res);
    }

    public IActionResult ReadAllGames([DataSourceRequest] DataSourceRequest request)
    {
        var res = Repository.GetAllGames();
        var dsr = res.ToDataSourceResult(request);
        return Json(dsr);
    }

    public IActionResult ReadMultiPlatformGames([DataSourceRequest] DataSourceRequest request)
    {
        var res = Repository.GetMultiPlatformGames();
        var dsr = res.ToDataSourceResult(request);
        return Json(dsr);
    }

    public IActionResult ReadPlatformExclusiveGames([DataSourceRequest] DataSourceRequest request)
    {
        var res = Repository.GetPlatformExclusiveGames();
        var dsr = res.ToDataSourceResult(request);
        return Json(dsr);
    }


    public IActionResult MultiPlatform()
    {
        return View();
    }

    public IActionResult Exclusives()
    {
        return View();
    }

    public IActionResult SelectGame(string platform)
    {
        var game = Repository.GetRandom(platform);
        return View("GameEntryPartial", game);
    }

    public async Task<IActionResult> RebuildHol()
    {
        try
        {
            await Repository.RebuildHol();
        }
        catch (Exception ex)
        {
            var kjh = ex.Message;
        }

        return Json(null);
    }

    public async Task<IActionResult> RebuildIgdb()
    {
        try
        {
            await Repository.RebuildIgdb();
        }
        catch (Exception ex)
        {
            var kjh = ex.Message;
        }

        return Json(null);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    public async Task<IActionResult> SetGamePlayed(int id)
    {
        await Repository.SetGamePlayed(id);
        return Json(true);
    }

    public async Task<IActionResult> UnsetGamePlayed(int id)
    {
        await Repository.UnsetGamePlayed(id);
        return Json(true);
    }
}