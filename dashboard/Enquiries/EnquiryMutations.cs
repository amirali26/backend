using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Database.MySql;
using dashboard.context;
using dashboard.Utilities;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace dashboard.Enquiries
{
    [ExtendObjectType(Name = "Mutation")]
    public class EnquiryMutations
    {
        [UseProjection]
        public async Task<IQueryable<Enquiry>> AddEnquiry([Service] DashboardContext context,
            [GlobalState("UserContext")] UserContext userContext, EnquiryInput enquiryInput)
        {
            var account = context.Accounts.FirstOrDefault(a => a.ExternalId == userContext.AccountId);
            var user = context.Users.FirstOrDefault(u => u.ExternalId == userContext.UserId);
            var request = context.Requests.Include(r => r.Client).FirstOrDefault(r => r.ExternalId == enquiryInput.RequestId);

            var enquiry = new Enquiry
            {
                Account = account,
                User = user,
                Message = enquiryInput.Message,
                Request = request,
                Status = Status.CONTACTED,
                EstimatedPrice = enquiryInput.EstimatedPrice,
                OfficeAppointment = enquiryInput.OfficeAppointment,
                PhoneAppointment = enquiryInput.PhoneAppointment,
                InitialConsultationFee = enquiryInput.InitialConsultationFee,
                VideoCallAppointment = enquiryInput.VideoCallAppointment,
                ExternalId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now
            };

            context.Enquiries.Add(enquiry);
            context.SaveChanges();

            var emailSubmission = new EnquiryEmailSubmission
            {
                EmailAddress = user.Email,
                RequestEmail = request.Client.Email
            };

            await AWSHelper.SendEmail(
                JsonConvert.SerializeObject(emailSubmission),
                "EnquirySubmission",
                enquiry.ExternalId);

            return context.Enquiries.Where(e => e.ExternalId == enquiry.ExternalId);
        }
    }
}