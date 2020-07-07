using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FunctionChallenge.BusinessLayer.Interfaces;
using FunctionChallenge.Models;

namespace ModifiedFunctionChallenge.Controllers
{
    public class ChartViewerController : Controller
    {
        IChartService chartService;
        IViewToStringConverter viewConverter;
        public ChartViewerController(IChartService chartService, IViewToStringConverter viewConverter)
        {
            this.chartService = chartService;
            this.viewConverter = viewConverter;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await chartService.GetAllChartsNamesAsync();
            ViewBag.ChartsNamesList = new SelectList(list);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeChartAjax(string chartName)
        {
            var chart = await chartService.GetChartByNameAsync(chartName);

            string html = await viewConverter.RenderPartialViewToString(this, "FuncView", new ChartViewModel
            {
                a = chart.a,
                b = chart.b,
                c = chart.c,
                from = chart.from,
                to = chart.to,
                step = chart.step,
                points = chart.points,
                chartName = chart.chartName
            });

            string points = chart.points;
            return Json(new {points, html });
        }
    }
}