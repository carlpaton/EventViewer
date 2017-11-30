using SharedModels;
using System;
using System.Collections.Generic;

namespace Repository.Interface
{
    public interface IEventLogRepository
    {
        void InsertBulk(List<EventLogModel> list);
        List<EventLogModel> SelectListBetween(DateTime from, DateTime to);
        List<EventLogModel> SelectListByMachineName(string machineName);
        List<EventLogModel> SelectListDescriptionStartsWith(string description);
        List<EventLogModel> SelectListDescriptionContains(string description);
    }
}
