using FunctionChallenge.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionChallenge.BusinessLayer.Interfaces
{
    public interface IChartService
    {
        Task<ChartModel> GetChartAsync(int id);
        Task<string> GetPointsForAsync(ChartModel model);

        Task SaveAsync(ChartModel model);

        Task DeleteAsync(int id);

        Task UpdateAsync(ChartModel model);
    }
}
