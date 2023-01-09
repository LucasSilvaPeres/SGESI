using System;
using Data.Models;

namespace Data
{
    public class Log
    {
        public Log(Guid userId, DateTime data, string message)
        {
            this.Id = new Guid();
            this.UserId = userId;
            this.Data = data;
            this.Message = message;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime Data { get; set; }
        public string Message { get; set; }
    }
}
