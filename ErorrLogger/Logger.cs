using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessFacade.Repositories;
using BusinessFacade.Repositories.Implementations;
using CsStat.Domain.Entities;
using DataService.Interfaces;

namespace ErrorLogger
{
    public interface ILogger
    {
        void Error(string latestString, Exception exception, string message);
        void Error(Exception exception, string message);
    }
    public class Logger : ILogger
    {
        private readonly IErrorLogRepository _errorLogRepository;
        private readonly IMongoRepositoryFactory _mongoRepository;

        public Logger(IMongoRepositoryFactory mongoRepository)
        {
            _mongoRepository = mongoRepository;
            _errorLogRepository = new ErrorLogRepository(_mongoRepository);
        }
        public void Error(string latestString, Exception exception, string message)
        {
            var error = new Error
            {
                Exception = exception.Message,
                LogString = latestString,
                Message = message,
                Time = $"{DateTime.Now:HH-mm:dd-MM-yyyy}"
            };

            _errorLogRepository.Error(error);
        }

        public void Error(Exception exception, string message)
        {
            Error("", exception, message);
        }
    }
}
