using System;
using Data;
using System.Threading.Tasks;
using Serilog;

namespace Interface.Helpers
{
    public class LoggerHelper
    {
        private readonly Context _context;

        public LoggerHelper(Context context)
        {
            _context = context;
        }

        public async Task Log(Guid userId, string message)
        {
            var log = new Data.Log(userId, LocalDateTime.PegaHoraBrasilia(), message);

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
