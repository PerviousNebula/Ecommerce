using System.Linq;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DBProject.ActionFilters
{
    public class ValidateEntityExistsAttribute<T> : IActionFilter where T: class, IEntity
    {
        private readonly RepositoryContext _context;

        public ValidateEntityExistsAttribute(RepositoryContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int id = 0;

            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (int)context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }

            var entity = _context.Set<T>().SingleOrDefault(x => x.id == id);
            if(entity == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}