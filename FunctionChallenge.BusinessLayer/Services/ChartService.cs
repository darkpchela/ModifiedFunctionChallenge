using FunctionChallenge.BusinessLayer.DTO;
using FunctionChallenge.BusinessLayer.Interfaces;
using FunctionChallenge.BusinessLayer.Infrastructure;
using FunctionChallenge.DataAccessLayer.EF;
using FunctionChallenge.DataAccessLayer.Entities;
using FunctionChallenge.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace FunctionChallenge.BusinessLayer.Services
{
    internal class ChartService : IChartService
    {
        private ILogger<ChartService> logger;
        private readonly IUnitOfWork unitOfWork;
        public ChartService(IUnitOfWork unitOfWork, ILogger<ChartService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task<string> GetPointsForAsync(ChartModel model)
        {
            if (model.to <= model.from)
               throw new CustomValidationException("Value of 'to' must be greater then 'From'", "");
            if (model.step >= (model.to - model.from))
               throw new CustomValidationException("Value of 'step' must be greater, then difference of 'from' and 'to'", "");

            //var points = await Task.Run(() =>innerFunction(model.a, model.b, model.c, model.step, model.from, model.to));
            var points = await Task.Run(()=>ParallelInnerFunc(model.a, model.b, model.c, model.step, model.from, model.to));
            return JsonSerializer.Serialize(points);
        }
        private IEnumerable<Point> innerFunction(double a, double b, double c, double step,
            double from, double to)
        {
            List<Point> points = new List<Point>();
            for (double x = from; x <= to; x += step)
            {
                double y = a * (Math.Pow(x, 2)) + (b * x) + c;
                Point point = new Point() 
                {
                    x=x,
                    y=y
                };
                points.Add(point);
            }
            return points;
        }

        private IEnumerable<Point> ParallelInnerFunc(double a, double b, double c, double step,
            double from, double to)
        {
            object locker = new object();
            List<Point> points = new List<Point>();
            Parallel.ForEach(StepIterator(from, to, step),(x)=> {
                double y = a * (Math.Pow(x, 2)) + (b * x) + c;
                Point point = new Point()
                {
                    x = x,
                    y = y
                };

                lock (locker)
                {
                    points.Add(point);
                }
            });

            return points.OrderBy(p=>p.x);

            //Iterator for parallel func
            IEnumerable<double> StepIterator(double startIndex, double endIndex, double stepSize)
            {
                for (double x = startIndex; x < endIndex; x += stepSize)
                {
                    yield return x;
                }
            }
        }
        public async Task SaveAsync(ChartModel model)
        {
            if (model.points == null)
                throw new CustomValidationException("Chart need to be plotted before saving", nameof(ChartModel.points));
            if (model.chartName == null)
                throw new CustomValidationException("Name required", nameof(ChartModel.chartName));
            if (await unitOfWork.Charts.GetAll().AnyAsync(c => c.ChartName == model.chartName))
                throw new CustomValidationException("Chart with this name already exists", nameof(ChartModel.chartName));

            //Here may use automapper
            UserData userData = new UserData() {
                a=model.a,
                b=model.b,
                c=model.c,
                step=model.step,
                x1=model.from,
                xn=model.to
            };
            await unitOfWork.UserDatas.CreateAsync(userData);
            await unitOfWork.SaveAsync();


            Chart chart = new Chart()
            {
                UDKey = userData.UDKey,
                ChartName = model.chartName
            };
            await unitOfWork.Charts.CreateAsync(chart);
            await unitOfWork.SaveAsync();

            var points = JsonSerializer.Deserialize<IEnumerable<Point>>(model.points);
            foreach (var p in  points)
            {
                p.CKey = chart.CKey;
                await unitOfWork.Points.CreateAsync(p);
            }
            await unitOfWork.SaveAsync();
            
        }

        public async Task<ChartModel> GetChartByNameAsync(string name)
        {
            var allCharts = unitOfWork.Charts.GetAll();
            var allUserDatas = unitOfWork.UserDatas.GetAll();
            var allPoints = unitOfWork.Points.GetAll();

            if (!allCharts.Any(c => c.ChartName == name))
                return new ChartModel();

            var res = await (from c in allCharts
                             join ud in allUserDatas on c.UDKey equals ud.UDKey
                             where c.ChartName == name
                             select new
                             {
                                 id = c.CKey,
                                 chartName = c.ChartName,
                                 a = ud.a,
                                 b = ud.b,
                                 c = ud.c,
                                 step = ud.step,
                                 fromX = ud.x1,
                                 toX = ud.xn

                             }).FirstAsync();

            ChartModel chartModel = new ChartModel()
            {
                a = res.a,
                b = res.b,
                c = res.c,
                chartName = res.chartName,
                from = res.fromX,
                to = res.toX,
                step = res.step,
                points = JsonSerializer.Serialize(allPoints.Where(p => p.CKey == res.id).OrderBy(p => p.x).Select(p => p))
            };
            return chartModel;
        }

        public async Task DeleteByNameAsync(string name)
        {
            var chart = unitOfWork.Charts.GetAll().First(c=>c.ChartName==name);
            await unitOfWork.Charts.DeleteAsync(chart.CKey);
        }

        public async Task<IEnumerable<string>> GetAllChartsNamesAsync()
        {
            List<string> chartNames = await unitOfWork.Charts.GetAll().Select(c => c.ChartName).ToListAsync();
            return chartNames;
        }
    }
}
