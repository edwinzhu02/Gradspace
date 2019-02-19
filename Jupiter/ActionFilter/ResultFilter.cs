using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using Jupiter.Models;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jupiter.ActionFilter
{
    public class ResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting (HttpActionContext  filterContext)
        {

            var check = CheckStateModel(filterContext.ModelState);

            JObject checkResult = JObject.Parse(JsonConvert.SerializeObject(check));

            if (check.IsSuccess == false)
            {
                filterContext.Response = filterContext.Request.CreateResponse(checkResult);
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }

        }

        protected Result<string> CheckStateModel(ModelStateDictionary modelState)
        {
            var result = new Result<string>();
            if (!modelState.IsValid)
            {
                result.IsSuccess = false;
                result.ErrorMessage = string.Join(",", GetErrorMessages(modelState));
                return result;
            }
            return result;
        }
        protected string[] GetErrorMessages(ModelStateDictionary modelStates)
        {
            var errorMessages = new List<string>();
            foreach (var modelState in modelStates.Values)
            {
                foreach (var modelError in modelState.Errors)
                {
                    errorMessages.Add(modelError.ErrorMessage);
                }
            }
            return errorMessages.ToArray();
        }
    }
}