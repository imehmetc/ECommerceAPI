using ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{
    public interface ILogService
    {
        Task<IReadOnlyList<LogEntryDto>> GetLogsAsync();
    }
}
