namespace CwkSocial.Api.Controllers.v1
{
    public class BaseController: ControllerBase
    {
        protected IActionResult HandleErrorResponse(List<Error>errors)
        {
            var apiError = new ErrorResponse();
            if (errors.Any(e => e.Code == ErrorCode.NotFound))
            {
                var error = errors.First(e => e.Code == ErrorCode.NotFound);
                 apiError = new ErrorResponse()
                {
                    StatusCode = 404,
                    StatusPhrase = "Not Found",
                    Timestamp = DateTime.Now,
                };
                apiError.Errors.Add(error.Message);
                return NotFound(apiError);
            }
            
                
                 apiError = new ErrorResponse()
                {
                    StatusCode = 400,
                    StatusPhrase = "Bad request",
                    Timestamp = DateTime.Now,
                };
                errors.ForEach(e=>apiError.Errors.Add(e.Message));
                //apiError.Errors.Add("Unknown Error");
                return StatusCode(400, apiError);
           
        }
    }
}
