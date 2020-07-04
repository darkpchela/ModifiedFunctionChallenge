using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FunctionChallenge.BusinessLayer.Interfaces;

namespace ModifiedFunctionChallenge.Controllers
{
    public class ChartViewerController : Controller
    {
        IChartService chartService;
        public ChartViewerController(IChartService chartService)
        {
            this.chartService = chartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await chartService.GetAllChartsNamesAsync();
            ViewBag.ChartsNamesList = new SelectList(list);
            return View();
        }

        [HttpPost]
        public async Task<string> ChangeChartAjax(string chartName)
        {
            var chart = await chartService.GetChartByNameAsync(chartName);
            string points = chart.points;
            return points;
        }
    }
}