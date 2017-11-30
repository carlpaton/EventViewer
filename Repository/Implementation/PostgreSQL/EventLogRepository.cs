using System;
using System.Collections.Generic;
using Repository.Interface;
using SharedModels;
using Repository.Implementation;

namespace Repository.PostgreSQL
{
    public class EventLogRepository : PostgreSQLContext, IEventLogRepository
    {
        public EventLogRepository(string connectionString) : base (connectionString)
        {

        }

        public void InsertBulk(List<EventLogModel> list)
        {
            var sql =
                @"
INSERT INTO eventlogs.evtx_log
(level, event_id, record_id, time_created, log_name, provider_name, level_display_name, user_id, machine_name, description, op_code, op_code_display_name, task_category, keywords)
VALUES
(@Level, @EventID, @RecordId, @TimeCreated, @LogName, @ProviderName, @LevelDisplayName, @UserId, @MachineName, @Description, @Opcode, @OpcodeDisplayName, @TaskCategory, @Keywords);";
            InsertBulk<List<EventLogModel>>(sql, list);
        }

        public List<EventLogModel> SelectListBetween(DateTime from, DateTime to)
        {
            var sql =
                @"
SELECT * FROM eventlogs.evtx_log
WHERE time_created >= @from
AND time_created <= @to";
            return SelectList<EventLogModel>(sql, new { from, to });
        }

        public List<EventLogModel> SelectListByMachineName(string machineName)
        {
            throw new NotImplementedException();
        }

        public List<EventLogModel> SelectListDescriptionContains(string description)
        {
            var sql =
                @"
SELECT * FROM eventlogs.evtx_log
WHERE description LIKE '%" + description + "%'";
            return SelectList<EventLogModel>(sql);
        }

        public List<EventLogModel> SelectListDescriptionStartsWith(string description)
        {
            var sql =
                @"
SELECT * FROM eventlogs.evtx_log
WHERE description LIKE '" + description + "%'";
            return SelectList<EventLogModel>(sql);
        }
    }
}
