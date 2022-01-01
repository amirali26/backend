using System;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Database.MySql;
using Newtonsoft.Json;

namespace dashboard.AccountUserInvitations
{
    public interface IAccountUserInvitationService
    {
        Task<AccountUserInvitation> AddAccountUserInvitationService(User referUser, string userEmail, Account account);
    }
    public class AccountUserInvitationService : IAccountUserInvitationService
    {
        private readonly DashboardContext context;
        public AccountUserInvitationService(DashboardContext _context)
        {
            context = _context;
        }
        
        public async Task<AccountUserInvitation> AddAccountUserInvitationService(User referUser, string userEmail, Account account)
        {
            var guid = Guid.NewGuid().ToString();
            var aui = new AccountUserInvitation()
            {
                Account = account,
                UserEmail = userEmail,
                ReferUser = referUser,
                ExternalId = guid
            };
            
            var accountUserInvitation = await context.AccountUserInvitations.AddAsync(aui);
            await context.SaveChangesAsync();

            var emailEnquiry = new
            {
                FirmName = account.Name,
                EmailAddress = userEmail,
            };
            
            await AWSHelper.SendEmail(
                JsonConvert.SerializeObject(emailEnquiry),
                "FirmInvitation",
                guid
            );
            return accountUserInvitation.Entity;
        }
    }
}