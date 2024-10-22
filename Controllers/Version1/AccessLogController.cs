using DeliveryApp.Models;
using DeliveryApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers.Version1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessLogController : ControllerBase
    {
        private readonly IAccessLog _accessLog;
        private readonly ILoggerService _loggerService;
        public AccessLogController(IAccessLog accessLog, ILoggerService loggerService)
        {
            _accessLog = accessLog;
            _loggerService = loggerService;
        }

        [HttpGet("FilterAccessLogs")]
        public async Task<IEnumerable<Log>> FilterAccessLogsAsync(DateTime startTime, DateTime endTime)
        {
            return await _accessLog.FilterAccessLogsAsync(startTime, endTime);
        }

        [HttpPost("CreateAccessLog")]
        public async Task<IActionResult> CreateAccessLogAsync([FromBody] Log log)
        {
            if (!ModelState.IsValid)
            {
                _loggerService.Log("Invalid access log data received.");
                return BadRequest("Invalid data.");
            }

            await _accessLog.CreateAccessLogAsync(log);

            return Ok();
        }
    }
}
