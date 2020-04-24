using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace SesNotifications.DataAccess
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                SessionManager.Manager.BeginTransaction(IsolationLevel.ReadCommitted);

                var executedContext = await next.Invoke();

                if (executedContext.Exception != null || executedContext.HttpContext.Response.StatusCode == 500)
                {
                    SessionManager.Manager.RollbackTransaction();
                    return;
                }

                try
                {
                    SessionManager.Manager.CommitTransaction();
                }
                catch (Exception e)
                {
                    SessionManager.Manager.RollbackTransaction();
                    Logger.Error(e, "Transaction commit failed");
                }
            }
            finally
            {
                SessionManager.Manager.CloseSession();
            }
        }
    }
}