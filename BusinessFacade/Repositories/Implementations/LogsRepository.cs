﻿using System;
using System.Collections.Generic;
using System.Linq;
using CsStat.Domain.Entities;
using DataService.Interfaces;
using MongoDB.Driver.Builders;

namespace BusinessFacade.Repositories.Implementations
{
    public class LogsRepository : ILogsRepository
    {
        private static IMongoRepositoryFactory _mongoRepository;

        public LogsRepository(IMongoRepositoryFactory mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }
  
        public IEnumerable<LogModel> GetAllLogs()
        {
            return _mongoRepository.GetRepository<LogModel>().Collection.FindAll();
        }

        public IEnumerable<LogModel> GetLogsForPeriod(DateTime timeFrom, DateTime timeTo)
        {
            var query = new QueryBuilder<LogModel>();
            return _mongoRepository.GetRepository<LogModel>().Collection.Find(query.And(query.GTE(x => x.DateTime, timeFrom), query.LTE(x => x.DateTime, timeTo))).ToList();
        }
    }
}