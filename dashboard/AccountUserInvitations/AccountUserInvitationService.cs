using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Database.MySql;
using Microsoft.EntityFrameworkCore;
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
            // Check if the user already exists in the account
            var doesUserExist = await context.Users.Include(u => u.Accounts).AnyAsync(u =>
                u.Email == userEmail && u.Accounts.Any(a => a.ExternalId == account.ExternalId));
            if (doesUserExist) throw new System.Exception("User already exists for this account");
            if (referUser.Email == userEmail) throw new Exception("You can not invite yourself to your firm");
            
            var guid = Guid.NewGuid().ToString();
            var aui = new AccountUserInvitation()
            {
                Account = account,
                UserEmail = userEmail,
                ReferUser = referUser,
                ExternalId = guid,
                CreatedAt = DateTime.Now,
                Status = 0,
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