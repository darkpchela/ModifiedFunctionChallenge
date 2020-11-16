using AutoMapper;
using FunctionChallenge.BusinessLayer.DTO;
using FunctionChallenge.BusinessLayer.Infrastructure;
using FunctionChallenge.BusinessLayer.Interfaces;
using FunctionChallenge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FunctionChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChartService chartService;

        public HomeController(IChartService chartService)
        {
            this.chartService = chartService;
        }

        [HttpGet]
        public IActionResult Main()
        {
            return View(new ChartViewModel()
            {
                a = 5,
                b = 5,
                c = 16,
                step = 1,
                from = -10,
                to = 10
            });
        }

        [HttpPost]
        public async Task<IActionResult> Function(ChartViewModel chartViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Main", chartViewModel);

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartViewModel, ChartModel>().ReverseMap()).CreateMapper();
                var model = mapper.Map<ChartViewModel, ChartModel>(chartViewModel);
                string points = await chartService.GetPointsForAsync(model);
                chartViewModel.points = points;//Maybe later better to remove "points" from VM, and work only through session
                HttpContext.Session.SetString("points", points);
                return View("Main", chartViewModel);
            }
            catch (CustomValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return View("Main", chartViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(ChartViewModel chartViewModel)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartViewModel, ChartModel>().ReverseMap()).CreateMapper();
                var model = mapper.Map<ChartViewModel, ChartModel>(chartViewModel);
                string sessionPoints = HttpContext.Session.GetString("points");
                string points = await chartService.GetPointsForAsync(model);
                if (sessionPoints != points)
                    throw new CustomValidationException("Values of current chart were changed. Plot new before saving", "");

                await chartService.SaveAsync(model);
                return View("Main");
            }
            catch (CustomValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return View("Main", chartViewModel);
            }
        }

        #region ReactVersion

        [HttpGet]
        public IActionResult ReactMain()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FunctionAjax(ChartModel functionView)
        {
            if (functionView.to <= functionView.from)
            {
                ModelState.AddModelError(nameof(functionView.to), "Value of 'to' must be greater then 'From'");
            }
            if (functionView.step >= (functionView.to - functionView.from))
            {
                ModelState.AddModelError(nameof(functionView.step), "Value of 'step' must be greater, then difference of 'from' and 'to'");
            }

            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ChartViewModel, ChartModel>().ReverseMap()).CreateMapper();
                string points = await chartService.GetPointsForAsync(functionView);
                return Json(points);
            }
            return Json(null);
        }

        #endregion ReactVersion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}