using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using Mapster;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Concretes
{
    public class LogService : ILogService
    {
        public readonly ILogRepository<LogEntry> _logRepository;

        public LogService(ILogRepository<LogEntry> logRepository)
        {
            _logRepository = logRepository;
        }
        
        public async Task<IReadOnlyList<LogEntryDto>> GetLogsAsync()
        {

            var logEntry = await _logRepository.GetAllLogsAsync();
            logEntry = logEntry.OrderByDescending(x => x.TimeStamp).ToList();

            var logEntryDto = logEntry.Adapt<IReadOnlyList<LogEntryDto>>();

            return logEntryDto;
        }
    }
}
