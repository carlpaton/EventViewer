using System;

namespace SharedModels
{
    public class EventLogModel
    {
        public byte? Level { get; set; }
        public int EventId { get; set; }
        public long? RecordId { get; set; }
        public DateTime? TimeCreated { get; set; }
        public string LogName { get; set; }
        public string ProviderName { get; set; }
        public string LevelDisplayName { get; set; }
        public string UserId { get; set; }
        public string MachineName { get; set; }
        public string Description { get; set; }

        //Custom CategoryModels ~ see comment at 'Common.EventLogger.Log' where category is passed
        public int TaskCategory { get; set; }

        //Ive never used these but they may be useful later?
        public short Opcode { get; set; }
        public string OpcodeDisplayName { get; set; }
        public string Keywords { get; set; }

        //Timestamp the record was inserted at
        public DateTime? InsertDate { get; set; }
    }
}
