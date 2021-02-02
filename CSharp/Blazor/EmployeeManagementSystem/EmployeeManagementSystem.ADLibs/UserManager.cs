using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Text;

namespace EmployeeManagementSystem.ADLibs
{
    public class UserManager
    {
        private readonly string username;
        private readonly string password;
        private readonly string domain;

        public UserManager(string username, string password, string domain)
        {
            this.username = username;
            this.password = password;
            this.domain = domain;
        }

        public void CreateUser(string path, string userToCreateName, string userToCreatePassword, string givenName, string surname, string mail, string phone)
        {
            var context = new PrincipalContext(ContextType.Domain, domain, path, username, password);
            var user = new UserPrincipal(context, userToCreateName, userToCreatePassword, true);
            user.GivenName = givenName;
            user.Surname = surname;
            user.EmailAddress = mail;
            user.VoiceTelephoneNumber = phone;
            user.Save();
        }

        public bool UnlockUser(string path, string searchValue)
        {
            UserPrincipal user = FindUser(path, searchValue);
            if (user == null)
                return false;
            user.UnlockAccount();
            return true;
        }

        public bool DisableUser(string path, string searchValue)
        {
            UserPrincipal user = FindUser(path, searchValue);
            if (user == null)
                return false;
            user.Enabled = false;
            user.Save();
            return true;
        }

        public bool EnableUser(string path, string searchValue)
        {
            UserPrincipal user = FindUser(path, searchValue);
            if (user == null)
                return false;
            user.Enabled = true;
            user.Save();
            return true;
        }

        public bool EnableUser(string path, string searchValue, string newPassword)
        {
            UserPrincipal user = FindUser(path, searchValue);
            if (user == null)
                return false;
            user.SetPassword(newPassword);
            user.Save();
            return true;
        }

        public bool AddUserToGroup(string path, string userName, string groupName)
        {
            var context = new PrincipalContext(ContextType.Domain, domain, path, username, password);
            var user = FindUser(context, path, userName);
            if (user == null)
                return false;
            var group = FindGroup(context, path, groupName);
            if (group == null)
                return false;
            group.Members.Add(user);
            group.Save();
            return true;
        }

        public bool RemoveUserFromGroup(string path, string userName, string groupName)
        {
            var context = new PrincipalContext(ContextType.Domain, domain, path, username, password);
            var user = FindUser(context, path, userName);
            if (user == null)
                return false;
            var group = FindGroup(context, path, groupName);
            if (group == null)
                return false;
            group.Members.Remove(user);
            group.Save();
            return true;
        }

        public ADUser GetUser(string path, string searchValue)
        {
            UserPrincipal user = FindUser(path, searchValue);
            if (user == null)
                return null;
            return new ADUser
            {
                LastLogon = user.LastLogon,
                LastBadPasswordAttempt = user.LastBadPasswordAttempt,
                Sid = user.Sid,
                BadLogonCount = user.BadLogonCount,
                AccountLockoutTime = user.AccountLockoutTime,
                Name = user.Name,
            };
        }

        private UserPrincipal FindUser(PrincipalContext ctx, string path, string searchValue)
        {
            var user = UserPrincipal.FindByIdentity(ctx, IdentityType.Name, searchValue);
            return user;
        }

        private UserPrincipal FindUser(string path, string searchValue)
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain, path, username, password);
            var user = UserPrincipal.FindByIdentity(ctx, IdentityType.Name, searchValue);
            return user;
        }

        private GroupPrincipal FindGroup(PrincipalContext ctx, string path, string searchValue)
        {
            var user = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, searchValue);
            return user;
        }

        private GroupPrincipal FindGroup(string path, string searchValue)
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain, path, username, password);
            var user = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, searchValue);
            return user;
        }

    }
}
