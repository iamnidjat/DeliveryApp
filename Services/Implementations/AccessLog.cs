using DeliveryApp.Models;
using DeliveryApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Services.Implementations
{
    public class AccessLog : IAccessLog
    {
        private readonly DeliveryAppDbContext _deliveryAppDbContext;
        private readonly ILoggerService _loggerService;
        public AccessLog(DeliveryAppDbContext deliveryAppDbContext, ILoggerService loggerService)
        {
            _deliveryAppDbContext = deliveryAppDbContext;
            _loggerService = loggerService;
        }

        public async Task<IEnumerable<Log>> FilterAccessLogsAsync(DateTime startTime, DateTime endTime)
        {
            try
            {
                _loggerService.Log($"Filtering access logs: from {startTime} to {endTime}");

                var filteredLogs = await _deliveryAppDbContext.Logs
                    .Where(log => log.AccessTime >= startTime && log.AccessTime <= endTime)
                    .OrderByDescending(log => log.AccessTime)
                    .ToListAsync();

                if (!filteredLogs.Any())
                {
                    _loggerService.Log($"No access logs were found!");
                    return Enumerable.Empty<Log>();
                }

                _loggerService.Log($"Found {filteredLogs.Count} access logs");
                return filteredLogs;
            }
            catch (Exception ex) when (ex is ArgumentNullException or OperationCanceledException)
            {
                _loggerService.Log($"Error occurred: {ex.Message}");
                return Enumerable.Empty<Log>();
            }
        }

        public async Task CreateAccessLogAsync(Log log)
        {
            try
            {
                await _deliveryAppDbContext.Logs.AddAsync(log);
                await _deliveryAppDbContext.SaveChangesAsync();
                _loggerService.Log($"Log {log.Id} created successfully.");
            }
            catch (Exception ex) when (ex is DbUpdateException or DbUpdateConcurrencyException or OperationCanceledException)
            {
                _loggerService.Log($"Error occurred: {ex.Message}");
            }
        }
    }
}
