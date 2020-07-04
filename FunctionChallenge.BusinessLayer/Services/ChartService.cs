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

namespace FunctionChallenge.BusinessLayer.Services
{
    internal class ChartService : IChartService
    {
        private readonly IUnitOfWork unitOfWork;
        //private readonly IMapper mapper;
        public ChartService(IUnitOfWork unitOfWork/*, IMapper mapper*/)
        {
            this.unitOfWork = unitOfWork;
            //this.mapper = mapper;
        }

        public async Task<string> GetPointsForAsync(ChartModel model)
        {
            if (model.to <= model.from)
               throw new CustomValidationException("Value of 'to' must be greater then 'From'", "");
            if (model.step >= (model.to - model.from))
               throw new CustomValidationException("Value of 'step' must be greater, then difference of 'from' and 'to'", "");

            var points = await Task.Run(() =>innerFunction(model.a, model.b, model.c, model.step, model.from, model.to));
            return JsonSerializer.Serialize(points);
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

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.Charts.DeleteAsync(id);
        }

        public async Task<ChartModel> GetChartAsync(int id)
        {
            //var chart = await unitOfWork.Charts.GetAsync(id);
            //var model = mapper.Map<ChartModel>(chart);

            //return model;
            return null;
        }

        public async Task UpdateAsync(ChartModel model)
        {

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
    }
}
