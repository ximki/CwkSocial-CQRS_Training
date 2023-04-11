namespace CwkSocial.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiError = new ErrorResponse();

                apiError = new ErrorResponse()
                {
                    StatusCode = 400,
                    StatusPhrase = "Bad Request",
                    Timestamp = DateTime.Now,
                };
               var errors= context.ModelState.AsEnumerable();
                //context.Result = new BadRequestObjectResult(apiError) { StatusCode=400};
                foreach (var error in errors)
                {
                    foreach (var inner in error.Value.Errors)
                    {
                        apiError.Errors.Add(inner.ErrorMessage);
                    }
                }
                context.Result = new BadRequestObjectResult(apiError);
            }
        }
        
    }
}
