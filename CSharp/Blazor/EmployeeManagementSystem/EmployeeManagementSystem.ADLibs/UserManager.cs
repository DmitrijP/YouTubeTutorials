using EmployeeManagementSystem.ADLibs.Interfaces;
using EmployeeManagementSystem.ADLibs.Interfaces.Models;
using System;
using System.DirectoryServices.AccountManagement;

namespace EmployeeManagementSystem.ADLibs
{
    public class UserManager : IUserManager, IDisposable
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _domain;
        private readonly string _path;
        private PrincipalContext _ctx;

        public UserManager(string username, string password, string domain, string path)
        {
            _username = username;
            _password = password;
            _domain = domain;
            _path = path;
            Initialize();
        }  

        public void Create(string username, string password, string firstName, string lastName, string mail, string phone)
        {
            using var user = new UserPrincipal(_ctx, username, password, false);
            user.GivenName = firstName;
            user.Surname = lastName;
            user.EmailAddress = mail;
            user.VoiceTelephoneNumber = phone;
            user.Save();
        }

        public void Delete(string name)
        {
            using var user = FindUser(name);
            user.Delete();
        }

        public void Unlock(string searchValue)
        {
            using var user = FindUser(searchValue);
            user.UnlockAccount();
        }

        public void ExpirePassword(string searchValue)
        {
            using var user = FindUser(searchValue);
            user.ExpirePasswordNow();
        }

        public void RefreshExpiredPassword(string searchValue)
        {
            using var user = FindUser(searchValue);
            user.ExpirePasswordNow();
        }

        public void Disable(string searchValue)
        {
            using var user = FindUser(searchValue);
            user.Enabled = false;
            user.Save();
        }

        public void Enable(string searchValue)
        {
            using var user = FindUser(searchValue);
            user.Enabled = true;
            user.Save();
        }

        public void ChangePassword(string searchValue, string newPassword)
        {
            using var user = FindUser(searchValue);
            user.SetPassword(newPassword);
            user.Save();
        }

        public ADUser GetDetails(string username)
        {
            using var user = FindUser(username);
            if (user == null)
                return null;
            return new ADUser
            {
                LastLogon = user.LastLogon,
                LastBadPasswordAttempt = user.LastBadPasswordAttempt,
                Sid = user.Sid.ToString(),
                BadLogonCount = user.BadLogonCount,
                AccountLockoutTime = user.AccountLockoutTime,
                Name = user.Name,
                Enabled = user.Enabled,
            };
        }

        private void Initialize()
         => _ctx = new PrincipalContext(ContextType.Domain, _domain, _path, _username, _password);

        private UserPrincipal FindUser(string searchValue)
            => UserPrincipal.FindByIdentity(_ctx, IdentityType.Name, searchValue);

        public void Dispose()
        {
            if(_ctx != null)
                _ctx.Dispose();
        }
    }
}
