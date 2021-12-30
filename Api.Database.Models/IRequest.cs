using System;
using System.Collections.Generic;

namespace Api.Database.Models
{
    public interface IRequest
    {
        string Id { get; set; }
        string ExternalId { get; set; }
        Client Client { get; set; }

        string Description { get; set; }
        AreasOfPractice Topic { get; set; }
        DateTime CreatedDate { get; set; }
        ICollection<Enquiry> Enquiries { get; set; }
    }
}