using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK
{
    public class SqlExceptionFilter : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            if(context.Exception is SqlException)
            {
                var result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "DatabaseError",
                    controller = "Error"
                }));
                context.Result = result;
                context.ExceptionHandled = true;
            }
        }
    }
}
