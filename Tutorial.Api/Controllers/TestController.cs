using Microsoft.AspNetCore.Mvc;


namespace Tutorial.Api.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        #region Property
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }
        #endregion

        [HttpGet("wait")]
        public IActionResult Waiting([FromQuery(Name = "time")] int time)
        {
            try
            {
                _logger.LogDebug("Waiting HTTP response duration time : " + time + " secound.");

                Thread.Sleep(time * 1000);

                return Ok("Waiting HTTP response duration time : " + time + " secound.");

            }
            catch (Exception e)
            {
                _logger.LogError("Get all error : ", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("log")]
        public IActionResult ApplicationInsightLog([FromQuery(Name = "lv")] string lv,[FromQuery(Name = "message")] string message)
        {
            // CONSOLE AND CONFIGURATION LOGING
            //Trace = 0, Debug = 1, Information = 2, Warning = 3, Error = 4, Critical = 5, and None = 6

            // SERVERITY LEVEL APPLICATION INSIGHT LOGGING
            /*
            Verbose = 0, Infomation = 1, Warning = 2, Error = 3, Critical = 4
            */
            
            try{
                switch(lv){
                    case "0": _logger.LogDebug("DEBUG : "+message);return Ok("DEBUG : "+message);
                    case "1": _logger.LogInformation("INFO : "+message);return Ok("INFO : "+message);
                    case "2": _logger.LogWarning("WARN : "+message);return Ok("WARN : "+message);
                    case "3": _logger.LogError("ERROR : "+message);return Ok("ERROR : "+message);
                    case "4": _logger.LogCritical("CRITICAL : "+message);return Ok("CRITICAL : "+message);
                    default:
                        return Ok("NONE : "+message);
                }
            }catch(Exception e){
                _logger.LogError(e.Message);
                return Ok("Exception : "+e.Message);
            }
            
        }
    }
}