using System;
using System.ComponentModel.DataAnnotations;

namespace LogViewWebApplication.Models
{
    public class LogReadViewModel
    {
        [Required]
        [Display(Name = "Date From")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TimeFrom { get; set; }

        [Required]
        [Display(Name = "Date To")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TimeTo { get; set; }

        public int Id { get; set; }
        public int Level { get; set; }
        public int EventId { get; set; }

        [Display(Name = "Task Category")]
        public int TaskCategory { get; set; }
        public int Opcode { get; set; }
        public long RecordId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TimeCreated { get; set; }
        public DateTime InsertDate { get; set; }

        public string LogName { get; set; }
        public string ProviderName { get; set; }
        public string LevelDisplayName { get; set; }
        public string UserId { get; set; }
        public string MachineName { get; set; }
        public string Description { get; set; }
        public string OpcodeDisplayName { get; set; }
        public string Keywords { get; set; }
    }
}