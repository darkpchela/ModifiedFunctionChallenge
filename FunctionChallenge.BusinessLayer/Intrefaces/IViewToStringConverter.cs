using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionChallenge.BusinessLayer.Interfaces
{
    public interface IViewToStringConverter
    {
        Task<string> RenderPartialViewToString(Controller controller, string viewName, object model = null);
    }
}
