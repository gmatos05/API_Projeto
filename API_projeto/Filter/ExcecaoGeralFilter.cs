using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API_projeto.Filter
{
    public class ExcecaoGeralFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problema = new ProblemDetails
            {
                Title = "Erro inesperado",
                Detail = "Ocorreu um erro inesperado na solicitação",
                Type = context.Exception.GetType().Name
            };
            switch (context.Exception)
            {

                case ArgumentException:
                    problema.Detail = "Ocorreu um erro no banco de dados";
                    context.HttpContext.Response.StatusCode = StatusCodes.Status501NotImplemented;
                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
            context.Result = new ObjectResult(problema);
        }
    }
}
