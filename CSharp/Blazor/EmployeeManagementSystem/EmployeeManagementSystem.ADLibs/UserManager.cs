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
    }
}
