using FunctionChallenge.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionChallenge.BusinessLayer.Interfaces
{
    public interface IChartService
    {
        Task<IEnumerable<string>> GetAllChartsNamesAsync();
        Task<ChartModel> GetChartByNameAsync(string name);
        Task<string> GetPointsForAsync(ChartModel model);

        Task SaveAsync(ChartModel model);

        Task DeleteByNameAsync(string name);

        Task UpdateAsync(ChartModel model);
    }
}
