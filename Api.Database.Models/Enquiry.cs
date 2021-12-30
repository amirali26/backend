using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

namespace Api.Database.Models
{
    public enum Status
    {
        OPEN = 1,
        CONTACTED = 2,
        IN_COMMUNCATION = 3
    }

    public class Enquiry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [GraphQLIgnore]
        public int Id { get; set; }

        [GraphQLName("id")] public string ExternalId { get; set; }

        public string Message { get; set; }
        public int InitialConsultationFee { get; set; }
        public int? EstimatedPrice { get; set; }
        public bool OfficeAppointment { get; set; }
        public bool PhoneAppointment { get; set; }
        public bool VideoCallAppointment { get; set; }
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "nvarchar(24)")] public Status Status { get; set; }

        public Request Request { get; set; }
        public Account Account { get; set; }
        public User User { get; set; }
    }
}