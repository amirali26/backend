using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

namespace Api.Database.Models
{
    public class Request : IRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [GraphQLIgnore]
        public string Id { get; set; }
        [GraphQLName("id")] public string ExternalId { get; set; }
        public string Description { get; set; }
        public AreasOfPractice Topic { get; set; }
        public DateTime CreatedDate { get; set; }
        public Client Client { get; set; }

        public ICollection<Enquiry> Enquiries { get; set; }
    }
}