using DeliveryApp.Models;
using DeliveryApp.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Services.Interfaces
{
    public interface IAccessLog
    {
        Task<IEnumerable<Log>> FilterAccessLogsAsync(DateTime startTime, DateTime endTime);
        Task CreateAccessLogAsync(Log log);
    }
}
